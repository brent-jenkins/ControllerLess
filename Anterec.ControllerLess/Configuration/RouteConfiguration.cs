namespace Anterec.ControllerLess.Configuration
{
    using System;
    using System.Configuration;
    using System.Linq;

    /// <summary>
    /// The RouteConfiguration class.
    /// </summary>
    public class RouteConfiguration : ConfigurationSection
    {
        /// <summary>
        /// Gets or sets the default controller.
        /// </summary>
        /// <value>
        /// The default controller.
        /// </value>
        [ConfigurationProperty("defaultController", DefaultValue = "ControllerLess", IsRequired = false)]
        public string DefaultController
        {
            get
            {
                return (string)this["defaultController"];
            }

            set
            {
                this["defaultController"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the default action.
        /// </summary>
        /// <value>
        /// The default action.
        /// </value>
        [ConfigurationProperty("defaultAction", DefaultValue = "Index", IsRequired = false)]
        public string DefaultAction
        {
            get
            {
                return (string)this["defaultAction"];
            }

            set
            {
                this["defaultAction"] = value;
            }
        }

        /// <summary>
        /// Gets the routes.
        /// </summary>
        /// <value>
        /// The routes.
        /// </value>
        [ConfigurationProperty("routes")]
        public RouteElementCollection Routes
        {
            get
            {
                return this["routes"] as RouteElementCollection;
            }
        }

        /// <summary>
        /// Gets the configuration settings.
        /// </summary>
        /// <returns>The configuration settings.</returns>
        public static RouteConfiguration GetConfigurationSettings()
        {
            var configuration = (RouteConfiguration)ConfigurationManager.GetSection("controllerLessSettings");
            
            if (configuration == null)
            {
                configuration = new RouteConfiguration();
            }

            return configuration;
        }

        /// <summary>
        /// Gets route for the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>The matched route element if found, otherwise null.</returns>
        public RouteElement Get(string url)
        {
            return Routes.Cast<RouteElement>().FirstOrDefault(element => element.Url.Equals(url, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
