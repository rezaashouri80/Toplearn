using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopLearn.core.Services.Intefaces;
using TopLearn.Datalayar.Context;
using TopLearn.Datalayar.Entities.Permisions;
using TopLearn.Datalayar.Entities.User;

namespace TopLearn.core.Services.Classes
{
   public class PermisionService:IPermisionService
   {
       private TopLearnContext _context;

       public PermisionService(TopLearnContext context)
       {
           _context = context;
       }

       public List<Role> GetRoles()
       {
         return  _context.Roles.ToList();
       }

       public void AddUserRoles(List<int> RoleId, int UserId)
       {
           foreach (int roleid in RoleId)
           {
               _context.UserRoles.Add(new UserRole()
               {
                   RoleId = roleid,
                   UserId = UserId
               });
           }

           _context.SaveChanges();
       }

       public int AddRole(Role role)
       {
           _context.Roles.Add(role);
           _context.SaveChanges();

           return role.RoleId;
       }

       public Role GetRoleById(int RoleId)
       {
           return _context.Roles.Find(RoleId);
       }

       public void updateRole(Role role)
       {
           _context.Roles.Update(role);
           _context.SaveChanges();
       }

       public void DeleteRole(Role role)
       {
           role.IsDelete = true;
           updateRole(role);
       }

       public void EditRoles(List<int> RoleId, int UserId)
       {
           _context.UserRoles.Where(r=>r.UserId==UserId).ToList()
               .ForEach(r=> _context.Remove(r) );

           AddUserRoles(RoleId,UserId);
       }

       public List<Permision> GetAllPermision()
       {
           return _context.Permisions.ToList();
       }

       public void AddPermisionToRoles(int RoleId, List<int> permisionsId)
       {
           

           foreach (int PerimisionId in permisionsId)
           {
               _context.RoleToPermisions.Add(new RoleToPermision()
               {
                   PermisionId = PerimisionId,
                   RoleId = RoleId
               });
           }

           _context.SaveChanges();
       }

       public List<int> PermisionRoles(int roleId)
       {
           return _context.RoleToPermisions
               .Where(p => p.RoleId == roleId)
               .Select(p => p.PermisionId)
               .ToList();
       }

       public void UpdatePermisionRole(int roleId, List<int> permisons)
       {
           _context.RoleToPermisions.Where(p=> p.RoleId==roleId)
               .ToList().ForEach(p=> _context.RoleToPermisions.Remove(p));

           AddPermisionToRoles(roleId,permisons);
       }

       public bool CheckPermision(int permisionId, string username)
       {
           int userId = _context.Users.Single(u => u.UserName == username).UserId;

           List<int> UserRoles = _context.UserRoles.Where(r => r.UserId == userId)
               .Select(r => r.RoleId).ToList();

           if (!UserRoles.Any())
           {
               return false;
           }

     
               List<int> Permisions = _context.RoleToPermisions.Where(p => p.PermisionId == permisionId).Select(p => p.RoleId)
                   .ToList();

               return Permisions.Any(p => UserRoles.Contains(p));

       }
   }
}
