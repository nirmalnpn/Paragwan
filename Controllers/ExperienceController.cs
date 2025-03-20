using Dapper;
using Microsoft.AspNetCore.Mvc;
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
            var experiences = _dapper.Query<Experience>("spGetAllClientExperiences", null);
            return View(experiences);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Experience experience)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", experience.UserId);
                parameters.Add("@Description", experience.Description);
                parameters.Add("@Rating", experience.Rating);

                _dapper.Execute("spAddClientExperience", parameters);
                TempData["SuccessMessage"] = "Experience shared successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error: " + ex.Message;
                return View();
            }
        }



        public IActionResult Edit(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ExperienceId", id);

            var experience = _dapper.QuerySingle<Experience>("spGetClientExperienceById", parameters);
            return View(experience);
        }

        [HttpPost]
        public IActionResult Edit(Experience experience)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ExperienceId", experience.ExperienceId);
            parameters.Add("@Description", experience.Description);
            parameters.Add("@Rating", experience.Rating);

            _dapper.Execute("spUpdateClientExperience", parameters);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ExperienceId", id);

            _dapper.Execute("spDeleteClientExperience", parameters);
            return RedirectToAction("Index");
        }
    }
}
