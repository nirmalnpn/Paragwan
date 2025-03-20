using Dapper;
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
            var parameters = new DynamicParameters();
            parameters.Add("@FullName", user.FullName);
            parameters.Add("@Email", user.Email);
            parameters.Add("@Address", user.Address);
            parameters.Add("@PhoneNumber", user.PhoneNumber);
            parameters.Add("@NationalID", user.NationalID);
            parameters.Add("@Photo", user.Photo);
            parameters.Add("@ProfilePicture", user.ProfilePicture);
            parameters.Add("@Password", user.Password);
            parameters.Add("@UserType", user.UserType);

            _dapper.Execute("spRegisterUser", parameters);

            TempData["SuccessMessage"] = "Registration successful! You can now login.";
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Email", email);
            parameters.Add("@Password", password);

            var user = _dapper.QuerySingle<User>("spLoginUser", parameters);

            if (user != null)
            {
                TempData["SuccessMessage"] = "Login successful! Welcome to Paragwan.";
                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorMessage"] = "Invalid login credentials. Please try again.";
            return View();
        }
    }
}
