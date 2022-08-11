using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TopLearn.Datalayar.Entities.Permisions
{
   public class Permision
    {
        [Key]
        public int PermisionId { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "عنوان نقش")] 
        [MaxLength(200,ErrorMessage = "{0} نمیتواند بیشتر {1} کاراکتر باشد")]
        public string PermisionTitle { get; set; }

        public int? ParentId { get; set; }

        #region Realations

        [ForeignKey("ParentId")]
        public List<Permision> Permisions { get; set; }


        public List<RoleToPermision> RoleToPermisions { get; set; }
        #endregion
    }
}
