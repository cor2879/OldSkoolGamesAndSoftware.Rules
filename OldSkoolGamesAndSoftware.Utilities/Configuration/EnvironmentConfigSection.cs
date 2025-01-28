using System;
using System.Configuration;

namespace OldSkoolGamesAndSoftware.Utilities.Configuration
{
    /// <summary>
    /// Defines the custom config section added to a UDE application's
    /// configuration file.
    /// </summary>
    public class EnvironmentConfigSection 
        : ConfigurationSection
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EnvironmentConfigSection"/> class.
        /// </summary>
        public EnvironmentConfigSection()
            : base()
        { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Environments that can be used by the application.
        /// </summary>
        /// <value>The environments.</value>
        [ConfigurationProperty("environments")]
        public EnvironmentNodeCollection Environments
        {
            get { return ((EnvironmentNodeCollection)(base["environments"])); }
        }

        #endregion
    }

    /// <summary>
    /// Defines a collection of Environment XML Nodes for use in a .Net config
    /// file.  Inherits from System.Configuration.ConfigurationElementCollection.
    /// </summary>
    [ConfigurationCollection(typeof(EnvironmentNode))]
    public class EnvironmentNodeCollection : ConfigurationElementCollection
    {
        #region Properties

        /// <summary>
        /// Gets the <see cref="EnvironmentNode"/> 
        /// at the specified index.
        /// </summary>
        /// <value></value>
        public EnvironmentNode this[int index]
        {
            get { return (EnvironmentNode)BaseGet(index); }
        }

        /// <summary>
        /// Gets the <see cref="EnvironmentNode"/> with the specified key.
        /// </summary>
        /// <value>
        /// The <see cref="EnvironmentNode"/>.
        /// </value>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public EnvironmentNode this[object key]
        {
            get { return (EnvironmentNode)BaseGet(key); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// When overridden in a derived class, creates a new 
        /// <see cref="T:System.Configuration.ConfigurationElement"/>.
        /// </summary>
        /// <returns>
        /// A new <see cref="T:System.Configuration.ConfigurationElement"/>.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new EnvironmentNode();
        }

        /// <summary>
        /// Gets the element key for a specified configuration element when overridden 
        /// in a derived class.
        /// </summary>
        /// <param name="element">
        /// The <see cref="T:System.Configuration.ConfigurationElement"/> to return the 
        /// key for.
        /// </param>
        /// <returns>
        /// An <see cref="T:System.Object"/> that acts as the key for the specified 
        /// <see cref="T:System.Configuration.ConfigurationElement"/>.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EnvironmentNode)(element)).Name;
        }

        #endregion
    }

    /// <summary>
    /// Defines an individual Environment XML node for a .Net config file.
    /// Inherits from <see cref="T:System.Configuration.ConfigurationElement"/>.
    /// </summary>
    public class EnvironmentNode : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the Virtual Machine name.
        /// </summary>
        /// <value>The name.</value>
        [ConfigurationProperty("name", DefaultValue = "", IsKey = true,
            IsRequired = true)]
        public string Name
        {
            get { return base["name"].ToString(); }
            set { base["name"] = value; }
        }

        /// <summary>
        /// Gets or sets the annotation service.
        /// </summary>
        /// <value>
        /// The annotation service.
        /// </value>
        [ConfigurationProperty("annotationService", DefaultValue = "", IsRequired = false)]
        public string AnnotationService
        {
            get { return base["annotationService"].ToString(); }
            set { base["annotationService"] = value; }
        }

        /// <summary>
        /// Gets or sets the ude base service.
        /// </summary>
        /// <value>
        /// The ude base service.
        /// </value>
        [ConfigurationProperty("udeBaseService", DefaultValue = "", IsRequired = false)]
        public string UdeBaseService
        {
            get { return base["udeBaseService"].ToString(); }
            set { base["udeBaseService"] = value; }
        }

        /// <summary>
        /// Gets or sets the annotation connection string.
        /// </summary>
        /// <value>
        /// The annotation connection string.
        /// </value>
        [ConfigurationProperty("annotationConnectionString", DefaultValue = "", IsRequired = false)]
        public string AnnotationConnectionString
        {
            get { return base["annotationConnectionString"].ToString(); }
            set { base["annotationConnectionString"] = value; }
        }

        /// <summary>
        /// Gets or sets the ude base connection string.
        /// </summary>
        /// <value>
        /// The ude base connection string.
        /// </value>
        [ConfigurationProperty("udeBaseConnectionString", DefaultValue = "", IsRequired = false)]
        public string UdeBaseConnectionString
        {
            get { return base["udeBaseConnectionString"].ToString(); }
            set { base["udeBaseConnectionString"] = value; }
        }

        /// <summary>
        /// Gets or sets the log fact model connection string.
        /// </summary>
        /// <value>
        /// The log fact model connection string.
        /// </value>
        [ConfigurationProperty("logFactModelConnectionString", DefaultValue = "", IsRequired = false)]
        public string LogFactModelConnectionString
        {
            get { return base["logFactModelConnectionString"].ToString(); }
            set { base["logFactModelConnectionString"] = value; }
        }

