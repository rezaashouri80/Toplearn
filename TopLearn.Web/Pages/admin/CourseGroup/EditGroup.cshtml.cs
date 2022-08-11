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
    public class EditGroupModel : PageModel
    {
        private ICourseService _courseService;

        public EditGroupModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public GroupCourse GroupCourse { get; set; }

        public void OnGet(int id)
        {
            GroupCourse = _courseService.GetGroupById(id);

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _courseService.UpdateGroup(GroupCourse);

            return RedirectToPage("Index");
        }
    }
}
