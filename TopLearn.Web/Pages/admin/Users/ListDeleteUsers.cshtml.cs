using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.core.DTOs;
using TopLearn.core.Services.Intefaces;

namespace TopLearn.Web.Pages.Admin.Users
{
    public class ListDeleteUsersModel : PageModel
    {
        private IUserService _userService;

        public ListDeleteUsersModel(IUserService userService)
        {
            _userService = userService;
        }

        public ShowUsersForAdminViewModel Users { get; set; }

        public void OnGet(int pageId = 1, string FilterUserName = "", string FilterEmail = "")
        {
            Users = _userService.GetDeleteUsers(pageId, FilterUserName, FilterEmail);
        }
    }
}

