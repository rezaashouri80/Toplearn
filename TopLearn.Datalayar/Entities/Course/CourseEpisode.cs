using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.Datalayar.Entities.Course
{
   public class CourseEpisode
    {
        [Key]
        public int EpisodeId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
         [MaxLength(200,ErrorMessage = "{0} نمیتواند بیشتر {1} کاراکتر باشد")]
        public string EpisodeTitle { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CourseId { get; set; }

        [Display(Name = "زمان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public TimeSpan EpisodeTime { get; set; }

        [Display(Name = "فایل")]
        public string EpisodeFileName { get; set; }

        [Display(Name = "رایگان")]
        public bool IsFree { get; set; }



        public Course Course { get; set; }
    }
}
