using System;

namespace bmcdavid.Episerver.CmsToolbox
{
    /// <inheritdoc/>
    public class DefaultDateTimeProvider : IDateTimeProvider
    {
        /// <inheritdoc/>
        public DateTime Now => DateTime.Now;

        /// <inheritdoc/>
        public DateTime UtcNow => DateTime.UtcNow;
    }
}