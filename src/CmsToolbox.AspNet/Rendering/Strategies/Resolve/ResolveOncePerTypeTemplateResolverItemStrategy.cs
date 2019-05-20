using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Framework.Web;
using EPiServer.Web;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace bmcdavid.Episerver.CmsToolbox.Rendering.Strategies.Resolve
{
    /// <summary>
    /// Resolves templates once per type
    /// </summary>
    public class ResolveOncePerTypeTemplateResolverItemStrategy : ITemplateResolverItemStrategy
    {
        private readonly Dictionary<Type, TemplateModel> _cachedTemplates = new Dictionary<Type, TemplateModel>();

        /// <summary>
        /// Resolves templates once per type
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="templateResolver"></param>
        /// <param name="content"></param>
        /// <param name="templateTag"></param>
        /// <returns></returns>
        public TemplateModel ResolveTemplate(HtmlHelper htmlHelper, ITemplateResolver templateResolver, IContent content, string templateTag)
        {
            if (!_cachedTemplates.TryGetValue(content.GetOriginalType(), out var template))
            {
                template = templateResolver.Resolve
                (
                    content,
                    content.GetOriginalType(),
                    TemplateTypeCategories.MvcPartialView,
                    new string[] { templateTag }
                );
            }

            return template;
        }
    }
}