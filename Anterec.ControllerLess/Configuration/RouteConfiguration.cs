namespace Anterec.ControllerLess.Configuration
{
    using System;
    using System.Configuration;

    /// <summary>
    /// The route configuration.
    /// </summary>
    public class RouteConfiguration : ConfigurationSection
    {
        /// <summary>
        /// Gets or sets the default controller.
        /// </summary>
        /// <value>
        /// The default controller.
        /// </value>
        [ConfigurationProperty("defaultController", DefaultValue = "ControllerLessView", IsRequired = false)]
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
        /// Gets route for the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>The matched route element if found, otherwise null.</returns>
        public RouteElement Get(string url)
        {
            RouteElement result = null;

            foreach (RouteElement element in Routes)
            {
                if (element.Url.Equals(url, StringComparison.InvariantCultureIgnoreCase))
                {
                    result = element;
                    break;
                }
            }

            return result;
        }
    }
}
