using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace TB.UI.Helper
{
    public static class SiteHelper
    {
        public static string RemoveHtmlTag(this string str)
        {
            return Regex.Replace(str, "<.*?>", string.Empty);
        }

        public static string GregorianToPersian(this DateTime date)
        {
            PersianCalendar persian = new PersianCalendar();
            return String.Format("{0}/{1}/{2}", persian.GetYear(date), persian.GetMonth(date), persian.GetDayOfMonth(date));
        }
        public static string SubStringDescription(this string str, int length = 80)
        {
            if (str.Length > length)
            {
                return str.Substring(0, length);
            }
            else
            {
                return str;
            }
        }
        public static string GetDisplayName(this Enum @enum)
        {
            var item = @enum.GetType().GetMember(@enum.ToString()).First()
                .GetCustomAttribute<DisplayAttribute>();

            return item?.Name ?? "";
        }

        public static T GetQueryStringByKey<T>(this NavigationManager nav, string key)
        {
            var uri = nav.ToAbsoluteUri(nav.Uri);

            QueryHelpers.ParseQuery(uri.Query).TryGetValue(key, out var queryResult);

            if (typeof(T).Equals(typeof(int)))
            {
                int.TryParse(queryResult, out int result);
                return (T)(object)result;
            }

            if (typeof(T).Equals(typeof(string)))
            {
                return (T)(object)queryResult.ToString();
            }

            return default;
        }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T) , value , true);
        }
    }
}
