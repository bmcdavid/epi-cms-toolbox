using EPiServer.Core;
using System.Collections.Generic;
using System.Web.Mvc;

namespace bmcdavid.Episerver.CmsToolbox.Rendering.Strategies
{
    /// <summary>
    /// Adds a first/last css class to content area items
    /// </summary>
    public class FirstLastItemRenderingStrategy : IContentAreaItemRenderingStrategy
    {
        /// <summary>
        /// Default First/Last strategy
        /// </summary>
        public static FirstLastItemRenderingStrategy Default = new FirstLastItemRenderingStrategy();

        private readonly string _firstCssClass;
        private readonly string _lastCssClass;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="firstCssClass"></param>
        /// <param name="lastCssClass"></param>
        public FirstLastItemRenderingStrategy(string firstCssClass = "first", string lastCssClass = "last")
        {
            _firstCssClass = $" {firstCssClass}";
            _lastCssClass = $" {lastCssClass}";
        }

        /// <summary>
        /// Renders items
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="contentAreaItems"></param>
        /// <param name="contentItemRenderer"></param>
        public void RenderContentAreaItems(HtmlHelper htmlHelper, IEnumerable<ContentAreaItem> contentAreaItems, IContentItemRenderer contentItemRenderer)
        {
            using (var iter = contentAreaItems.GetEnumerator())
            {
                var isFirst = true;

                if (iter.MoveNext())
                {
                    var renderItem = iter.Current;

                    do
                    {
                        var cssClass = string.Empty;

                        if (isFirst)
                        {
                            cssClass = _firstCssClass;
                            isFirst = false;
                        }

                        var currentItem = renderItem;
                        renderItem = iter.MoveNext() ? iter.Current : null;
                        if (renderItem == null) { cssClass = _lastCssClass; }

                        contentItemRenderer.RenderContentAreaItem
                        (
                            htmlHelper,
                            currentItem,
                            contentItemRenderer.GetContentAreaItemTemplateTag(htmlHelper, currentItem),
                            contentItemRenderer.GetContentAreaItemHtmlTag(htmlHelper, currentItem),
                            contentItemRenderer.GetContentAreaItemCssClass(htmlHelper, currentItem) + cssClass
                        );
                    } while (renderItem != null);
                }
            }
        }
    }
}