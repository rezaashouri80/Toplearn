using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TopLearn.Datalayar.Entities.Course;

namespace TopLearn.Datalayar.Entities.User
{
  public  class User
    {
        public User()
        {
            
        }
        [Key]
        public int UserId  { get; set; }
       
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200,ErrorMessage = "{0} نمیتواند بیشتر {1} کاراکتر باشد")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
         [MaxLength(200,ErrorMessage = "{0} نمیتواند بیشتر {1} کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده نا معتبر است")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
         [MaxLength(200,ErrorMessage = "{0} نمیتواند بیشتر {1} کاراکتر باشد")]
        public string Password { get; set; }

        [Display(Name = "کد فعالسازی")]
         [MaxLength(200,ErrorMessage = "{0} نمیتواند بیشتر {1} کاراکتر باشد")]
        public string Activeocde { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime rigesterDate { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        [Display(Name = "آواتار")]
         [MaxLength(200,ErrorMessage = "{0} نمیتواند بیشتر {1} کاراکتر باشد")]
        public string UserAvatar { get; set; }

        public bool IsDelete { get; set; }




        #region Relations

        public virtual List<UserRole> UserRoles { get; set; }

        public virtual List<Wallet.Wallet> Wallets { get; set; }

        public virtual List<Course.Course> Courses { get; set; }

        public virtual List<Order.Order> Orders { get; set; }

        public List<UserCourse> UserCourse { get; set; }

        public List<CourseVote> CourseVotes { get; set; }

        public List<CourseComments> CourseComments { get; set; }

        #endregion

    }
}
