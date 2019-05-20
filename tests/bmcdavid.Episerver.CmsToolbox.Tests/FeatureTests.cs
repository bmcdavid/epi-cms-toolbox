using EPiServer;
using EPiServer.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace bmcdavid.Episerver.CmsToolbox.Tests
{
    [TestClass]
    public class FeatureTests : BaseTests
    {        
        [TestMethod]
        public void ShouldHaveActiveFeature()
        {
            var locator = GetMockedLocatorProvider(c => c.AddTransient<AlwaysActiveFeature>());

            Assert.IsTrue(FeatureManager.IsActive<AlwaysActiveFeature>(locator));
        }

        [TestMethod]
        public void ShouldHaveDisabledFeature()
        {
            var locator = GetMockedLocatorProvider(c =>
                c.AddTransient(_ => new ConditionalFeature(enabled: false)));

            Assert.IsFalse(FeatureManager.IsActive<ConditionalFeature>(locator));
        }

        [TestMethod]
        public void ShouldHaveInjectedFeature()
        {
            var locator = GetMockedLocatorProvider(c =>
                c.AddSingleton<IContentLoader,MockedContentLoader>()
                .AddTransient<InjectableFeature>());

            Assert.IsTrue(FeatureManager.IsActive<InjectableFeature>(locator));
        }

        public class AlwaysActiveFeature : IFeature
        {
            public bool IsActive() => true;
        }

        public class ConditionalFeature : IFeature
        {
            private readonly bool enabled;

            public ConditionalFeature(bool enabled = true) => this.enabled = enabled;

            public bool IsActive() => enabled;
        }

        public class InjectableFeature : IFeature
        {
            private readonly IContentLoader contentLoader;

            public InjectableFeature(IContentLoader contentLoader) => this.contentLoader = contentLoader;

            public bool IsActive() => contentLoader is object;
        }
    }
}