namespace bmcdavid.Episerver.CmsToolbox.Rendering
{
    /// <summary>
    /// Content area render options
    /// </summary>
    public class ContentAreaRenderOptions
    {
        /// <summary>
        /// Css class applied to each child item displayed
        /// </summary>
        public string ChildrenCssClass { get; set; }

        /// <summary>
        /// Html Tag for each child item, default is div
        /// </summary>
        public string ChildrenCustomTagName { get; set; }

        /// <summary>
        /// Css class for wrapping element
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Custom HTML tag for wrapping element
        /// </summary>
        public string CustomTag { get; set; }

        /// <summary>
        /// If false, disables the wrapping HTML element
        /// </summary>
        public bool HasContainer { get; set; }

        /// <summary>
        /// If true, disables the filtering of elements, only use if list has been filtered by other means.
        /// </summary>
        public bool Prefiltered { get; set; }

        /// <summary>
        /// Episerver display tag to find a display template for each content item
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Optional strategy for determining how content area items are rendered.
        /// <para>Example strategies are even/odd CssClass wrappings or group N items per HTML tag</para>
        /// </summary>
        public IContentAreaItemRenderingStrategy ContentAreaItemRenderingStrategy { get; set; }

        /// <summary>
        /// Optional strategy for resolving tags
        /// </summary>
        public ITemplateResolverItemStrategy TemplateResolverItemRenderingStrategy { get; set; }
    }
}