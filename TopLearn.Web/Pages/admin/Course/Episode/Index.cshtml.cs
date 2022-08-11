using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.core.Security;
using TopLearn.core.Services.Intefaces;
using TopLearn.Datalayar.Entities.Course;

namespace TopLearn.Web.Pages.admin.Course.Episode
{
    [PermisionChecker(1005)]
    public class IndexModel : PageModel
    {
        private ICourseService _courseService;

        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public List<CourseEpisode> CourseEpisodes { get; set; }

        public void OnGet(int id)
        {
            CourseEpisodes = _courseService.GetAllEpisodes(id);
            ViewData["CourseId"] = id;
        }
    }
}
