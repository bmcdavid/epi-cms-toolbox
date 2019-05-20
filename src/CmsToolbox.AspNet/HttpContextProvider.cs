using System.Web;

namespace bmcdavid.Episerver.CmsToolbox
{
    /// <summary>
    /// Safe injectable as singleton accessor to HttpContext
    /// </summary>
    public class HttpContextProvider
    {
        /// <summary>
        /// Returns current HttpContextBase or null
        /// </summary>
        public virtual HttpContextBase CurrentHttpContext => HttpContext.Current is object ? new HttpContextWrapper(HttpContext.Current) : null;
    }
}