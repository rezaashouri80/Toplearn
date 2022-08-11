using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.core.DTOs
{
    public class RegisterViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر {1} کاراکتر باشد")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر {1} کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده نا معتبر است")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر {1} کاراکتر باشد")]
        public string Password { get; set; }

        [Display(Name = " تکرار کلمه عبور ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر {1} کاراکتر باشد")]
        [Compare("Password",ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string RePassword { get; set; }
    }

    public class LoginViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر {1} کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده نا معتبر است")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر {1} کاراکتر باشد")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgetpasswordViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر {1} کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده نا معتبر است")]
        public string Email { get; set; }
    }

    public class ResetpasswordViewModel
    {
        public string Activecode { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر {1} کاراکتر باشد")]
        public string Password { get; set; }

        [Display(Name = " تکرار کلمه عبور ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر {1} کاراکتر باشد")]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string RePassword { get; set; }
    }
}
