using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;

namespace bmcdavid.Episerver.CmsToolbox
{
    /// <summary>
    /// Registers default toolbox services
    /// </summary>
    [ModuleDependency(typeof(ServiceContainerInitialization))]
    public class CmsToolboxConfigurationModule : IConfigurableModule
    {
        /// <summary>
        /// Configures default toolbox services
        /// </summary>
        /// <param name="context"></param>
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Services
                .AddSingleton<IDateTimeProvider, DefaultDateTimeProvider>()
                .AddSingleton<AncestorPropertyFinder>();
        }

        /// <summary>
        /// init
        /// </summary>
        /// <param name="context"></param>
        public void Initialize(InitializationEngine context)
        {
        }

        /// <summary>
        /// uninit
        /// </summary>
        /// <param name="context"></param>
        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}