using EPiServer.Core;
using EPiServer.Core.Html.StringParsing;
using EPiServer.DataAbstraction;
using EPiServer.Security;
using EPiServer.Web;
using EPiServer.Web.Mvc.Html;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace bmcdavid.Episerver.CmsToolbox.Rendering
{
    /// <summary>
    /// A flexible content area renderer where a many strategies may be leveraged in PropertyFor and DisplayFor scenarios
    /// </summary>
    public class StrategyBasedContentAreaRenderer : ContentAreaRenderer, IContentItemRenderer
    {
        private readonly TemplateResolver _templateResolver;
        private readonly IDefaultContentStrategy _defaultContentStrategy;
        private IContentAreaItemRenderingStrategy _contentAreaItemRenderingStrategy = null;
        private ITemplateResolverItemStrategy _templateResolverItemStrategy = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="templateRepository"></param>
        /// <param name="defaultContentStrategy"></param>
        public StrategyBasedContentAreaRenderer(TemplateResolver templateRepository, IDefaultContentStrategy defaultContentStrategy)
        {
            _templateResolver = templateRepository;
            _defaultContentStrategy = defaultContentStrategy;
        }

        /// <summary>
        /// Renders content area properties
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="contentArea"></param>
        public override void Render(HtmlHelper htmlHelper, ContentArea contentArea)
        {
            if (contentArea == null || contentArea.IsEmpty) { return; }
            var viewContext = htmlHelper.ViewContext;
            TagBuilder tagBuilder = null;
            _contentAreaItemRenderingStrategy = viewContext.ViewData[nameof(ContentAreaRenderOptions.ContentAreaItemRenderingStrategy)] as IContentAreaItemRenderingStrategy ?? _defaultContentStrategy?.Default;
            _templateResolverItemStrategy = viewContext.ViewData[nameof(ContentAreaRenderOptions.TemplateResolverItemRenderingStrategy)] as ITemplateResolverItemStrategy;

            if (!IsInEditMode(htmlHelper) && ShouldRenderWrappingElement(htmlHelper))
            {
                tagBuilder = new TagBuilder(GetContentAreaHtmlTag(htmlHelper, contentArea));
                AddNonEmptyCssClass(tagBuilder, viewContext.ViewData[nameof(ContentAreaRenderOptions.CssClass)] as string);
                viewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
            }

            var itemsToDisplay = viewContext.ViewData[nameof(ContentAreaRenderOptions.Prefiltered)] as bool? == true ?
                UnFilteredItems(contentArea) :
                FilteredItems(contentArea);

            RenderContentAreaItems(htmlHelper, itemsToDisplay);
            if (tagBuilder == null) { return; }

            viewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.EndTag));
        }

        /// <summary>
        /// Enableds strategy based content area item rendering
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="contentAreaItems"></param>
        protected override void RenderContentAreaItems(HtmlHelper htmlHelper, IEnumerable<ContentAreaItem> contentAreaItems)
        {
            if (_contentAreaItemRenderingStrategy is object)
            {
                _contentAreaItemRenderingStrategy.RenderContentAreaItems(htmlHelper, contentAreaItems, this);

                return;
            }

            base.RenderContentAreaItems(htmlHelper, contentAreaItems);
        }

        /// <summary>
        /// Endables strategy based template resolving
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="content"></param>
        /// <param name="templateTag"></param>
        /// <returns></returns>
        protected override TemplateModel ResolveTemplate(HtmlHelper htmlHelper, IContent content, string templateTag)
        {
            if (_templateResolverItemStrategy is object)
            {
                return _templateResolverItemStrategy.ResolveTemplate(htmlHelper, _templateResolver, content, templateTag);
            }

            return base.ResolveTemplate(htmlHelper, content, templateTag);
        }

        private IEnumerable<ContentAreaItem> FilteredItems(ContentArea contentArea) => contentArea.Fragments
                .GetFilteredFragments(PrincipalInfo.CurrentPrincipal)
                .OfType<ContentFragment>()
                .Select(f => new ContentAreaItem(f));

        private IEnumerable<ContentAreaItem> UnFilteredItems(ContentArea contentArea) => contentArea.Fragments
                .OfType<ContentFragment>()
                .Select(f => new ContentAreaItem(f));

        string IContentItemRenderer.GetContentAreaItemCssClass(HtmlHelper htmlHelper, ContentAreaItem contentAreaItem) =>
            GetContentAreaItemCssClass(htmlHelper, contentAreaItem);

        string IContentItemRenderer.GetContentAreaItemHtmlTag(HtmlHelper htmlHelper, ContentAreaItem contentAreaItem) =>
            GetContentAreaItemHtmlTag(htmlHelper, contentAreaItem);

        string IContentItemRenderer.GetContentAreaItemTemplateTag(HtmlHelper htmlHelper, ContentAreaItem contentAreaItem) => GetContentAreaItemTemplateTag(htmlHelper, contentAreaItem);

        void IContentItemRenderer.RenderContentAreaItem(HtmlHelper htmlHelper, ContentAreaItem contentAreaItem, string templateTag, string htmlTag, string cssClass) =>
            RenderContentAreaItem(htmlHelper, contentAreaItem, templateTag, htmlTag, cssClass);
    }
}