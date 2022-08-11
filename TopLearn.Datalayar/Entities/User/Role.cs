using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TopLearn.Datalayar.Entities.Course;
using TopLearn.Datalayar.Entities.Permisions;

namespace TopLearn.Datalayar.Entities.User
{
   public class Role
    {
        public Role()
        {
            

        }
        
        [Key]
        public int RoleId { get; set; }
       
        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200,ErrorMessage = "{0} نمیتواند بیشتر {1} کاراکتر باشد")]
        public string RoleTittle { get; set; }

        public bool IsDelete { get; set; }

        #region Relations

        public virtual List<UserRole> UserRoles { get; set; }

        public List<RoleToPermision> RoleToPermisions { get; set; }

        public List<UserCourse> UserCourse { get; set; }
        #endregion
    }
}
