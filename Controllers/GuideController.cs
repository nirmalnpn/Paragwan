using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Paragwan.DataAccess;
using Paragwan.Models;

namespace Paragwan.Controllers
{
    public class GuideController : Controller
    {
        private readonly DapperHelper _dapper;

        public GuideController(DapperHelper dapper)
        {
            _dapper = dapper;
        }

        public IActionResult Index()
        {
            var guides = _dapper.Query<Guide>("GetGuides");
            return View(guides);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Guide guide)
        {
            // Ensure the user is logged in
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "You must be logged in to add guide information.";
                return RedirectToAction("Login", "Auth");
            }
            guide.UserId = userId.Value;

            _dapper.Execute("AddGuide", guide);
            TempData["SuccessMessage"] = "Guide added successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var guide = _dapper.QuerySingle<Guide>("GetGuideById", new { GuideId = id });
            return View(guide);
        }

        [HttpPost]
        public IActionResult Edit(Guide guide)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "You must be logged in to edit guide information.";
                return RedirectToAction("Login", "Auth");
            }
            // Optionally verify guide.UserId == userId.Value

            _dapper.Execute("UpdateGuide", guide);
            TempData["SuccessMessage"] = "Guide updated successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "You must be logged in to delete guide information.";
                return RedirectToAction("Login", "Auth");
            }
            _dapper.Execute("DeleteGuide", new { GuideId = id });
            TempData["SuccessMessage"] = "Guide deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
