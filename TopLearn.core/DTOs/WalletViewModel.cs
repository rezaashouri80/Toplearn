using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.core.DTOs
{
    public class ChargeWalletViewModel
    {
        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }
    }

    public class walletViewModel
    {
        public int Amount { get; set; }

        public int TypeId { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }
    }

}
