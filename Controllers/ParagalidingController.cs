using Microsoft.AspNetCore.Mvc;
using Paragwan.DataAccess;
using Paragwan.Models;

namespace Paragwan.Controllers
{
    public class ParaglidingController : Controller
    {
        private readonly DapperHelper _dapper;
        public ParaglidingController(DapperHelper dapper)
        {
            _dapper = dapper;
        }
        public IActionResult Index()
        {
            var details = _dapper.Query<ParaglidingDetail>("GetParaglidingDetails");
            return View(details);
        }
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(ParaglidingDetail detail)
        {
            _dapper.Execute("AddParaglidingDetail", detail);
            TempData["SuccessMessage"] = "Paragliding detail added successfully!";
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var detail = _dapper.QuerySingle<ParaglidingDetail>("GetParaglidingById", new { ParaglidingId = id });
            return View(detail);
        }
        [HttpPost]
        public IActionResult Edit(ParaglidingDetail detail)
        {
            _dapper.Execute("UpdateParaglidingDetail", detail);
            TempData["SuccessMessage"] = "Paragliding detail updated successfully!";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _dapper.Execute("DeleteParaglidingDetail", new { ParaglidingId = id });
            TempData["SuccessMessage"] = "Paragliding detail deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
