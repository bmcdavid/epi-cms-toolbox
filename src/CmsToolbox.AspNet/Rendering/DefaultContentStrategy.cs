namespace bmcdavid.Episerver.CmsToolbox.Rendering
{
    /// <summary>
    /// Default implementation of default strategy
    /// </summary>
    public class DefaultContentStrategy : IDefaultContentStrategy
    {
        /// <summary>
        /// Empty/null is provideded by default
        /// </summary>
        public IContentAreaItemRenderingStrategy Default => null;
    }
}