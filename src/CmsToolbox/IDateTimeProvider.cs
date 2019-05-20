using System;

namespace bmcdavid.Episerver.CmsToolbox
{
    /// <summary>
    /// Best practice, mockable datetime provider
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Current system time
        /// </summary>
        DateTime Now { get; }

        /// <summary>
        /// Current system UTC time
        /// </summary>
        DateTime UtcNow { get; }
    }
}