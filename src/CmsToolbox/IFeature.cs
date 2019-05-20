namespace bmcdavid.Episerver.CmsToolbox
{
    /// <summary>
    /// Simple Feature Flag
    /// </summary>
    public interface IFeature
    {
        /// <summary>
        /// Determines if feature is active
        /// </summary>
        /// <returns></returns>
        bool IsActive();
    }
}