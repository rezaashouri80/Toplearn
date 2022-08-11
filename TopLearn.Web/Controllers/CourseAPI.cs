using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Datalayar.Context;

namespace TopLearn.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseApi : ControllerBase
    {
        private TopLearnContext _context;

        public CourseApi(TopLearnContext context)
        {
            _context = context;
        }

        [Produces("application/json")]
        [HttpGet("search")]
        public async Task<IActionResult> Search()
        {
            try
            {
                string filter = HttpContext.Request.Query["term"].ToString();
                var courseTitle = _context.Courses
                    .Where(c => c.CourseTitle.Contains(filter))
                    .Select(c => c.CourseTitle).ToList();

                return Ok(courseTitle) ;
            }
            catch
            {
                return  BadRequest();
            }
        }
    }
}
