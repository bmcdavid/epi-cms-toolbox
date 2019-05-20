using EPiServer.ServiceLocation;
using System;
using System.Linq;

namespace bmcdavid.Episerver.CmsToolbox
{
    /// <summary>
    /// Used to check if an IFeature is enabled
    /// </summary>
    public class FeatureManager
    {
        private static readonly FeatureManager _defaultFeatureManagerInstance = new FeatureManager();

        /// <summary>
        /// Constructor
        /// </summary>
        protected FeatureManager() { }
        
        /// <summary>
        /// Determines if given feature T is active for the set FeatureManager
        /// </summary>
        /// <typeparam name="TFeature"></typeparam>
        /// <returns></returns>
        public static bool IsActive<TFeature>(IServiceLocator serviceLocator = null) where TFeature : IFeature
        {
            var featureInstance = _defaultFeatureManagerInstance.GetFeature(typeof(TFeature), serviceLocator);

            return featureInstance?.IsActive() ?? false;
        }

        /// <summary>
        /// Determines if feature is active
        /// </summary>
        /// <param name="tFeature"></param>
        /// <param name="serviceLocator"></param>
        /// <returns></returns>
        public virtual IFeature GetFeature(Type tFeature, IServiceLocator serviceLocator = null)
        {
            var locator = ResolveLocator(serviceLocator);

            if (!locator.TryGetExistingInstance(tFeature, out var feature)) // concrete check
            {
                feature = locator.GetAllInstances<IFeature>().FirstOrDefault(t => t.GetType() == tFeature); // fallback to interface check
            }

            return feature as IFeature;
        }

        /// <summary>
        /// Resovles a service locator or defaults to Current
        /// </summary>
        /// <param name="serviceLocator"></param>
        /// <returns></returns>
        protected virtual IServiceLocator ResolveLocator(IServiceLocator serviceLocator) => serviceLocator ?? ServiceLocator.Current;
    }
}