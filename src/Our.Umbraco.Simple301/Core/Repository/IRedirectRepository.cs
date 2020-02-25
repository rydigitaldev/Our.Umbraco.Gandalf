using System.Collections.Generic;
using Simple301.Core.Models;

namespace Simple301.Core
{
    public interface IRedirectRepository
    {
        Redirect AddRedirect(bool isRegex, string oldUrl, string newUrl, string notes);
        void ClearCache();
        void DeleteRedirect(int id);
        Redirect FindRedirect(string oldUrl);
        IEnumerable<Redirect> GetAllRedirects();
        Redirect UpdateRedirect(Redirect redirect);
    }
}