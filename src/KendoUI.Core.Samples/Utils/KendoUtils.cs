using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace KendoUI.Core.Samples.Utils
{
    public static class KendoUtils
    {
        public static string GetJsonDataFromQueryString(this HttpContext httpContext)
        {
            var rawQueryString = httpContext.Request.QueryString.ToString();
            var rawQueryStringKeyValue = QueryHelpers.ParseQuery(rawQueryString).FirstOrDefault();
            var dataString = Uri.UnescapeDataString(rawQueryStringKeyValue.Key);
            return dataString;
        }
    }
}