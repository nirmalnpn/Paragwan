using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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
            var photos = _dapper.Query<Photo>("GetPhotos");
            return View(photos);
        }

        public IActionResult Upload() => View();

        [HttpPost]
        public IActionResult Upload(Photo photo)
        {
            // Ensure the user is logged in by checking the session for a valid UserId
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "You must be logged in to upload a photo.";
                return RedirectToAction("Login", "Auth");
            }
            photo.UserId = userId.Value;

            _dapper.Execute("AddPhoto", photo);
            TempData["SuccessMessage"] = "Photo uploaded successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var photo = _dapper.QuerySingle<Photo>("GetPhotoById", new { PhotoId = id });
            return View(photo);
        }

        [HttpPost]
        public IActionResult Edit(Photo photo)
        {
            // Optionally check session if editing should be done by logged-in user.
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "You must be logged in to edit a photo.";
                return RedirectToAction("Login", "Auth");
            }
            // You might also verify that photo.UserId == userId.Value before editing.

            _dapper.Execute("UpdatePhoto", photo);
            TempData["SuccessMessage"] = "Photo updated successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            // Optionally, verify that the logged-in user is allowed to delete this photo.
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "You must be logged in to delete a photo.";
                return RedirectToAction("Login", "Auth");
            }

            _dapper.Execute("DeletePhoto", new { PhotoId = id });
            TempData["SuccessMessage"] = "Photo deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
