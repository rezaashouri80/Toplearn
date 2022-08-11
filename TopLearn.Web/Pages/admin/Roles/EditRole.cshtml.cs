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
    [PermisionChecker(8)]

    public class EditRoleModel : PageModel
    {
        private IPermisionService _permisionService;

        public EditRoleModel(IPermisionService permisionService)
        {
            _permisionService = permisionService;
        }
        
        [BindProperty]
        public Role role { set; get; }

        public void OnGet(int id)
        {
            ViewData["Permisions"] = _permisionService.PermisionRoles(id);
            ViewData["AllPermisions"] = _permisionService.GetAllPermision();
            role = _permisionService.GetRoleById(id);
        }

        public IActionResult OnPost(List<int> SelectedPermisions)
        {
            if (ModelState.IsValid == false)
                return Page();

            _permisionService.updateRole(role);

            _permisionService.UpdatePermisionRole(role.RoleId,SelectedPermisions);

            return RedirectToPage("Index");
        }
    }
}
