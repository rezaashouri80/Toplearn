using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLearn.Datalayar.Entities.Order
{
  public  class Order
    {
        [Key]
        public int OredrId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int OrderSum { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }

        public int? DiscountId { get; set; }
      

        public bool IsFinally { get; set; }

        #region Realations

        public virtual User.User User { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }

        public Discount Discount { get; set; }
        #endregion
    }   
}
