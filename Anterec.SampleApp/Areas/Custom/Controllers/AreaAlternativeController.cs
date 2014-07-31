namespace Anterec.SampleApp.Areas.Custom.Controllers
{
    using System.Web.Mvc;
    using Anterec.SampleApp.Models;

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
        [HttpGet]
        public ActionResult AlternativeRouting()
        {
            return View(new User());
        }

        /// <summary>
        /// Handle the AlternativeRouting action.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// The AlternativeRouting View.
        /// </returns>
        [HttpPost]
        public ActionResult AlternativeRouting(User user)
        {
            return View(user);
        }
    }
}