using Dapper;
using Microsoft.AspNetCore.Mvc;
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
            var guides = _dapper.Query<Guide>("spGetAllGuides", null);
            return View(guides);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Guide guide)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", guide.UserId);
                parameters.Add("@Bio", guide.Bio);
                parameters.Add("@ExperienceYears", guide.ExperienceYears);
                parameters.Add("@Ratings", guide.Ratings);

                _dapper.Execute("spAddGuide", parameters);
                TempData["SuccessMessage"] = "Guide registered successfully!";
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
            parameters.Add("@GuideId", id);

            var guide = _dapper.QuerySingle<Guide>("spGetGuideById", parameters);
            return View(guide);
        }

        [HttpPost]
        public IActionResult Edit(Guide guide)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@GuideId", guide.GuideId);
            parameters.Add("@Bio", guide.Bio);
            parameters.Add("@ExperienceYears", guide.ExperienceYears);
            parameters.Add("@Ratings", guide.Ratings);

            _dapper.Execute("spUpdateGuide", parameters);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@GuideId", id);

            _dapper.Execute("spDeleteGuide", parameters);
            return RedirectToAction("Index");
        }
    }
}
