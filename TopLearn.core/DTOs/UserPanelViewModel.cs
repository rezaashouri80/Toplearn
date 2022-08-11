using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace TopLearn.core.DTOs
{
   public class InformationUserViewModel
    {
        public string Username { get; set; } 

        public string Email { get; set; }

        public DateTime Registerdate { get; set; }

        public int Wallet { get; set; }

        
    }

    public class SideBarUserPanelViewModel
    {
        public string Username { get; set; }

        public string Imagename { get; set; }

        public DateTime RegisterDate { get; set; }
    }

    public class EditProfileViewModel
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

        public string ImageName { get; set; }

        public IFormFile Avatar { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Display(Name = "کلمه عبور فعلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string OldPassword { get; set; }


        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string RePassword { get; set; }
    }

}
