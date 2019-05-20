using EPiServer.Core;
using System.Collections.Generic;
using System.Web.Mvc;

namespace bmcdavid.Episerver.CmsToolbox.Rendering.Strategies
{
    /// <summary>
    /// Wraps content area items in an even/odd dif
    /// </summary>
    public class EvenOddItemRenderingStrategy : IContentAreaItemRenderingStrategy
    {
        /// <summary>
        /// Default implementation with even/odd class names
        /// </summary>
        public static EvenOddItemRenderingStrategy Default = new EvenOddItemRenderingStrategy();

        private readonly string _evenCssClass;
        private readonly string _oddCssClass;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="evenClass"></param>
        /// <param name="oddClass"></param>
        public EvenOddItemRenderingStrategy(string evenClass = "even", string oddClass = "odd")
        {
            _evenCssClass = $" {evenClass} ";
            _oddCssClass = $" {oddClass} ";
        }

        /// <summary>
        /// Renders items
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="contentAreaItems"></param>
        /// <param name="contentItemRenderer"></param>
        public void RenderContentAreaItems(HtmlHelper htmlHelper, IEnumerable<ContentAreaItem> contentAreaItems, IContentItemRenderer contentItemRenderer)
        {
            var index = 0;
            foreach (var contentAreaItem in contentAreaItems)
            {
                var cssClass = (++index) % 2 == 0 ? _evenCssClass : _oddCssClass;
                contentItemRenderer.RenderContentAreaItem(htmlHelper,
                    contentAreaItem,
                    contentItemRenderer.GetContentAreaItemTemplateTag(htmlHelper, contentAreaItem),
                    contentItemRenderer.GetContentAreaItemHtmlTag(htmlHelper, contentAreaItem),
                    contentItemRenderer.GetContentAreaItemCssClass(htmlHelper, contentAreaItem) + cssClass)
                ;
            }
        }
    }
}