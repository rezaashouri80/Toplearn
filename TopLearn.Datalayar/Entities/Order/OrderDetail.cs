using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TopLearn.Datalayar.Entities.Order
{
   public class OrderDetail
    {
        [Key]
        public int DetailId { get; set; }
      
        [Required]
        public int OrderId { get; set; }
       
        [Required]
        public int CourseId { get; set; }
      
        [Required]
        public int Price { get; set; }
        
        [Required]
        public int Count { get; set; }


        #region Relations
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course.Course Course { get; set; }

        #endregion
    }
}
