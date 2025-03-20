using Dapper;
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
            var paraglidingDetails = _dapper.Query<ParaglidingDetail>("GetParaglidingById", new { ParaglidingId = 1 });
            return View(paraglidingDetails);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ParaglidingDetail detail)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Name", detail.Name);
                parameters.Add("@Description", detail.Description);
                parameters.Add("@Location", detail.Location);
                parameters.Add("@Price", detail.Price);
                parameters.Add("@Duration", detail.Duration);
                parameters.Add("@ImageUrl", detail.ImageUrl);

                _dapper.Execute("spAddParaglidingDetail", parameters);
                TempData["SuccessMessage"] = "Paragliding adventure added successfully!";
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
            parameters.Add("@ParaglidingId", id);

            var detail = _dapper.QuerySingle<ParaglidingDetail>("spGetParaglidingById", parameters);
            return View(detail);
        }

        [HttpPost]
        public IActionResult Edit(ParaglidingDetail detail)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ParaglidingId", detail.ParaglidingId);
                parameters.Add("@Name", detail.Name);
                parameters.Add("@Description", detail.Description);
                parameters.Add("@Location", detail.Location);
                parameters.Add("@Price", detail.Price);
                parameters.Add("@Duration", detail.Duration);
                parameters.Add("@ImageUrl", detail.ImageUrl);

                _dapper.Execute("spUpdateParaglidingDetail", parameters);
                TempData["SuccessMessage"] = "Paragliding adventure updated successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error: " + ex.Message;
                return View(detail);
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ParaglidingId", id);

                _dapper.Execute("spDeleteParaglidingDetail", parameters);
                TempData["SuccessMessage"] = "Paragliding adventure deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error: " + ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
