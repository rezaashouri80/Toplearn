using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TopLearn.Datalayar.Entities.Course
{
    public class GroupCourse
    {
        [Key]
        public int GroupId { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر {1} کاراکتر باشد")]
        public string GroupTitle { get; set; }

        [Display(Name = "حذف شده؟")]
        public bool IsDelete { get; set; }
       
        [Display(Name = "گروه اصلی")]
        public int? ParentId { get; set; }

        #region Relations
        [ForeignKey("ParentId")]
        public List<GroupCourse> GroupCourses { get; set; }

        [InverseProperty("CourseGroup")]
        public List<Course> Courses { get; set; }

        [InverseProperty("Group")]
        public List<Course> SubGroup { get; set; }


        #endregion
    }
}
