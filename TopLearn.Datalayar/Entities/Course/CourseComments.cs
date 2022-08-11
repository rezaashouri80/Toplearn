using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.Datalayar.Entities.Course
{
   public class CourseComments
    {
        [Key]
        public int CommentId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [MaxLength(700)]
        public string Comment { get; set; }

        public DateTime DateTime { get; set; }

        public bool IsAdminRead { get; set; }

        public bool IsDelete { get; set; }


        #region Relations

        public User.User User { get; set; }

        public Course Course { get; set; }


        #endregion
    }
}
