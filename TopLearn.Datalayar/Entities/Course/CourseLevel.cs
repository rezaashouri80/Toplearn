using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.Datalayar.Entities.Course
{
  public  class CourseLevel
    {
        [Key]
        public int LevelId { get; set; }

        [Required]
        [MaxLength(150)]
        public string LevelTitle { get; set; }

        #region Relations

        public List<Course> Courses { get; set; }

        #endregion
    }
}
