using EPiServer.DataAbstraction.RuntimeModel;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Initialization;
using EPiServer.ServiceLocation;

namespace bmcdavid.Episerver.CmsToolbox
{
    /// <summary>
    /// Applies decorated IContentTypeModelAssigner to use SyncAttributes for type synchronization
    /// </summary>
    [InitializableModule]
    [ModuleDependency(typeof(CmsCoreInitialization))]
    public class ModelSyncInitialization : IConfigurableModule
    {
        /// <summary>
        /// Enables attribute synching from additional models
        /// </summary>
        /// <param name="context"></param>
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            if (CmsToolboxConfiguration.Current.EnableContentAttributeSynchronization)
            {
                context.ConfigurationComplete += (o, e) =>
                {
                    e.Services.Intercept<IContentTypeModelAssigner>((_, defaultAssigner) =>
                        new AttributeProviderContentTypeModelAssigner(defaultAssigner));
                };
            }
        }

        /// <inheritdoc/>
        public void Initialize(InitializationEngine context)
        {
        }

        /// <inheritdoc/>
        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}