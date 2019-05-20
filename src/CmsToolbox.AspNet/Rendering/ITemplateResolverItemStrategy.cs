using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Web;
using System.Web.Mvc;

namespace bmcdavid.Episerver.CmsToolbox.Rendering
{
    /// <summary>
    /// Provides option based template resolving
    /// </summary>
    public interface ITemplateResolverItemStrategy
    {
        /// <summary>
        /// Selections template from given arguments
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="templateResolver"></param>
        /// <param name="content"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        TemplateModel ResolveTemplate(HtmlHelper htmlHelper, ITemplateResolver templateResolver, IContent content, string tag);
    }
}