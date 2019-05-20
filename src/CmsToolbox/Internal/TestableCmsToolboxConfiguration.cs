using System;

namespace bmcdavid.Episerver.CmsToolbox.Internal
{
    /// <summary>
    /// INTERNAL: Use for testing purposes only
    /// </summary>
    public class TestableCmsToolboxConfiguration : IDisposable
    {
        /// <summary>
        /// Creates a toolbox configuration operation that is cleared when disposed
        /// </summary>
        /// <returns></returns>
        public static TestableCmsToolboxConfiguration Create() => new TestableCmsToolboxConfiguration();

        private TestableCmsToolboxConfiguration() => CmsToolboxConfiguration.ResetCurrent();

        void IDisposable.Dispose() => CmsToolboxConfiguration.ResetCurrent();
    }
}