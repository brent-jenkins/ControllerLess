namespace Anterec.SampleApp
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using Anterec.ControllerLess.Mvc;

    /// <summary>
    /// The route config.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var route = new Route(
                        "{controller}/{action}/{id}",
                        new RouteValueDictionary(new { controller = "Home", action = "Index", id = UrlParameter.Optional }),
                        new ControllerLessRouteHandler());

            routes.Add(route);
        }
    }
}
