using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;
using TopLearn.Datalayar.Entities.User;

namespace TopLearn.core.DTOs
{
    public class ShowUsersForAdminViewModel
    {
        public List<User> Users { get; set; }

        public int PageCount { get; set; }

        public int  CurrentPage { get; set; }
    }

    public class CreateUserViewModel
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

        public IFormFile UserAvatar { get; set; }

       // public List<int> UserRoles { get; set; } 

    }

    public class EditUserForAdminViewModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر {1} کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده نا معتبر است")]
        public string Email { get; set; }

        public string ImageName { get; set; }

        public IFormFile Avatar { get; set; }

        public List<int> UseerRoles { get; set; }

        [Display(Name = "کلمه عبور")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر {1} کاراکتر باشد")]
        public string Password { get; set; }
    }
}
