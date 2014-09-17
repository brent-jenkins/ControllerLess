namespace Anterec.ControllerLess.Mvc
{
    using System.Web.Mvc;

    /// <summary>
    /// The ControllerLessController class.
    /// </summary>
    public class ControllerLessController : Controller
    {
        /// <summary>
        /// Provides the default view controller.
        /// </summary>
        /// <returns>The requested view.</returns>
        public virtual ActionResult Index()
        {
            var action = RouteData.Values["x-action"].ToString();
            var controller = RouteData.Values["x-controller"].ToString();
            RouteData.Values["action"] = action;
            RouteData.Values["controller"] = controller;
            if (RouteData.Values["area"] != null)
            {
                RouteData.DataTokens["area"] = RouteData.Values["area"].ToString();
            }

            return View(action);
        }
    }
}
