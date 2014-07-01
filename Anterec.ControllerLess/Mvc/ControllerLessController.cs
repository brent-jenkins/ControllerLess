namespace Anterec.ControllerLess.Mvc
{
    using System.Web.Mvc;
    using Anterec.ControllerLess.Configuration;

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
            var settings = RouteConfiguration.GetConfigurationSettings();
            var controller = RouteData.Values["x-ctrl"].ToString();
            var action = RouteData.Values["x-action"].ToString();
            var area = RouteData.Values["x-area"].ToString();
            var view = string.Format("/{0}/{1}", controller, action);
            var extension = settings.DefaultViewExtension;

            var route = settings.Get(view);
            if (route != null)
            {
                extension = route.ViewExtension;
            }

            if (!string.IsNullOrWhiteSpace(area))
            {
                return View(string.Format("~/Areas/{0}/Views{1}{2}", area, view, extension));
            }

            return View(string.Format("~/Views{0}{1}", view, extension));
        }
    }
}
