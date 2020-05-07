using System;
using System.Data.Entity;
using System.Web;

namespace Cupcake.Data
{
    public static class ContextHelper<T> where T : DbContext, new()
    {
        private const string ObjectContextKey = "ObjectContext";

        public static T GetCurrentContext()
        {
            HttpContext httpContext = HttpContext.Current;
            if (httpContext != null)
            {
                string contextTypeKey = ObjectContextKey + typeof(T).Name;
                if (httpContext.Items[contextTypeKey] == null)
                {
                    httpContext.Items.Add(contextTypeKey, new T());
                }
                return httpContext.Items[contextTypeKey] as T;
            }
            throw new ApplicationException("There is no Http Context available");
        }
    }
}
