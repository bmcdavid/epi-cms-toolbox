namespace bmcdavid.Episerver.CmsToolbox.Rendering
{
    /// <summary>
    /// Provides a default strategy to content area renderer
    /// </summary>
    public interface IDefaultContentStrategy
    {
        /// <summary>
        /// Default content item rendering strategy, if null then default behavior is used
        /// </summary>
        IContentAreaItemRenderingStrategy Default { get; }
    }
}