using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using EPiServer.Web.Mvc.Html;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace bmcdavid.Episerver.CmsToolbox.Rendering.Strategies
{
    /// <summary>
    /// Strategy to suppress child HTML wrapping classes
    /// </summary>
    public class NoChildContainersStrategy : IContentAreaItemRenderingStrategy
    {
        private readonly IContentRenderer _contentRenderer;
        private readonly IContentAreaItemAttributeAssembler _itemAttributeAssembler;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="itemAttributeAssembler"></param>
        /// <param name="contentRenderer"></param>
        public NoChildContainersStrategy
        (
            IContentAreaItemAttributeAssembler itemAttributeAssembler = null,
            IContentRenderer contentRenderer = null
        )
        {
            _itemAttributeAssembler = itemAttributeAssembler ??
                ServiceLocator.Current.GetInstance<IContentAreaItemAttributeAssembler>();
            _contentRenderer = contentRenderer ??
                ServiceLocator.Current.GetInstance<IContentRenderer>();
        }

        /// <summary>
        /// Renders items
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="contentAreaItems"></param>
        /// <param name="contentItemRenderer"></param>
        public void RenderContentAreaItems(HtmlHelper htmlHelper, IEnumerable<ContentAreaItem> contentAreaItems, IContentItemRenderer contentItemRenderer)
        {
            foreach (var item in contentAreaItems)
            {
                CustomRenderContentAreaItem
                (
                    htmlHelper,
                    item,
                    contentItemRenderer
                );
            }
        }

        private void CustomRenderContentAreaItem
        (
            HtmlHelper htmlHelper,
            ContentAreaItem item,
            IContentItemRenderer renderer
        )
        {
            var content = item.GetContent();
            if (content is null) { return; }

            var templateTag = renderer.GetContentAreaItemTemplateTag(htmlHelper, item);
            var cssClass = renderer.GetContentAreaItemCssClass(htmlHelper, item);
            var htmlTag = renderer.GetContentAreaItemHtmlTag(htmlHelper, item);
            var renderSettings = new Dictionary<string, object>
            {
                ["childrencustomtagname"] = htmlTag,
                ["childrencssclass"] = cssClass,
                ["tag"] = templateTag
            };

            renderSettings = item.RenderSettings.Concat(
                from r in renderSettings
                where !item.RenderSettings.ContainsKey(r.Key)
                select r).ToDictionary(r => r.Key, r => r.Value);

            htmlHelper.ViewData["RenderSettings"] = renderSettings;
            bool isInEditMode = renderer.IsInEditMode(htmlHelper);

            using (new ContentAreaContext(htmlHelper.ViewContext.RequestContext, content.ContentLink))
            {
                var templateModel = renderer.ResolveTemplate(htmlHelper, content, templateTag);
                if (templateModel is null || !isInEditMode) { return; }

                if (!isInEditMode)
                {
                    htmlHelper.RenderContentData(content, true, templateModel, _contentRenderer);
                }

                var tagBuilder = new TagBuilder(htmlTag);
                renderer.AddNonEmptyCssClass(tagBuilder, cssClass);
                tagBuilder.MergeAttributes
                (_itemAttributeAssembler.GetAttributes(
                    item,
                    isInEditMode,
                    templateModel is object
                ));
                renderer.BeforeRenderContentAreaItemStartTag(tagBuilder, item);
                htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
                htmlHelper.RenderContentData(content, true, templateModel, _contentRenderer);
                htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.EndTag));
            }
        }
    }
}