using Microsoft.AspNetCore.Mvc;
using Paragwan.DataAccess;
using Paragwan.Models;

namespace Paragwan.Controllers
{
    public class AuthController : Controller
    {
        private readonly DapperHelper _dapper;

        public AuthController(DapperHelper dapper)
        {
            _dapper = dapper;
        }

        public IActionResult Login() => View();
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(User user)
        {
            var parameters = new
            {
                FullName = user.FullName,
                Email = user.Email,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                NationalID = user.NationalID,
                Photo = user.Photo,
                ProfilePicture = user.ProfilePicture,
                Password = user.Password,
                UserType = user.UserType
            };

            _dapper.Execute("RegisterUser", parameters);
            TempData["SuccessMessage"] = "Registration successful! Please log in.";
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _dapper.QuerySingle<User>("LoginUser", new { Email = email, Password = password });
            if (user != null)
            {
                TempData["SuccessMessage"] = "Login successful! Welcome.";
                return RedirectToAction("Index", "Home");
            }
            TempData["ErrorMessage"] = "Invalid login credentials.";
            return View();
        }
    }
}