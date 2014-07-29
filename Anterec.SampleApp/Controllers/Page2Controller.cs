namespace Anterec.SampleApp.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// The page 2 controller.
    /// </summary>
    public class Page2Controller : Controller
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