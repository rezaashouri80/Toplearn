using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.core.Security;
using TopLearn.core.Services.Intefaces;
using TopLearn.Datalayar.Entities.Course;

namespace TopLearn.Web.Pages.admin.Course.Episode
{
    [PermisionChecker(1005)]
    public class CreateEpisodeModel : PageModel
    {
        private ICourseService _courseService;

        public CreateEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        } 

        [BindProperty]
        public CourseEpisode CourseEpisode { get; set; }

        public void OnGet(int id)
        {
            CourseEpisode = new CourseEpisode();
            CourseEpisode.CourseId = id;
        }

        public IActionResult OnPost(IFormFile File)
        {
            if (!ModelState.IsValid || File==null)
            {
                return Page();
            }

            if (_courseService.IsExisTFileName(File.FileName))
            {
                ViewData["IsExisTFileName"] = true;
                return Page();
            }

            _courseService.AddEpisode(CourseEpisode, File);

            return Redirect("/admin/course/episode/" + CourseEpisode.CourseId);

            
        }

    }
}
