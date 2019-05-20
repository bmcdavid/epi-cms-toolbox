using bmcdavid.Episerver.CmsToolbox.Rendering;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc.Html;

namespace bmcdavid.Episerver.CmsToolbox
{
    /// <summary>
    /// Episerver module to configure services
    /// </summary>
    [InitializableModule]
    [ModuleDependency(typeof(ServiceContainerInitialization))]
    public class CmsToolboxAspNetConfigurationModule : IConfigurableModule
    {
        /// <summary>
        /// Configures container
        /// </summary>
        /// <param name="context"></param>
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            if (CmsToolboxConfiguration.Current.EnableContentRenderer)
            {
                context.Services.AddTransient<ContentAreaRenderer, StrategyBasedContentAreaRenderer>();
            }

            context.Services
                .AddSingleton<IEpiserverEnvironment, AspNetEpiserverEnvironment>()
                .AddSingleton<IDefaultContentStrategy, DefaultContentStrategy>()
                .AddSingleton<HttpContextProvider>();
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="context"></param>
        public void Initialize(InitializationEngine context)
        {
        }

        /// <summary>
        /// Uninit
        /// </summary>
        /// <param name="context"></param>
        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}