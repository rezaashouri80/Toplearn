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
    [PermisionChecker(6)]
    public class IndexModel : PageModel
    {
        private IPermisionService _permisionService;

        public IndexModel(IPermisionService permisionService)
        {
            _permisionService = permisionService;
        }

        public List<Role> Roles { get; set; }

        public void OnGet()
        {
            Roles = _permisionService.GetRoles();
        }
    }
}
