namespace bmcdavid.Episerver.CmsToolbox
{
    /// <summary>
    /// Episerver Environment information
    /// </summary>
    public interface IEpiserverEnvironment
    {
        /// <summary>
        /// Current Environment Name
        /// </summary>
        string EnvironmentName { get; }

        /// <summary>
        /// Determines if given environment matches current environment
        /// </summary>
        /// <param name="environmentName"></param>
        /// <returns></returns>
        bool IsEnvironment(string environmentName);

        /// <summary>
        /// Determines if EnvironmentName is 'Integration'
        /// </summary>
        /// <returns></returns>
        bool IsIntegration();

        /// <summary>
        /// Determines if EnvironmentName is 'Local'
        /// </summary>
        /// <returns></returns>
        bool IsLocal();

        /// <summary>
        /// Determines if EnvironmentName is 'Preproduction'
        /// </summary>
        /// <returns></returns>
        bool IsPreproduction();

        /// <summary>
        /// Determines if EnvironmentName is 'Production'
        /// </summary>
        /// <returns></returns>
        bool IsProduction();

        /// <summary>
        /// Determines if EnvironmentName is 'UnitTest'
        /// </summary>
        /// <returns></returns>
        bool IsUnitTest();
    }
}