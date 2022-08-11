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
    public class DeleteEpisodeModel : PageModel
    {
        private ICourseService _courseService;

        public DeleteEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [BindProperty]
        public CourseEpisode CourseEpisode { get; set; }

        public void OnGet(int id)
        {
            CourseEpisode = _courseService.GetEpisodeById(id);
        }

        public IActionResult OnPost()
        {
            int CourseId = CourseEpisode.CourseId;
            _courseService.DeleteEpisode(CourseEpisode.EpisodeId);

            return Redirect("/admin/course/episode/" + CourseId);
        }
    }
}
