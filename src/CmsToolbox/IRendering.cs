using EPiServer.Core;

namespace bmcdavid.Episerver.CmsToolbox
{
    /// <summary>
    /// Allows for simple rich view models from IContentData (BlockData)
    /// </summary>
    public interface IRendering
    {
        /// <summary>
        /// ContentData instance
        /// </summary>
        IContentData ContentData { get; }

        /// <summary>
        /// ContentData as IContent, may be null, check IsIContent first
        /// </summary>
        IContent AsContent { get; }

        /// <summary>
        /// Determines if IContentData is also IContent
        /// </summary>
        bool IsIContent { get; }
    }

    /// <summary>
    /// Generic typed IRendering
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRendering<T> : IRendering where T : IContentData
    {
        /// <summary>
        /// T instance
        /// </summary>
        new T ContentData { get; }
    }
}