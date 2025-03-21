using Microsoft.AspNetCore.Mvc;
using Paragwan.DataAccess;
using Paragwan.Models;

namespace Paragwan.Controllers
{
    public class HomeController : Controller
    {
        private readonly DapperHelper _dapper;
        public HomeController(DapperHelper dapper)
        {
            _dapper = dapper;
        }

        public IActionResult Index()
        {
            // Fetch dynamic data using DapperHelper
            var model = new HomeViewModel()
            {
                Adventures = _dapper.Query<ParaglidingDetail>("GetParaglidingDetails"),
                Photos = _dapper.Query<Photo>("GetPhotos"),
                Experiences = _dapper.Query<ClientExperience>("GetClientExperiences")
            };
            return View(model);
        }
    }
}
