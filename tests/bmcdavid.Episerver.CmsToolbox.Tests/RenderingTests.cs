using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace bmcdavid.Episerver.CmsToolbox.Tests
{
    [TestClass]
    public class RenderingTests : BaseTests
    {
        [TestMethod]
        public void ShouldTransformBlockToViewModel()
        {
            var viewmodel = new SampleBlock().BuildRendering<SampleBlock, SampleViewmodel>(GetMockedLocatorProvider(c => c.AddSingleton(typeof(IContentLoader), new MockedContentLoader())));

            Assert.IsTrue(viewmodel.ContentLoader is MockedContentLoader);
        }
        
        public class SampleBlock : BlockData
        {
            public virtual string Title { get; set; }
        }

        public class SampleViewmodel : IRendering<SampleBlock>
        {
            public SampleViewmodel(SampleBlock currentBlock, IContentLoader contentLoader)
            {
                ContentData = currentBlock;
                ContentLoader = contentLoader;
                AsContent = currentBlock as IContent;
                IsIContent = AsContent != null;
            }

            public IContent AsContent { get; }
            public SampleBlock ContentData { get; }
            IContentData IRendering.ContentData => ContentData;
            public IContentLoader ContentLoader { get; }
            public bool IsIContent { get; }
        }
    }
}