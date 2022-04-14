using Group21ProjectMVC.Helpers;
using Group21ProjectMVC.Models;
using Group21ProjectMVC.ViewModels;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;

namespace Group21ProjectMVC.Controllers
{
    public class AccountController : Controller
    {
        private IConfiguration _configuration;
        CommonHelper _helper;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
            _helper = new CommonHelper(_configuration);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel vm)
        {
            string UserExistsQuery = $"Select * from [PassengerProfile] where Email='{vm.Email}'";
            bool userExists = _helper.UserAlreadyExists(UserExistsQuery);
            if (userExists)
            {
                ViewBag.Error = "Email Already Exists!";
                return View("Register", "Account");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(vm.Password);
            string Query = "Insert into [PassengerProfile](FirstName,LastName,Email,PhoneNumber,Password)values" +
                $"('{vm.FirstName}','{vm.LastName}','{vm.Email}','{vm.PhoneNumber}','{passwordHash}')";
            int result = _helper.DMLTransaction(Query);
            if (result > 0)
            {
                EntryIntoSession(vm.Email, vm.FirstName, vm.LastName);
                //return RedirectToAction("Index", "Home");
                ViewBag.Success = "Thanks for Registering!";
                return View();
            }
            return View();
        }

        private void EntryIntoSession(string Email,string FirstName, string LastName)
        {

            HttpContext.Session.SetString("Email", Email);
            HttpContext.Session.SetString("FirstName", FirstName);
            HttpContext.Session.SetString("LastName", LastName);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Login(LoginViewModel vm)
        {
            if (string.IsNullOrEmpty(vm.Email) && string.IsNullOrEmpty(vm.Password))
            {
                ViewBag.ErrorMsg = "Username and Password are Empty";
                return View();
            }
            else
            {
                bool isfind = SignInMethod(vm.Email, vm.Password);
                if (isfind)
                {
                    ViewBag.Success = "Login Successful!";
                    return View();  //return RedirectToAction("Index","Home");
                }
                return View("Login");
            }
        }

        private bool SignInMethod(string email, string password)
        {
            bool Result = false;
            string Query = $"SELECT * FROM [PassengerProfile] WHERE Email='{email}'";
            var userDetails = _helper.GetUserByEmail(Query);
            if (userDetails.Email != null && BCrypt.Net.BCrypt.Verify(password, userDetails.Password))
            {
                Result = true;
                EntryIntoSession(userDetails.Email, userDetails.FirstName, userDetails.LastName);
            }
            else
            {
                ViewBag.ErrorMsg = "UserName & Password Are Incorrect!";
            }
            return Result;
        }
    }
}
