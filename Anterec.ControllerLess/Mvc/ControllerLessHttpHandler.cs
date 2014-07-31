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
        /// Initializes a new instance of the <see cref="ControllerLessHttpHandler"/> class.
        /// </summary>
        /// <param name="requestContext">The requestContext to be used in this instance.</param>
        public ControllerLessHttpHandler(RequestContext requestContext)
        {
            _requestContext = requestContext;

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
                        _requestContext.RouteData.Values["x-action"] = action;

                        var settings = RouteConfiguration.GetConfigurationSettings();
                        var route = settings.Get(string.Format("/{0}/{1}", controller, action));

                        if (route != null)
                        {
                            _requestContext.RouteData.Values["controller"] = route.Controller;
                            _requestContext.RouteData.Values["action"] = route.Action;
                            controller = route.Controller;
                        }
                        else
                        {
                            _requestContext.RouteData.Values["action"] = settings.DefaultAction;
                            controller = settings.DefaultController;
                        }

                        viewController = controllerFactory.CreateController(_requestContext, controller);
                        if (viewController != null)
                        {
                            viewController.Execute(_requestContext);
                        }
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
    }
}
