﻿using BlogMVC.Repository.LoggerService;
using BlogMVC.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogMVC.Web.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;
        private readonly ILoggerManager _logger;

        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ILoggerManager logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        //GET: Login
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        //POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            if (!signInResult.Succeeded)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.UserName);

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            if (isAdmin)
            {
                _logger.LogInfo("Admin signed in");
                return RedirectToAction("Index", "AdminPanel");
            }

            _logger.LogInfo("User signed in");
            return RedirectToAction("Index", "Home");
        }

        //GET: Register
        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.ConfirmPassword);

            if (result.Succeeded)
            {
                _logger.LogInfo("New user registered");
                //await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }

        //GET: Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

    }
}
