using System.Text.RegularExpressions;

namespace TB.UI.Helper
{
    public static class SiteHelper
    {
        public static string RemoveHtmlTag(this string str)
        {
            return Regex.Replace(str , "<.*?>" , string.Empty);
        }
    }
}
