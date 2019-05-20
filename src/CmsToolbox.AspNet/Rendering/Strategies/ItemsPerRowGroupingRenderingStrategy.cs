using EPiServer.Core;
using System.Collections.Generic;
using System.Web.Mvc;

namespace bmcdavid.Episerver.CmsToolbox.Rendering.Strategies
{
    /// <summary>
    /// Items per row strategy
    /// </summary>
    public class ItemsPerRowGroupingRenderingStrategy : IContentAreaItemRenderingStrategy
    {
        private TagBuilder _currentContainer;
        private readonly int _itemsPerRow;
        private readonly string _wrappingCssClass;
        private readonly string _wrappingHtmlTag;
        private bool _isContainerOpen;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="itemsPerRow"></param>
        /// <param name="wrappingHtmlTag"></param>
        /// <param name="wrappingCssClass"></param>
        public ItemsPerRowGroupingRenderingStrategy(int itemsPerRow = 3, string wrappingHtmlTag = "div", string wrappingCssClass = "row-group-list")
        {
            _itemsPerRow = itemsPerRow;
            _wrappingHtmlTag = wrappingHtmlTag;
            _wrappingCssClass = wrappingCssClass;
        }

        /// <summary>
        /// Renders items
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="contentAreaItems"></param>
        /// <param name="contentItemRenderer"></param>
        public void RenderContentAreaItems(HtmlHelper htmlHelper, IEnumerable<ContentAreaItem> contentAreaItems, IContentItemRenderer contentItemRenderer)
        {
            _isContainerOpen = false;
            var index = 0;

            using (var iter = contentAreaItems.GetEnumerator())
            {
                while (iter.MoveNext())
                {
                    var contentAreaItem = iter.Current;
                    if (CanOpenContainer(index)) { StartContainer(htmlHelper); }
                    index++;

                    contentItemRenderer.RenderContentAreaItem
                    (
                        htmlHelper,
                        contentAreaItem,
                        contentItemRenderer.GetContentAreaItemTemplateTag(htmlHelper, contentAreaItem),
                        contentItemRenderer.GetContentAreaItemHtmlTag(htmlHelper, contentAreaItem),
                        contentItemRenderer.GetContentAreaItemCssClass(htmlHelper, contentAreaItem)
                    );

                    if (CanCloseContainer(index)) { EndContainer(htmlHelper); }
                }

                EndContainer(htmlHelper); // last item
            }
        }

        private bool CanCloseContainer(int index) => _isContainerOpen ? CanOpenContainer(index) : false;

        private bool CanOpenContainer(int index) => (index % _itemsPerRow == 0);

        private void EndContainer(HtmlHelper html)
        {
            if (_isContainerOpen)
            {
                html.ViewContext.Writer.Write(_currentContainer.ToString(TagRenderMode.EndTag));
                _isContainerOpen = false;
            }
        }

        private void StartContainer(HtmlHelper html)
        {
            _currentContainer = new TagBuilder(_wrappingHtmlTag);
            _currentContainer.AddCssClass(_wrappingCssClass);
            html.ViewContext.Writer.Write(_currentContainer.ToString(TagRenderMode.StartTag));
            _isContainerOpen = true;
        }
    }
}