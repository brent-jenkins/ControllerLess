namespace Anterec.ControllerLess.Mvc
{
    using System.Web.Mvc;

    /// <summary>
    /// The dynamic controller.
    /// </summary>
    public class ControllerLessViewController : Controller
    {
        /// <summary>
        /// Performs the Index action.
        /// </summary>
        /// <returns>The Index view.</returns>
        public ActionResult Index()
        {
            var controller = RouteData.Values["ctrl"].ToString();
            var action = RouteData.Values["requested-action"].ToString();
            var route = string.Format("~/Views/{0}/{1}.cshtml", controller, action);
            return View(route);
        }
    }
}
