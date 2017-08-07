using Simple301.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Simple301.Core.Extensions;
using System.Text.RegularExpressions;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.SqlSyntax;

namespace Simple301.Core
{
    /// <summary>
    /// Redirect Repository that handles CRUD operations for the repository collection
    /// Utilizes Umbraco Database context to persist redirects into the database with
    /// cached in memory collection for fast query
    /// </summary>
    public static class RedirectRepository
    {
        /// <summary>
        /// Get all redirects from the repositry
        /// </summary>
        /// <returns>Collection of redirects</returns>
        public static IEnumerable<Redirect> GetAllRedirects()
        {
            // Update with latest from DB
            return FetchRedirects().Select(x => x.Value);
        }

        /// <summary>
        /// Add a new redirect to the redirects collection
        /// </summary>
        /// <param name="oldUrl">Old Url to redirect from</param>
        /// <param name="newUrl">New Url to redirect to</param>
        /// <param name="notes">Any associated notes with this redirect</param>
        /// <returns>New redirect from DB if successful</returns>
        public static Redirect AddRedirect(string domain, string oldUrl, string newUrl, string notes)
        {
            if (!oldUrl.IsSet()) throw new ArgumentNullException("oldUrl");
            if (!newUrl.IsSet()) throw new ArgumentNullException("newUrl");

            //Ensure starting slash if not regex
            oldUrl = oldUrl.EnsurePrefix("/").ToLower();

            // Allow external redirects and ensure slash if not absolute
            newUrl = Uri.IsWellFormedUriString(newUrl, UriKind.Absolute) ?
                newUrl : 
                newUrl.EnsurePrefix("/").ToLower();

            // First look for single match
            var redirect = FetchRedirectsByOldUrl(oldUrl, domain);
            if (redirect != null) throw new ArgumentException("A redirect for " + domain + oldUrl + " already exists");

            //Add redirect to DB
            var db = ApplicationContext.Current.DatabaseContext.Database;
            var idObj = db.Insert(new Redirect()
            {
                OldUrl = oldUrl,
                NewUrl = newUrl,
                LastUpdated = DateTime.Now.ToUniversalTime(),
                Notes = notes,
                Domain = domain
            });

            //Fetch the added redirect
            var newRedirect = FetchRedirectById(Convert.ToInt32(idObj));

            //return new redirect
            return newRedirect;
        }

        /// <summary>
        /// Update a given redirect
        /// </summary>
        /// <param name="redirect">Redirect to update</param>
        /// <returns>Updated redirect if successful</returns>
        public static Redirect UpdateRedirect(Redirect redirect)
        {
            if (redirect == null) throw new ArgumentNullException("redirect");
            if (!redirect.OldUrl.IsSet()) throw new ArgumentNullException("redirect.OldUrl");
            if (!redirect.NewUrl.IsSet()) throw new ArgumentNullException("redirect.NewUrl");

            //Ensure starting slash
            redirect.OldUrl = redirect.OldUrl.EnsurePrefix("/").ToLower();

            // Allow external redirects and ensure slash if not absolute
            redirect.NewUrl = Uri.IsWellFormedUriString(redirect.NewUrl, UriKind.Absolute) ?
                redirect.NewUrl :
                redirect.NewUrl.EnsurePrefix("/").ToLower();

            //todo: do infinite loop check with domain - previous version was without

            //get DB Context, set update time, and persist
            var db = ApplicationContext.Current.DatabaseContext.Database;
            redirect.LastUpdated = DateTime.Now.ToUniversalTime();
            db.Update(redirect);

            //return updated redirect
            return redirect;
        }

        /// <summary>
        /// Handles deleting a redirect from the redirect collection
        /// </summary>
        /// <param name="id">Id of redirect to remove</param>
        public static void DeleteRedirect(int id)
        {
            var item = FetchRedirectById(id);
            if (item == null) throw new ArgumentException("No redirect with an Id that matches " + id);

            //Get database context and delete
            var db = ApplicationContext.Current.DatabaseContext.Database;
            db.Delete(item);
        }

        /// <summary>
        /// Handles finding a redirect based on the oldUrl
        /// </summary>
        /// <param name="oldUrl">Url to search for</param>
        /// <returns>Matched Redirect</returns>
        public static Redirect FindRedirect(string oldUrl, string domain)
        {
            var matchedRedirect = FetchRedirectsByOldUrl(oldUrl, domain);
            if (matchedRedirect != null) return matchedRedirect;

            return null;
        }

