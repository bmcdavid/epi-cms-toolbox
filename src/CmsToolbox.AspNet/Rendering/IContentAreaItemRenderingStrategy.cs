using EPiServer.Core;
using System.Collections.Generic;
using System.Web.Mvc;

namespace bmcdavid.Episerver.CmsToolbox.Rendering
{
    /// <summary>
    /// Allows for flexible content area item rendering passed via options
    /// </summary>
    public interface IContentAreaItemRenderingStrategy
    {
        /// <summary>
        /// Allows custom content item rendering
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="contentAreaItems"></param>
        /// <param name="contentRenderItemRenderer"></param>
        void RenderContentAreaItems
        (
            HtmlHelper htmlHelper,
            IEnumerable<ContentAreaItem> contentAreaItems,
            IContentItemRenderer contentRenderItemRenderer
        );
    }
}