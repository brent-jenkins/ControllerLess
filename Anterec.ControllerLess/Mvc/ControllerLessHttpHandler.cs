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
        /// The request context.
        /// </summary>
        private readonly RequestContext _requestContext;

        /// <summary>
        /// The configuration settings.
        /// </summary>
        private readonly RouteConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerLessHttpHandler"/> class.
        /// </summary>
        /// <param name="requestContext">The requestContext to be used in this instance.</param>
        public ControllerLessHttpHandler(RequestContext requestContext)
        {
            _requestContext = requestContext;
            _configuration = RouteConfiguration.GetConfigurationSettings();

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
            var controller = _requestContext.RouteData.GetRequiredString("controller");
            var action = string.Empty;

            if (_requestContext.RouteData.Values["action"] != null)
            {
                action = _requestContext.RouteData.Values["action"].ToString();
            }

            if (action != string.Empty)
            {
                IController viewController = null;
                IControllerFactory controllerFactory = null;

                try
                {
                    controllerFactory = ControllerBuilder.Current.GetControllerFactory();

                    try
                    {
                        viewController = controllerFactory.CreateController(_requestContext, controller);
                        viewController.Execute(_requestContext);
                    }
                    catch
                    {
                        DispatchRequest(controllerFactory, controller, action);
                    }
                }
                finally
                {
                    if (controllerFactory != null)
                    {
                        controllerFactory.ReleaseController(viewController);
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

        /// <summary>
        /// Dispatches the request.
        /// </summary>
        /// <param name="controllerFactory">The controller factory.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        private void DispatchRequest(IControllerFactory controllerFactory, string controller, string action)
        {
            var route = GetRoute(controller, action);
            _requestContext.RouteData.Values["x-action"] = action;

            if (route != null)
            {
                _requestContext.RouteData.Values["controller"] = route.Controller;
                _requestContext.RouteData.Values["action"] = route.Action;

                if (route.Area != string.Empty)
                {
                    _requestContext.RouteData.DataTokens["area"] = route.Area;
                }

                controller = route.Controller;
            }
            else
            {
                _requestContext.RouteData.Values["action"] = _configuration.DefaultAction;
                controller = _configuration.DefaultController;
            }

            var viewController = controllerFactory.CreateController(_requestContext, controller);
            if (viewController != null)
            {
                viewController.Execute(_requestContext);
            }
        }

        /// <summary>
        /// Gets the configured route.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        /// <returns>The configured route (or null if the route is not configured).</returns>
        private RouteElement GetRoute(string controller, string action)
        {
            RouteElement route;

            if (_requestContext.RouteData.Values["area"] != null)
            {
                var area = _requestContext.RouteData.Values["area"].ToString();
                route = _configuration.Get(string.Format("/{0}/{1}/{2}", area, controller, action));
            }
            else
            {
                route = _configuration.Get(string.Format("/{0}/{1}", controller, action));
            }

            return route;
        }
    }
}
