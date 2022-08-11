using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TopLearn.core.Security;
using TopLearn.core.Services.Intefaces;

namespace TopLearn.Web.Pages.admin.Course
{
    [PermisionChecker(1004)]
    public class EditCourseModel : PageModel
    {
        private ICourseService _courseService;

        public EditCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty] 
        public Datalayar.Entities.Course.Course Course { get; set; }

        public void OnGet(int id)
        {
          Course =  _courseService.GetCourseById(id);

            var groups = _courseService.GetGroupForManageCourse();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text", Course.GroupId);

            var SubGroups = _courseService.GetSubGroupForManageCourse(Course.GroupId);
            ViewData["SubGroups"] = new SelectList(SubGroups, "Value", "Text",Course.SubGroup??0);

            var teachers = _courseService.GetTeachers();
            ViewData["Teachers"] = new SelectList(teachers, "Value", "Text",Course.TeacherId);

            var levels = _courseService.GetLevels();
            ViewData["Levels"] = new SelectList(levels, "Value", "Text",Course.LevelId);

            var status = _courseService.GetStatus();
            ViewData["Status"] = new SelectList(status, "Value", "Text",Course.StatusId);
        }

        public IActionResult OnPost(IFormFile imgCourseUp, IFormFile demoUp)
        {
            if (!ModelState.IsValid)
                return Page();


            _courseService.UpdateCourse(Course, imgCourseUp, demoUp);


            return RedirectToPage("Index");
        }
    }
}
