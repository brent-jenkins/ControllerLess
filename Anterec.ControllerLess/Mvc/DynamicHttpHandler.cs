namespace Anterec.ControllerLess.Mvc
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// The DynamicHttpHandler class.
    /// </summary>
    public class DynamicHttpHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState, IRouteHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicHttpHandler"/> class.
        /// </summary>
        /// <param name="requestContext">The RequestContext to be used in this instance.</param>
        public DynamicHttpHandler(RequestContext requestContext)
        {
            RequestContext = requestContext;
        }

        /// <summary>
        /// Gets or sets the RequestContext property.
        /// </summary>
        public RequestContext RequestContext { get; set; }

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
            var controllerName = RequestContext.RouteData.GetRequiredString("controller");
            var actionName = string.Empty;

            if (RequestContext.RouteData.Values["action"] != null)
            {
                actionName = RequestContext.RouteData.Values["action"].ToString();
            }

            // Google Chrome make additional requests for favicon.ico, etc. We need to filter this
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
                        controller = factory.CreateController(RequestContext, controllerName);
                    }
                    catch
                    {
                        // If the view specific controller isn't available, then fall-back to the
                        // dynamic view controller instead.
                        RequestContext.RouteData.Values["ctrl"] = controllerName;
                        RequestContext.RouteData.Values["view"] = RequestContext.RouteData.Values["action"];
                        RequestContext.RouteData.Values["action"] = "Index";
                        controllerName = "Dynamic";
                        controller = factory.CreateController(RequestContext, controllerName);
                    }

                    if (controller != null)
                    {
                        controller.Execute(RequestContext);
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
        /// <param name="requestContext">The RequestContext for the current request.</param>
        /// <returns>The HttpHandler for the current request.</returns>
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            throw new NotImplementedException();
        }
    }
}
