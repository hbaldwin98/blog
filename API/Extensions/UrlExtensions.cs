using System.Text.RegularExpressions;

namespace API.Extensions
{
    public static class UrlExtensions
    {
        public static string ConvertUrl(this string url)
        {
            url = Regex.Replace(url, "[^a-zA-Z0-9 ]", String.Empty).Replace(" ", "-").ToLower();
            return url;
        }   
    }
}