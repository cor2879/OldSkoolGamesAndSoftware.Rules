using System;
using System.Configuration;

namespace OldSkoolGamesAndSoftware.Utilities.Configuration
{
    /// <summary>
    /// Defines a static Configuration Manager class for the UDE
    /// environment and associated libraries.
    /// </summary>
    public static class EnvironmentConfigManager
    {
        #region Properties

        /// <summary>
        /// Gets the VM server information stored in the associated config file.
        /// </summary>
        /// <value>The VM server.</value>
        public static EnvironmentConfigSection Environments
        {
            get
            {
                return (EnvironmentConfigSection)ConfigurationManager.GetSection("environment");
            }
        }

        #endregion
    }
}