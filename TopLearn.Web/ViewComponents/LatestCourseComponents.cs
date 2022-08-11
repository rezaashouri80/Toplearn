using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TopLearn.core.Services.Intefaces;

namespace TopLearn.Web.ViewComponents
{
    public class LatestCourseComponents:ViewComponent
    {
        private ICourseService _courseService;

        public LatestCourseComponents(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult) View("LatestCourse",
                _courseService.GetCourseItem().Item1));
        }
    }
}
