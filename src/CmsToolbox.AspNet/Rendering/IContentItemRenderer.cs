using EPiServer.Core;
using EPiServer.DataAbstraction;
using System.Web.Mvc;

namespace bmcdavid.Episerver.CmsToolbox.Rendering
{
    /// <summary>
    /// Provides strategies access to content area rendering functions
    /// </summary>
    public interface IContentItemRenderer
    {
        /// <summary>
        /// Adds non empty css classes
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="cssClass"></param>
        /// <returns></returns>
        TagBuilder AddNonEmptyCssClass(TagBuilder tagBuilder, string cssClass);

        /// <summary>
        /// Executes before rendering item
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <param name="contentAreaItem"></param>
        void BeforeRenderContentAreaItemStartTag(TagBuilder tagBuilder, ContentAreaItem contentAreaItem);

        /// <summary>
        /// Gets css class for content area item
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="contentAreaItem"></param>
        /// <returns></returns>
        string GetContentAreaItemCssClass(HtmlHelper htmlHelper, ContentAreaItem contentAreaItem);

        /// <summary>
        /// Gets html tag of content area item
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="contentAreaItem"></param>
        /// <returns></returns>
        string GetContentAreaItemHtmlTag(HtmlHelper htmlHelper, ContentAreaItem contentAreaItem);

        /// <summary>
        /// Gets the template tag of content area item
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="contentAreaItem"></param>
        /// <returns></returns>
        string GetContentAreaItemTemplateTag(HtmlHelper htmlHelper, ContentAreaItem contentAreaItem);

        /// <summary>
        /// Determines if edit mode is enabled
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        bool IsInEditMode(HtmlHelper htmlHelper);

        /// <summary>
        /// Renders content area item
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="contentAreaItem"></param>
        /// <param name="templateTag"></param>
        /// <param name="htmlTag"></param>
        /// <param name="cssClass"></param>
        void RenderContentAreaItem(HtmlHelper htmlHelper, ContentAreaItem contentAreaItem, string templateTag, string htmlTag, string cssClass);

        /// <summary>
        /// Resolves a template for given content and tag
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="content"></param>
        /// <param name="templateTag"></param>
        /// <returns></returns>
        TemplateModel ResolveTemplate(HtmlHelper htmlHelper, IContent content, string templateTag);
    }
}