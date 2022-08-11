using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.CodeAnalysis.Differencing;
using TopLearn.core.DTOs;
using TopLearn.Core.Security;
using TopLearn.core.Services.Intefaces;

namespace TopLearn.Web.Areas.Userpanel.Controllers
{
    [Area("Userpanel")]
    [Authorize]
    public class HomeController : Controller
    {
        IUserService _userService;


        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View(_userService.GetInformationUser(User.Identity.Name));
        }

        public IActionResult EditProfile()
        {
            return View(_userService.getEditProfileData(User.Identity.Name));
        }

        [HttpPost]
        public IActionResult EditProfile(EditProfileViewModel profile)
        {
            if (!ModelState.IsValid)
            {
                return View(profile);
            }

            _userService.EditProfile(User.Identity.Name, profile); 
           
            
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


            return Redirect("/Login?Editprofile=true");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel change)
        {
            if (!ModelState.IsValid)
            {
                return View(change);
            }

            string username = User.Identity.Name;

            if (!_userService.CompareOldPassword(username,change.OldPassword))
            {
                ModelState.AddModelError("OldPassword","کلمه عبور صحیح نمیباشد");
                return View(change);
            }
         
            _userService.ChaangePassword(username,change.Password);
            ViewBag.IsSuccess = true;

            return View();
        }
    }
}
