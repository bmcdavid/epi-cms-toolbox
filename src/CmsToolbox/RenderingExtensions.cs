using EPiServer.Core;
using EPiServer.ServiceLocation;
using System;

namespace bmcdavid.Episerver.CmsToolbox
{
    /// <summary>
    /// Rendering Extensions
    /// </summary>
    public static class RenderingExtensions
    {
        /// <summary>
        /// Simple converter for IContentData blocks to rich view models with need the use of a Controller
        /// </summary>
        /// <typeparam name="TContentData"></typeparam>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="contentData"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static TViewModel BuildRendering<TContentData, TViewModel>
        (
            this TContentData contentData,
            IServiceProvider serviceProvider = null
        )
            where TContentData : IContentData
            where TViewModel : IRendering<TContentData>
        {
            if (contentData == null) { throw new ArgumentNullException(nameof(contentData)); }
            serviceProvider = serviceProvider ?? ServiceLocator.Current;
            var factory = serviceProvider.GetService(typeof(Func<TContentData, TViewModel>)) as Func<TContentData, TViewModel>;

            return factory.Invoke(contentData);
        }
    }
}