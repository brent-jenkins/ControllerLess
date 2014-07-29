namespace Anterec.SampleApp.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// The home controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Handle the Index action.
        /// </summary>
        /// <returns>
        /// The Index View.
        /// </returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}