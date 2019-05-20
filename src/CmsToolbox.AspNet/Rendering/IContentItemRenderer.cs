using EPiServer.Core;
using System.Web.Mvc;

namespace bmcdavid.Episerver.CmsToolbox.Rendering
{
    /// <summary>
    /// Provides strategies access to content area rendering functions
    /// </summary>
    public interface IContentItemRenderer
    {
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
        /// Renders content area item
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="contentAreaItem"></param>
        /// <param name="templateTag"></param>
        /// <param name="htmlTag"></param>
        /// <param name="cssClass"></param>
        void RenderContentAreaItem(HtmlHelper htmlHelper, ContentAreaItem contentAreaItem, string templateTag, string htmlTag, string cssClass);
    }
}