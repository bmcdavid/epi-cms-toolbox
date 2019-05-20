using EPiServer;
using EPiServer.Core;
using System;
using System.Linq;

namespace bmcdavid.Episerver.CmsToolbox
{
    /// <summary>
    /// Uses page tree to locate parent IContent where criteria condition is true
    /// </summary>
    public class AncestorPropertyFinder
    {
        private readonly IContentLoader _contentLoader;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contentLoader"></param>
        public AncestorPropertyFinder(IContentLoader contentLoader) => _contentLoader = contentLoader;

        /// <summary>
        /// First first match or null for given criteria.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="criteria"></param>
        /// <param name="startingContent"></param>
        /// <returns></returns>
        public virtual T FindFirst<T>(Func<T, bool> criteria, ContentReference startingContent) where T : class
        {
            if (criteria == null) { throw new ArgumentNullException(nameof(criteria)); }
            if (startingContent == null) { throw new ArgumentNullException(nameof(startingContent)); }

            var matchingAncestor = _contentLoader.GetAncestors(startingContent)
                .FirstOrDefault(f =>
                {
                    if (!(f is T matchingInstance)) { return false; }
                    if (criteria(matchingInstance)) { return true; }
                    return f.ParentLink == ContentReference.RootPage;
                });

            return matchingAncestor != null ? (T)matchingAncestor : null;
        }
    }
}