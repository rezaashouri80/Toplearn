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
    [PermisionChecker(7)]
    public class CreateRoleModel : PageModel
    {
        private IPermisionService _permisionService;

       
        public CreateRoleModel(IPermisionService permisionService)
        {
            _permisionService = permisionService;
        }

        [BindProperty]
        public Role Role { get; set; }


        public void OnGet()
        {
            ViewData["Permisions"] = _permisionService.GetAllPermision();

        }

        public IActionResult OnPost(List<int> SelectedPermisions)
        {
            if (!ModelState.IsValid)
                return Page();

            int RoleId= _permisionService.AddRole(Role);

            _permisionService.AddPermisionToRoles(RoleId,SelectedPermisions);

            return RedirectToPage("Index");
        }
    }
}
