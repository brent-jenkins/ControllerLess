namespace Anterec.ControllerLess.Mvc
{
    using System.Web;
    using System.Web.Routing;

    /// <summary>
    /// The DynamicRouteHandler class.
    /// </summary>
    public sealed class DynamicRouteHandler : IRouteHandler
    {
        /// <summary>
        /// Gets the IRouteHandler for the current request.
        /// </summary>
        /// <param name="requestContext">The RequestContext for the current request.</param>
        /// <returns>The IRouteHandler for the current request.</returns>
        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            return new DynamicHttpHandler(requestContext);
        }
    }
}
