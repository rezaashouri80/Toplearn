using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TopLearn.Datalayar.Entities.User;

namespace TopLearn.Datalayar.Entities.Permisions
{
   public class RoleToPermision
    {
        [Key]
        public int RP_Id { get; set; }

        public int RoleId { get; set; }

        public int PermisionId { get; set; }

        #region Relations

        public Permision Permision { get; set; }

        public Role Role { get; set; }

        #endregion
    }
}
