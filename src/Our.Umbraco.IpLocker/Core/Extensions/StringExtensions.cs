namespace Our.Umbraco.IpLocker.Core.Migrations
{
    public static class StringExtensions
    {
        public static bool IsSet(this string s)
        {
            return !string.IsNullOrWhiteSpace(s);
        }

        public static string EnsurePrefix(this string s, string prefix)
        {
            if (!s.IsSet()) return string.Empty;
            return s.StartsWith(prefix) ? s : prefix + s;
        }
    }
}