        /// <summary>
        /// Fetches all redirects through cache layer
        /// </summary>
        /// <returns>Collection of redirects</returns>
        private static Dictionary<string,Redirect> FetchRedirects()
        {
            return FetchRedirectsFromDb();
        }

        /// <summary>
        /// Fetches a single redirect from the DB based on an Id
        /// </summary>
        /// <param name="id">Id of redirect to fetch</param>
        /// <returns>Single redirect with matching Id</returns>
        private static Redirect FetchRedirectById(int id)
        {
            return ApplicationContext.Current.DatabaseContext.Database.Single<Redirect>(id);
        }

        /// <summary>
        /// Fetches a single redirect from the DB based on OldUrl
        /// </summary>
        /// <param name="oldUrl">OldUrl of redirect to find</param>
        /// <returns>Single redirect with matching OldUrl</returns>
        private static Redirect FetchRedirectsByOldUrl(string oldUrl, string domain)
        {
            var query = "SELECT * FROM Redirects WHERE OldUrl=@0 AND Domain=@1";
            return ApplicationContext.Current.DatabaseContext.Database.Query<Redirect>(query, oldUrl, domain).SingleOrDefault();
        }

        /// <summary>
        /// Handles fetching a single redirect from the DB based
        /// on the provided query and parameter
        /// </summary>
        /// <typeparam name="T">Datatype of param</typeparam>
        /// <param name="query">Query</param>
        /// <param name="param">Param value</param>
        /// <returns>Redirect</returns>
        private static IEnumerable<Redirect> FetchRedirectFromDbByQuery<T>(string query, params object[] param)
        {
            var db = ApplicationContext.Current.DatabaseContext.Database;
            return db.Query<Redirect>(query, param);
        }

        /// <summary>
        /// Handles fetching a collection of redirects from the DB based
        /// on the provided query and parameter
        /// </summary>
        /// <typeparam name="T">Datatype of param</typeparam>
        /// <param name="query">Query</param>
        /// <param name="param">Param value</param>
        /// <returns>Collection of redirects</returns>
        private static List<Redirect> FetchRedirectsFromDbByQuery<T>(string query, T param)
        {
            var db = ApplicationContext.Current.DatabaseContext.Database;
            return db.Query<Redirect>(query, param).ToList();
        }

        /// <summary>
        /// Fetches all redirects from the database
        /// </summary>
        /// <returns>Collection of redirects</returns>
        private static Dictionary<string, Redirect> FetchRedirectsFromDb()
        {
            var db = ApplicationContext.Current.DatabaseContext.Database;
            var redirects = db.Query<Redirect>("SELECT * FROM Redirects");
            return redirects != null ? redirects.ToDictionary(x => x.OldUrl) : new Dictionary<string, Redirect>();
        }

        /// <summary>
        /// Detects a loop in the redirects list given the new redirect.
        /// Uses Floyd's cycle-finding algorithm.
        /// </summary>
        /// <param name="oldUrl">Old URL for new redirect</param>
        /// <param name="newUrl">New URL for new redirect</param>
        /// <param name="redirects">Current list of all redirects</param>
        /// <returns>True if loop detected, false if no loop detected</returns>
        private static bool DetectLoop(string oldUrl, string newUrl, Dictionary<string, Redirect> redirects)
        {
            // quick check for any links to this new redirect
            if (!redirects.ContainsKey(newUrl) && !redirects.Any(x => x.Value.NewUrl.Equals(oldUrl))) return false;

            // clone redirect list
            var linkedList = redirects.ToDictionary(entry => entry.Key, entry => entry.Value);
            var redirect = new Redirect() { OldUrl = oldUrl, NewUrl = newUrl };

            // add new redirect to cloned list for traversing
            if (!linkedList.ContainsKey(oldUrl))
                linkedList.Add(oldUrl, redirect);
            else
                linkedList[oldUrl] = redirect;

            // Use Floyd's cycle finding algorithm to detect loops in a linked list
            var slowP = redirect;
            var fastP = redirect;

            while (slowP != null && fastP != null && linkedList.ContainsKey(fastP.NewUrl))
            {
                slowP = linkedList[slowP.NewUrl];
                fastP = linkedList.ContainsKey(linkedList[fastP.NewUrl].NewUrl) ? linkedList[linkedList[fastP.NewUrl].NewUrl] : null;

                if (slowP == fastP)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
