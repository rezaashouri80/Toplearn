using System;
using System.Collections.Generic;
using System.Text;

namespace TopLearn.core.DTOs.Course
{
   public class ShowCourseForAdminViewModel
    {
        public int CourseId { get; set; }

        public string ImageName { get; set; }

        public string CourseTitle { get; set; }

        public int EpisodeCount { get; set; }

        public string TeacherName { get; set; }
    }
}
