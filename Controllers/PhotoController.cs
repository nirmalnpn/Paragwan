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
            var photos = _dapper.Query<Photo>("GetPhotos");
            return View(photos);
        }

        public IActionResult Upload() => View();

        [HttpPost]
        public IActionResult Upload(Photo photo)
        {
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
            _dapper.Execute("UpdatePhoto", photo);
            TempData["SuccessMessage"] = "Photo updated successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _dapper.Execute("DeletePhoto", new { PhotoId = id });
            TempData["SuccessMessage"] = "Photo deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}