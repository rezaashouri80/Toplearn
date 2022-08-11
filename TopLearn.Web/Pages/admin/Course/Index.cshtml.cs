using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.core.DTOs.Course;
using TopLearn.core.Security;
using TopLearn.core.Services.Intefaces;

namespace TopLearn.Web.Pages.Course
{
    [PermisionChecker(1002)]
    public class IndexModel : PageModel
    {
        private ICourseService _courseService;

        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public List<ShowCourseForAdminViewModel> listCourse { set; get; }

        public void OnGet()
        {
            listCourse = _courseService.showCourseForAdmin();
        }
    }
}
