using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.Datalayar.Entities.Wallet
{
  public  class Wallet
    {
        public Wallet()
        {
            
        }

        [Key]
        public int WalletId { get; set; }
        
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نوع")]
        public int TypeId { get; set; }
        
        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int UserId { get; set; }
        
        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }
         
        [MaxLength(200,ErrorMessage = "{0} نمیتواند بیشتر {1} کاراکتر باشد")]
         [Display(Name = "شرح")]
        public string Description { get; set; }
        
        [Display(Name = "پرداخت شده")]
        public bool IsPay { get; set; }
        
        [Display(Name = "تاریخ")]
        public DateTime CreateDate { get; set; }



        #region Realations

        public virtual User.User User { get; set; }

        public virtual WalletType WalletType { get; set; }
        

        #endregion


    }
}
