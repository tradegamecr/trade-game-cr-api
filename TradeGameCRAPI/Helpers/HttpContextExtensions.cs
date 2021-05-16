using Microsoft.AspNetCore.Http;
using System;

namespace TradeGameCRAPI.Helpers
{
    public static class HttpContextExtensions
    {
        public static void InsertPaginationParams(this HttpContext httpContext, double count, int pageSize)
        {
            double pageCount = Math.Ceiling(count / pageSize);

            httpContext.Response.Headers.Add("Page-Count", pageCount.ToString());
        }
    }
}