        /// <summary>
        /// Gets or sets the win de connection string.
        /// </summary>
        /// <value>
        /// The win de connection string.
        /// </value>
        [ConfigurationProperty("windeConnectionString", DefaultValue = "", IsRequired = false)]
        public string WinDEConnectionString
        {
            get { return base["windeConnectionString"].ToString(); }
            set { base["windeConnectionString"] = value; }
        }

        /// <summary>
        /// Gets or sets the workspace service.
        /// </summary>
        /// <value>
        /// The workspace service.
        /// </value>
        [ConfigurationProperty("workspaceService", DefaultValue = "", IsRequired = false)]
        public string WorkspaceService
        {
            get { return base["workspaceService"].ToString(); }
            set { base["workspaceService"] = value; }
        }

        /// <summary>
        /// Gets or sets the notification service.
        /// </summary>
        /// <value>
        /// The notification service.
        /// </value>
        [ConfigurationProperty("notificationService", DefaultValue = "", IsRequired = false)]
        public string NotificationService
        {
            get { return base["notificationService"].ToString(); }
            set { base["notificationService"] = value; }
        }

        /// <summary>
        /// Gets or sets the NGFM notification endpoint.
        /// </summary>
        /// <value>
        /// The NGFM notification endpoint.
        /// </value>
        [ConfigurationProperty("NGFMNotificationEndpoint", DefaultValue = "", IsRequired = false)]
        public string NGFMNotificationEndpoint
        {
            get { return base["NGFMNotificationEndpoint"].ToString(); }
            set { base["NGFMNotificationEndpoint"] = value; }
        }

        /// <summary>
        /// Gets or sets the approved certificate thumbprints.
        /// </summary>
        /// <value>
        /// The approved certificate thumbprints.
        /// </value>
        [ConfigurationProperty("ApprovedCertificateThumbprints", DefaultValue = "", IsRequired = false)]
        public string ApprovedCertificateThumbprints
        {
            get { return base["ApprovedCertificateThumbprints"].ToString(); }
            set { base["ApprovedCertificateThumbprints"] = value; }
        }

        /// <summary>
        /// Gets or sets the monitoring connection string.
        /// </summary>
        /// <value>
        /// The monitoring connection string.
        /// </value>
        [ConfigurationProperty("monitoringConnectionString", DefaultValue = "", IsRequired = false)]
        public string MonitoringConnectionString
        {
            get { return base["monitoringConnectionString"].ToString(); }
            set { base["monitoringConnectionString"] = value; }
        }

        /// <summary>
        /// Gets or sets the alert management connection string.
        /// </summary>
        /// <value>
        /// The alert management connection string.
        /// </value>
        [ConfigurationProperty("alertManagementConnectionString", DefaultValue = "", IsRequired = false)]
        public string AlertManagementConnectionString
        {
            get { return base["alertManagementConnectionString"].ToString(); }
            set { base["alertManagementConnectionString"] = value; }
        }

        /// <summary>
        /// Gets or sets the host server.
        /// </summary>
        /// <value>
        /// The host server.
        /// </value>
        [ConfigurationProperty("hostServer", DefaultValue = "", IsRequired = false)]
        public string hostServer
        {
            get { return base["hostServer"].ToString(); }
            set { base["hostServer"] = value; }
        }

        /// <summary>
        /// Gets or sets the sr data service.
        /// </summary>
        /// <value>
        /// The sr data service.
        /// </value>
        [ConfigurationProperty("SRDataService", DefaultValue = "", IsRequired = false)]
        public string SRDataService
        {
            get { return base["SRDataService"].ToString(); }
            set { base["SRDataService"] = value; }
        }

        /// <summary>
        /// Gets or sets the RSA encryption certificate store location.
        /// </summary>
        /// <value>
        /// The RSA encryption certificate store location.
        /// </value>
        [ConfigurationProperty("RSAEncryptionCertificateStoreLocation", DefaultValue = "", IsRequired = false)]
        public string RSAEncryptionCertificateStoreLocation
        {
            get { return base["RSAEncryptionCertificateStoreLocation"].ToString(); }
            set { base["RSAEncryptionCertificateStoreLocation"] = value; }
        }

        /// <summary>
        /// Gets or sets the name of the RSA encryption certificate subject.
        /// </summary>
        /// <value>
        /// The name of the RSA encryption certificate subject.
        /// </value>
        [ConfigurationProperty("RSAEncryptionCertificateSubjectName", DefaultValue = "", IsRequired = false)]
        public string RSAEncryptionCertificateSubjectName
        {
            get { return base["RSAEncryptionCertificateSubjectName"].ToString(); }
            set { base["RSAEncryptionCertificateSubjectName"] = value; }
        }
    }
}
