using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.Datalayar.Entities.Course
{
   public class CourseVote
    {
        [Key]
        public int VoteId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public bool Vote { get; set; }
        [Required]
        public DateTime VoteDate { get; set; } = DateTime.Now;


        #region Relations

        public Course Course { get; set; }

        public User.User User { get; set; }

        #endregion
    }
}
