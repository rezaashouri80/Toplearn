using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.core.Services.Intefaces;
using TopLearn.Datalayar.Entities.Course;

namespace TopLearn.Web.Pages.admin.CourseGroup
{
    public class CreateGroupModel : PageModel
    {
        private ICourseService _courseService;

        public CreateGroupModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public GroupCourse GroupCourse { get; set; }

        public void OnGet(int? id)
        {
            GroupCourse = new GroupCourse()
            {
                ParentId = id
            };
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _courseService.AddGroup(GroupCourse);

            return RedirectToPage("Index");
        }
    }
}
