using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace bmcdavid.Episerver.CmsToolbox.Tests
{

    [TestClass]
    public class ConfigurationTests
    {
        [TestMethod]
        public void ShouldDisableRendering()
        {
            using (Internal.TestableCmsToolboxConfiguration.Create())
            {
                CmsToolboxConfiguration.Configure(c => c.DisableContentRenderer());

                Assert.IsFalse(CmsToolboxConfiguration.Current.EnableContentRenderer);
            }
        }

        [TestMethod]
        public void ShouldDisableAttrSync()
        {
            using (Internal.TestableCmsToolboxConfiguration.Create())
            {
                CmsToolboxConfiguration.Configure(c => c.DisableAttributeSync());

                Assert.IsFalse(CmsToolboxConfiguration.Current.EnableContentAttributeSynchronization);
            }
        }

        [TestMethod]
        public void ShouldBeEnabledByDefault()
        {
            using (Internal.TestableCmsToolboxConfiguration.Create())
            {
                Assert.IsTrue(CmsToolboxConfiguration.Current.EnableContentAttributeSynchronization, "Failed Attr");
                Assert.IsTrue(CmsToolboxConfiguration.Current.EnableContentRenderer, "Failed render");
            }
        }
    }
}