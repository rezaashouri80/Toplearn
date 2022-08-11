using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.core.DTOs;
using TopLearn.core.Security;
using TopLearn.core.Services.Intefaces;

namespace TopLearn.Web.Pages.Admin.Users
{
    [PermisionChecker(3)]
    public class CreateUserModel : PageModel
    {
        private IUserService _userService;
        private IPermisionService _permisionService;

        public CreateUserModel(IUserService userService, IPermisionService permisionService)
        {
            _userService = userService;
            _permisionService = permisionService;
        }

        [BindProperty]
        public CreateUserViewModel CreateUser { get; set; }

        public void OnGet()
        {
            ViewData["Roles"] = _permisionService.GetRoles();
        }

        public IActionResult OnPost(List<int> UserRoles)
        {
            // ViewData["Roles"] = _permisionService.GetRoles();

            if (!ModelState.IsValid)
            {
                return Page();
            }



            //if (!_userService.IsExistEmail(CreateUser.Email))
            //{
            //    ModelState.AddModelError("Email", "تیمیل تکراری است");
            //    return Page();
            //}

            //if (!_userService.IsExistUserName(CreateUser.UserName))
            //{
            //    ModelState.AddModelError("UserName", "تام کاربری تکراری است");
            //    return Page();
            //}

            int userId = _userService.AddUserForAdmin(CreateUser);

            _permisionService.AddUserRoles(UserRoles,userId);

            return Redirect("/admin/users");
        }
    }
}
