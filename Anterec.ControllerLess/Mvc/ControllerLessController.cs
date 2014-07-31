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
        public ActionResult Index()
        {
            var action = RouteData.Values["x-action"].ToString();
            RouteData.Values["action"] = action;
            if (RouteData.Values["area"] != null)
            {
                RouteData.DataTokens["area"] = RouteData.Values["area"].ToString();
            }

            return View(action);
        }
    }
}
