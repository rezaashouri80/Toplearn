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
    [PermisionChecker(4)]
    public class EditUserModel : PageModel
    {
        private IUserService _userService;
        private IPermisionService _permisionService;

        public EditUserModel(IUserService userService, IPermisionService permisionService)
        {
            _userService = userService;
            _permisionService = permisionService;
        }
        [BindProperty]
        public EditUserForAdminViewModel EditUser { set; get; }

        public void OnGet(int id)
        {
            EditUser = _userService.GetUserForEdit(id);
            ViewData["Roles"] = _permisionService.GetRoles();
        }

        public IActionResult OnPost(List<int> UserRoles)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _userService.EditUserFromAdmin(EditUser);
            
            //Edit User Roles
            _permisionService.EditRoles(UserRoles,EditUser.UserId);

           return RedirectToPage("Index");
        }
    }
}
