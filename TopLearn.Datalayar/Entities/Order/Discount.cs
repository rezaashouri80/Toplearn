using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.Datalayar.Entities.Order
{
   public class Discount
    {
        [Key]
        public int DiscountId { get; set; }

        [Display(Name = "کد تخفیف")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100,ErrorMessage = "{0} نمیتواند بیشتر {1} کاراکتر باشد")]
        public string DiscountCode { get; set; }
        
        public int? UsableCount { get; set; }

        [Display(Name = "تعداد کد تخفیف ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int DiscountPercent { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }


        #region Relations

        public List<Order> Orders { get; set; }

        #endregion

    }
}
