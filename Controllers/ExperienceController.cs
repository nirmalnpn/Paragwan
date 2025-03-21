using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Paragwan.DataAccess;
using Paragwan.Models;

namespace Paragwan.Controllers
{
    public class ExperienceController : Controller
    {
        private readonly DapperHelper _dapper;

        public ExperienceController(DapperHelper dapper)
        {
            _dapper = dapper;
        }

        public IActionResult Index()
        {
            var experiences = _dapper.Query<ClientExperience>("GetClientExperiences");
            return View(experiences);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(ClientExperience experience)
        {
            // Ensure the user is logged in
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "You must be logged in to share your experience.";
                return RedirectToAction("Login", "Auth");
            }
            experience.UserId = userId.Value;

            _dapper.Execute("AddClientExperience", experience);
            TempData["SuccessMessage"] = "Experience shared successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var experience = _dapper.QuerySingle<ClientExperience>("GetClientExperienceById", new { ExperienceId = id });
            return View(experience);
        }

        [HttpPost]
        public IActionResult Edit(ClientExperience experience)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "You must be logged in to edit your experience.";
                return RedirectToAction("Login", "Auth");
            }
            // Optionally verify that experience.UserId == userId.Value
            _dapper.Execute("UpdateClientExperience", experience);
            TempData["SuccessMessage"] = "Experience updated successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "You must be logged in to delete an experience.";
                return RedirectToAction("Login", "Auth");
            }
            _dapper.Execute("DeleteClientExperience", new { ExperienceId = id });
            TempData["SuccessMessage"] = "Experience deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
