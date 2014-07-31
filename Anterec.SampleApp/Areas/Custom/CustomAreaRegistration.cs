namespace Anterec.SampleApp.Areas.Custom
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using Anterec.ControllerLess.Mvc;

    /// <summary>
    /// The custom area registration.
    /// </summary>
    public class CustomAreaRegistration : AreaRegistration
    {
        /// <summary>
        /// Gets the name of the area to register.
        /// </summary>
        public override string AreaName
        {
            get
            {
                return "Custom";
            }
        }
        
        /// <summary>
        /// Registers an area in an ASP.NET MVC application using the specified area's context information.
        /// </summary>
        /// <param name="context">Encapsulates the information that is required in order to register the area.</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            var route = new Route(
                "Custom/{controller}/{action}/{id}",
                new RouteValueDictionary(new { area = "Custom", controller = "Home", action = "Index", id = UrlParameter.Optional }),
                new ControllerLessRouteHandler());

            context.Routes.Add(route);
        }
    }
}