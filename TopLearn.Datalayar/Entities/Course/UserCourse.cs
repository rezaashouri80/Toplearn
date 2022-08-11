using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.Datalayar.Entities.Course
{
   public class UserCourse
    {
        [Key]
        public int UC_Id { get; set; }

        public int CourseId { get; set; }

        public int UserId { get; set; }


        
        public Course Course { get; set; }

        public User.User User { get; set; }


    }
}
