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
    [PermisionChecker(5)]
    public class DeleteUserModel : PageModel
    {
        private IUserService _userService;

        public DeleteUserModel(IUserService userService)
        {
            _userService = userService;
        }

        public InformationUserViewModel InformationUserViewModel { get; set; }

        public void OnGet(int id)
        {
            ViewData["UserId"] = id;
            InformationUserViewModel = _userService.GetInformationUser(id);
        }

        public IActionResult OnPost(int Id)
        {
            _userService.DeleteUser(Id);

            return RedirectToPage("Index");
        }
    }
}
