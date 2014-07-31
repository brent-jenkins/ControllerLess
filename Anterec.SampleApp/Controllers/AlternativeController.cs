namespace Anterec.SampleApp.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// The alternative controller.
    /// </summary>
    public class AlternativeController : Controller
    {
        /// <summary>
        /// Handle the AlternativeRouting action.
        /// </summary>
        /// <returns>
        /// The AlternativeRouting View.
        /// </returns>
        public ActionResult AlternativeRouting()
        {
            return View();
        }
    }
}