using Microsoft.AspNetCore.Mvc;
using Paragwan.DataAccess;
using Paragwan.Models;

namespace Paragwan.Controllers
{
    public class ProfileController : Controller
    {
        private readonly DapperHelper _dapper;

        public ProfileController(DapperHelper dapper)
        {
            _dapper = dapper;
        }

        public IActionResult Index()
        {
            // Get current user's ID from session
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            var user = _dapper.QuerySingle<User>("GetUserById", new { UserId = userId });
            return View(user);
        }

        public IActionResult BookAdventure()
        {
            // Placeholder page for booking adventures
            return View();
        }
    }
}
