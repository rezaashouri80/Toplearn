using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.core.Security;
using TopLearn.core.Services.Intefaces;
using TopLearn.Datalayar.Entities.User;

namespace TopLearn.Web.Pages.admin.Roles
{
    [PermisionChecker(9)]
    public class DeleteRoleModel : PageModel
    {
        private IPermisionService _permisionService;

        public DeleteRoleModel(IPermisionService permisionService)
        {
            _permisionService = permisionService;
        }

        [BindProperty]
        public Role Role { set; get; }

        public void OnGet(int id)
        {
            Role = _permisionService.GetRoleById(id);
        }

        public IActionResult OnPost()
        {
            _permisionService.DeleteRole(Role);

            return RedirectToPage("Index");
        }
    }
}
