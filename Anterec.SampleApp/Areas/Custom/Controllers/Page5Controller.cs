namespace Anterec.SampleApp.Areas.Custom.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// The page 5 controller.
    /// </summary>
    public class Page5Controller : Controller
    {
        /// <summary>
        /// Handle the WithController action.
        /// </summary>
        /// <returns>
        /// The WithController View.
        /// </returns>
        public ActionResult WithController()
        {
            return View();
        }
    }
}