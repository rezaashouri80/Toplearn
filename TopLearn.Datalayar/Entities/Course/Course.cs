using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TopLearn.Datalayar.Entities.Order;

namespace TopLearn.Datalayar.Entities.Course
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        public int GroupId { get; set; }

        public bool IsDelete { get; set; }

        public int? SubGroup { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Required]
        public int LevelId { get; set; }

        [Display(Name = "عنوان دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر {1} کاراکتر باشد")]
        public string CourseTitle { get; set; }

        [Display(Name = "قیمت دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")] 
        public int CoursePrice { get; set; }

        [Display(Name = "شرح دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CourseDescription { get; set; }

         [MaxLength(600,ErrorMessage = "{0} نمیتواند بیشتر {1} کاراکتر باشد")]
        public string Tags { get; set; }

        [MaxLength(50)]
        public string ImageName { get; set; }

         [MaxLength(100,ErrorMessage = "{0} نمیتواند بیشتر {1} کاراکتر باشد")]
        public string DemoFileName { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        #region Relations
        [ForeignKey("TeacherId")]
        public User.User User { get; set; }

        [ForeignKey("GroupId")]
        public GroupCourse CourseGroup { get; set; }

        [ForeignKey("SubGroup")]
        public GroupCourse Group { get; set; }

        [ForeignKey("StatusId")]
        public CourseStatus CourseStatus { get; set; }

        [ForeignKey("LevelId")]
        public CourseLevel CourseLevel { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

        public List<CourseEpisode> CourseEpisodes { set; get; }

        public List<UserCourse> UserCourses { get; set; }

        public List<CourseComments> CourseComments { get; set; }

        public List<CourseVote> CourseVotes { get; set; }
        #endregion
    }
}
