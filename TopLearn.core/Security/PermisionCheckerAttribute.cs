using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TopLearn.core.Services.Intefaces;

namespace TopLearn.core.Security
{
    public class PermisionCheckerAttribute : AuthorizeAttribute,IAuthorizationFilter
    {
        private int _permisionId = 0;

        private IPermisionService _permisionService;

        public PermisionCheckerAttribute(int permisionId)
        {
            _permisionId = permisionId;
        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string username = context.HttpContext.User.Identity.Name;

                _permisionService =
                    (IPermisionService) context.HttpContext.RequestServices.GetService(typeof(IPermisionService));

                if (!_permisionService.CheckPermision(_permisionId, username))
                {
                    context.Result = new ForbidResult();
                }

            }
            else
            {
                context.Result=new RedirectResult("/Login?"+ context.HttpContext.Request.Path);
            }
            
        }
    }
}
