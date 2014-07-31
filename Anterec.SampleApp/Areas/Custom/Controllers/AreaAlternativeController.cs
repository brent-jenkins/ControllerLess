namespace Anterec.SampleApp.Areas.Custom.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// The area alternative controller.
    /// </summary>
    public class AreaAlternativeController : Controller
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