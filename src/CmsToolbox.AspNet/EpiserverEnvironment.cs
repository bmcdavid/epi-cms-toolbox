using System.Configuration;

namespace bmcdavid.Episerver.CmsToolbox
{
    /// <summary>
    /// Default AspNet Framework implementation
    /// </summary>
    public class AspNetEpiserverEnvironment : IEpiserverEnvironment
    {
        /// <summary>
        /// Default DXC integration value
        /// </summary>
        public const string Integration = "Integration";

        /// <summary>
        /// Default value for local work
        /// </summary>
        public const string Local = "Local";

        /// <summary>
        /// Default DXC preproduction value
        /// </summary>
        public const string Preproduction = "Preproduction";

        /// <summary>
        /// Default DXC production value
        /// </summary>
        public const string Production = "Production";

        /// <summary>
        /// Default unit test value
        /// </summary>
        public const string UnitTest = "UnitTest";

        /// <summary>
        /// Default constructor
        /// </summary>
        public AspNetEpiserverEnvironment() : this(null)
        {
        }

        /// <summary>
        /// Constructor with environment
        /// </summary>
        /// <param name="environmentName"></param>
        protected AspNetEpiserverEnvironment(string environmentName)
        {
            EnvironmentName = environmentName;

            if (string.IsNullOrWhiteSpace(EnvironmentName))
            {
                var appSetting = ConfigurationManager.AppSettings["episerver:EnvironmentName"];
                EnvironmentName = !string.IsNullOrWhiteSpace(appSetting) ? appSetting : Local;
            }
        }

        /// <summary>
        /// Current environment's name
        /// </summary>
        public string EnvironmentName { get; }

        /// <summary>
        /// Determines if environment name matches given name
        /// </summary>
        /// <param name="environmentName"></param>
        /// <returns></returns>
        public virtual bool IsEnvironment(string environmentName) => string.CompareOrdinal(environmentName, EnvironmentName) == 0;

        /// <summary>
        /// Determines if environment is Integration
        /// </summary>
        /// <returns></returns>
        public virtual bool IsIntegration() => IsEnvironment(Integration);

        /// <summary>
        /// Determines if environment is Local
        /// </summary>
        /// <returns></returns>
        public virtual bool IsLocal() => IsEnvironment(Local);

        /// <summary>
        /// Determines if environment is Preproduction
        /// </summary>
        /// <returns></returns>
        public virtual bool IsPreproduction() => IsEnvironment(Preproduction);

        /// <summary>
        /// Determines if environment is Production
        /// </summary>
        /// <returns></returns>
        public virtual bool IsProduction() => IsEnvironment(Production);

        /// <summary>
        /// Determines if environment is UnitTest
        /// </summary>
        /// <returns></returns>
        public virtual bool IsUnitTest() => IsEnvironment(UnitTest);
    }
}