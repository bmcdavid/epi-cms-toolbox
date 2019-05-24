using EPiServer.Core;
using System.Collections.Generic;
using System.Linq;

namespace bmcdavid.Episerver.CmsToolbox
{
    /// <summary>
    /// Content Extensions
    /// </summary>
    public static class ContentExtensions
    {
        /// <summary>
        /// Converts enumerable of ContentReference to ContentArea
        /// </summary>
        /// <param name="contentReferences"></param>
        /// <returns></returns>
        public static ContentArea ToContentArea(this IEnumerable<ContentReference> contentReferences)
        {
            if (contentReferences?.Any() != true) { return null; }

            var contentArea = new ContentArea();
            contentReferences.All(c =>
            {
                contentArea.Items.Add(new ContentAreaItem { ContentLink = c });
                return true;
            });

            return contentArea;
        }

        /// <summary>
        /// Converts enumerable of IContent to contentarea
        /// </summary>
        /// <param name="contentItems"></param>
        /// <returns></returns>
        public static ContentArea ToContentArea(this IEnumerable<IContent> contentItems)
        {
            if (contentItems?.Any() != true) { return null; }

            var contentArea = new ContentArea();
            contentItems.All(c =>
            {
                contentArea.Items.Add(new ContentAreaItem
                {
                    ContentLink = c.ContentLink,
                    ContentGuid = c.ContentGuid
                });
                return true;
            });

            return contentArea;
        }
    }
}
