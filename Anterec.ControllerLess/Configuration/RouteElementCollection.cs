namespace Anterec.ControllerLess.Configuration
{
    using System.Configuration;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// The route element collection.
    /// </summary>
    public class RouteElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </summary>
        /// <returns>
        /// A new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new RouteElement();
        }

        /// <summary>
        /// Gets the element key for a specified configuration element when overridden in a derived class.
        /// </summary>
        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to return the key for.</param>
        /// <returns>
        /// An <see cref="T:System.Object" /> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            object value = null;
            var property = typeof(RouteElement).GetProperties(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(x => x.Name == "Url");
            if (property != null)
            {
                value = property.GetValue(element, BindingFlags.Instance, null, null, null);
            }

            return value;
        }
    }
}
