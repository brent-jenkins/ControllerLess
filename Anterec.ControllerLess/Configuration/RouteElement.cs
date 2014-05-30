namespace Anterec.ControllerLess.Configuration
{
    using System.Configuration;

    /// <summary>
    /// The route element.
    /// </summary>
    public class RouteElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the route URL.
        /// </summary>
        /// <value>
        /// The route URL.
        /// </value>
        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get
            {
                return (string)this["url"];
            }

            set
            {
                this["url"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the controller.
        /// </summary>
        /// <value>
        /// The controller.
        /// </value>
        [ConfigurationProperty("controller", DefaultValue = "Home", IsRequired = true)]
        public string Controller
        {
            get
            {
                return (string)this["controller"];
            }

            set
            {
                this["controller"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        [ConfigurationProperty("action", DefaultValue = "Index", IsRequired = true)]
        public string Action
        {
            get
            {
                return (string)this["action"];
            }

            set
            {
                this["action"] = value;
            }
        }
    }
}
