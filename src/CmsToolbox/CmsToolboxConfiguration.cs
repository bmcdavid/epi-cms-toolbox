using System;

namespace bmcdavid.Episerver.CmsToolbox
{
    /// <summary>
    /// Code configuration class with callback to disable strategy based content rendering and attribute synchronization
    /// </summary>
    public class CmsToolboxConfiguration
    {
        private static Action<CmsToolboxConfiguration> _configurationCallback;
        private static CmsToolboxConfiguration _current;
        private bool _configured;

        private CmsToolboxConfiguration()
        {
            EnableContentAttributeSynchronization = true;
            EnableContentRenderer = true;
            _configured = false;
        }

        /// <summary>
        /// Current configuration, please assign callback before accessing.
        /// </summary>
        public static CmsToolboxConfiguration Current
        {
            get
            {
                if (_current is object) { return _current; }
                _current = new CmsToolboxConfiguration();
                _configurationCallback?.Invoke(_current);
                _current.SetConfigured();
                return _current;
            }
        }

        /// <summary>
        /// Default is true to synch content attributes
        /// </summary>
        public bool EnableContentAttributeSynchronization { get; private set; }

        /// <summary>
        /// Default is true to enable content renderer
        /// </summary>
        public bool EnableContentRenderer { get; private set; }

        /// <summary>
        /// Callback for customizations
        /// </summary>
        /// <param name="configure"></param>
        public static void Configure(Action<CmsToolboxConfiguration> configure) =>
            _configurationCallback = configure ?? throw new ArgumentNullException(nameof(configure));

        /// <summary>
        /// Disables Attribute Sync
        /// </summary>
        /// <returns></returns>
        public CmsToolboxConfiguration DisableAttributeSync() => RunAction(() => EnableContentAttributeSynchronization = false);

        /// <summary>
        /// Disables Content Renderer
        /// </summary>
        /// <returns></returns>
        public CmsToolboxConfiguration DisableContentRenderer() => RunAction(() => EnableContentRenderer = false);

        internal static void ResetCurrent()
        {
            _current = null;
            _configurationCallback = null;
        }

        internal void SetConfigured() => _configured = true;

        private CmsToolboxConfiguration RunAction(Action action)
        {
            if (_configured) { return this; }
            action.Invoke();
            return this;
        }
    }
}