namespace Anterec.ControllerLess.Mvc
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Anterec.ControllerLess.Configuration;

    /// <summary>
    /// The ControllerLessHttpHandler class.
    /// </summary>
    public class ControllerLessHttpHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState, IRouteHandler
    {
        /// <summary>
        /// The assembly name.
        /// </summary>
        private const string AssemblyName = "Anterec.ControllerLess.Mvc";

        /// <summary>
        /// Gets or sets the requestContext property.
        /// </summary>
        private readonly RequestContext _requestContext;

        /// <summary>
        /// The settings field.
        /// </summary>
        private readonly RouteConfiguration _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerLessHttpHandler"/> class.
        /// </summary>
        /// <param name="requestContext">The requestContext to be used in this instance.</param>
        public ControllerLessHttpHandler(RequestContext requestContext)
        {
            _requestContext = requestContext;
            _settings = (RouteConfiguration)System.Configuration.ConfigurationManager.GetSection("controllerLessSettings");

            if (!ControllerBuilder.Current.DefaultNamespaces.Contains(AssemblyName))
            {
                ControllerBuilder.Current.DefaultNamespaces.Add(AssemblyName);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this class is reusable.
        /// </summary>
        public bool IsReusable
        {
            get { return true; }
        }

        /// <summary>
        /// Process the current HTTP request.
        /// </summary>
        /// <param name="httpContext">The HttpContext containing the request.</param>
        public void ProcessRequest(HttpContext httpContext)
        {
            var controllerName = _requestContext.RouteData.GetRequiredString("controller");
            var actionName = string.Empty;

            if (_requestContext.RouteData.Values["action"] != null)
            {
                actionName = _requestContext.RouteData.Values["action"].ToString();
            }

            // Some browsers make additional requests for favicon.ico, etc. We need to filter this
            // out before trying to find the correct controller.
            if (!controllerName.Contains(".") && !actionName.Contains("."))
            {
                IController controller = null;
                IControllerFactory factory = null;

                try
                {
                    factory = ControllerBuilder.Current.GetControllerFactory();

                    try
                    {
                        // Try to create an instance of the view specific controller.
                        controller = factory.CreateController(_requestContext, controllerName);
                    }
                    catch
                    {
                        // If the view specific controller isn't available, then fall-back to the
                        // controller-less view controller instead.
                        _requestContext.RouteData.Values["ctrl"] = controllerName;
                        _requestContext.RouteData.Values["view"] = _requestContext.RouteData.Values["action"];

                        var route = _settings.Get(string.Format("/{0}/{1}", controllerName, _requestContext.RouteData.Values["action"]));

                        if (route != null)
                        {
                            _requestContext.RouteData.Values["action"] = route.Action;
                            controllerName = route.Controller;
                        }
                        else
                        {
                            _requestContext.RouteData.Values["action"] = _settings.DefaultAction;
                            controllerName = _settings.DefaultController;
                        }

                        controller = factory.CreateController(_requestContext, controllerName);
                    }

                    if (controller != null)
                    {
                        controller.Execute(_requestContext);
                    }
                }
                finally
                {
                    if (factory != null)
                    {
                        factory.ReleaseController(controller);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the HttpHandler for the current request.
        /// </summary>
        /// <param name="requestContext">The requestContext for the current request.</param>
        /// <returns>The HttpHandler for the current request.</returns>
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            throw new NotImplementedException();
        }
    }
}
