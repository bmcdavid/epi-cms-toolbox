using EPiServer;
using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace bmcdavid.Episerver.CmsToolbox.Rendering.Strategies
{
    /// <summary>
    /// Rendering strategy based on Alloy demo site example
    /// </summary>
    public class AlloyRenderingStrategy : IContentAreaItemRenderingStrategy
    {
        private readonly Func<IContent, string> _contentCssClass;

        /// <summary>
        /// Constructor, can use ICustomCssClass interface to provide extra options
        /// </summary>
        /// <param name="contentCssClass"></param>
        public AlloyRenderingStrategy(Func<IContent, string> contentCssClass = null) => _contentCssClass = contentCssClass;

        /// <summary>
        /// Render items
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="contentAreaItems"></param>
        /// <param name="contentItemRenderer"></param>
        public void RenderContentAreaItems(HtmlHelper htmlHelper, IEnumerable<ContentAreaItem> contentAreaItems, IContentItemRenderer contentItemRenderer)
        {
            foreach (var contentAreaItem in contentAreaItems)
            {
                var templateTag = contentItemRenderer.GetContentAreaItemTemplateTag(htmlHelper, contentAreaItem);
                contentItemRenderer.RenderContentAreaItem
                (
                    htmlHelper,
                    contentAreaItem,
                    templateTag,
                    contentItemRenderer.GetContentAreaItemHtmlTag(htmlHelper, contentAreaItem),
                    contentItemRenderer.GetContentAreaItemCssClass(htmlHelper, contentAreaItem) + GetAlloyCssClass(contentAreaItem, templateTag));
            }
        }

        /// <summary>
        /// Gets a CSS class used for styling based on a tag name (ie a Bootstrap class name)
        /// </summary>
        /// <param name="tagName">Any tag name available</param>
        private static string GetCssClassForTag(string tagName)
        {
            if (string.IsNullOrEmpty(tagName)) { return string.Empty; }
            switch (tagName.ToLower())
            {
                case "span12":
                    return "full";
                case "span8":
                    return "wide";
                case "span6":
                    return "half";
                default:
                    return string.Empty;
            }
        }

        private string GetAlloyCssClass(ContentAreaItem contentAreaItem, string tag) =>
            $" block {tag} {GetTypeSpecificCssClasses(contentAreaItem)} {GetCssClassForTag(tag)}";

        private string GetTypeSpecificCssClasses(ContentAreaItem contentAreaItem)
        {
            var content = contentAreaItem.GetContent();
            var cssClass = content == null ? string.Empty : content.GetOriginalType().Name.ToLowerInvariant();
            var customContentCss = _contentCssClass?.Invoke(content);

            if (!string.IsNullOrWhiteSpace(customContentCss))
            {
                cssClass += string.Format(" {0}", customContentCss);
            }

            return cssClass;
        }
    }
}
