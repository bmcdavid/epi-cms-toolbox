using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace bmcdavid.Episerver.CmsToolbox.Tests
{
    public class BaseTests
    {
        protected IServiceLocator GetMockedLocatorProvider(Action<IServiceConfigurationProvider> configure)
        {
            var factory = new StructureMapServiceLocatorFactory();
            var provider = factory.CreateProvider();
            configure.Invoke(provider);

            return factory.CreateLocator();
        }

        public class MockedContentLoader : IContentLoader
        {
            public T Get<T>(Guid contentGuid) where T : IContentData
            {
                throw new NotImplementedException();
            }

            public T Get<T>(Guid contentGuid, LoaderOptions settings) where T : IContentData
            {
                throw new NotImplementedException();
            }

            public T Get<T>(Guid contentGuid, CultureInfo language) where T : IContentData
            {
                throw new NotImplementedException();
            }

            public T Get<T>(ContentReference contentLink) where T : IContentData
            {
                throw new NotImplementedException();
            }

            public T Get<T>(ContentReference contentLink, CultureInfo language) where T : IContentData
            {
                throw new NotImplementedException();
            }

            public T Get<T>(ContentReference contentLink, LoaderOptions settings) where T : IContentData
            {
                throw new NotImplementedException();
            }

            public IEnumerable<IContent> GetAncestors(ContentReference contentLink)
            {
                throw new NotImplementedException();
            }

            public IContent GetBySegment(ContentReference parentLink, string urlSegment, CultureInfo language)
            {
                throw new NotImplementedException();
            }

            public IContent GetBySegment(ContentReference parentLink, string urlSegment, LoaderOptions settings)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<T> GetChildren<T>(ContentReference contentLink) where T : IContentData
            {
                throw new NotImplementedException();
            }

            public IEnumerable<T> GetChildren<T>(ContentReference contentLink, CultureInfo language) where T : IContentData
            {
                throw new NotImplementedException();
            }

            public IEnumerable<T> GetChildren<T>(ContentReference contentLink, LoaderOptions settings) where T : IContentData
            {
                throw new NotImplementedException();
            }

            public IEnumerable<T> GetChildren<T>(ContentReference contentLink, CultureInfo language, int startIndex, int maxRows) where T : IContentData
            {
                throw new NotImplementedException();
            }

            public IEnumerable<T> GetChildren<T>(ContentReference contentLink, LoaderOptions settings, int startIndex, int maxRows) where T : IContentData
            {
                throw new NotImplementedException();
            }

            public IEnumerable<ContentReference> GetDescendents(ContentReference contentLink)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<IContent> GetItems(IEnumerable<ContentReference> contentLinks, CultureInfo language)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<IContent> GetItems(IEnumerable<ContentReference> contentLinks, LoaderOptions settings)
            {
                throw new NotImplementedException();
            }

            public bool TryGet<T>(ContentReference contentLink, out T content) where T : IContentData
            {
                throw new NotImplementedException();
            }

            public bool TryGet<T>(ContentReference contentLink, CultureInfo language, out T content) where T : IContentData
            {
                throw new NotImplementedException();
            }

            public bool TryGet<T>(ContentReference contentLink, LoaderOptions settings, out T content) where T : IContentData
            {
                throw new NotImplementedException();
            }

            public bool TryGet<T>(Guid contentGuid, out T content) where T : IContentData
            {
                throw new NotImplementedException();
            }

            public bool TryGet<T>(Guid contentGuid, CultureInfo language, out T content) where T : IContentData
            {
                throw new NotImplementedException();
            }

            public bool TryGet<T>(Guid contentGuid, LoaderOptions loaderOptions, out T content) where T : IContentData
            {
                throw new NotImplementedException();
            }
        }


    }
}