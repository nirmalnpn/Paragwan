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
            var guides = _dapper.Query<Guide>("GetGuides");
            return View(guides);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Guide guide)
        {
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
            _dapper.Execute("UpdateGuide", guide);
            TempData["SuccessMessage"] = "Guide updated successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _dapper.Execute("DeleteGuide", new { GuideId = id });
            TempData["SuccessMessage"] = "Guide deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}