using Dapper;
using Microsoft.AspNetCore.Mvc;
using Paragwan.DataAccess;
using Paragwan.Models;

namespace Paragwan.Controllers
{
    public class PhotoController : Controller
    {
        private readonly DapperHelper _dapper;

        public PhotoController(DapperHelper dapper)
        {
            _dapper = dapper;
        }

        public IActionResult Index()
        {
            var photos = _dapper.Query<Photo>("spGetAllPhotos", null);
            return View(photos);
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(Photo photo)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", photo.UserId);
                parameters.Add("@Title", photo.Title);
                parameters.Add("@Description", photo.Description);
                parameters.Add("@ImageUrl", photo.ImageUrl);

                _dapper.Execute("spAddPhoto", parameters);
                TempData["SuccessMessage"] = "Photo uploaded successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error: " + ex.Message;
                return View();
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PhotoId", id);

                _dapper.Execute("spDeletePhoto", parameters);
                TempData["SuccessMessage"] = "Photo deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error: " + ex.Message;
            }
            return RedirectToAction("Index");
        }

    }
}
