using System;
using System.Collections.Generic;
using System.Text;
using TopLearn.Datalayar.Entities.Permisions;
using TopLearn.Datalayar.Entities.User;

namespace TopLearn.core.Services.Intefaces
{
    public interface IPermisionService
    {
        List<Role> GetRoles();

        void AddUserRoles(List<int> RoleId, int UserId);

        int AddRole(Role role);

        Role GetRoleById(int RoleId);

        void updateRole(Role role);

        void DeleteRole(Role role);

        void EditRoles(List<int> roleId, int UserId);

        #region Permision

        List<Permision> GetAllPermision();

        void AddPermisionToRoles(int roleId, List<int> permisionsId);

        List<int> PermisionRoles(int roleId);

        void UpdatePermisionRole(int roleId, List<int> permisons);

        bool CheckPermision(int permisionId, string username);

        #endregion
    }
}
