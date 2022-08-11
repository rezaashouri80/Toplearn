using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using TopLearn.core.DTOs;
using TopLearn.Datalayar.Entities.User;
using TopLearn.Datalayar.Entities.Wallet;

namespace TopLearn.core.Services.Intefaces
{
   public interface IUserService
   {
       public bool IsExistEmail(string email);

       public bool IsExistUserName(string username);

       public int AddUser(User user);

       public User LoginUser(LoginViewModel login);

       bool ActiveAccount(string ActiveCode);

        User GetUserBYEmail(string email);

        User GetUserByUsername(string username);

        User GetUSerByActivecode(string activecode);

        void UpdateUser(User user);

        User GetUserByUserId(int userId);

        int GetUserIdByUsername(string username);

        void SaveUserAvatar(IFormFile Avatar, User user);

        void DeleteUserAvatar(string imageName );

        void DeleteUser(int userId);
        #region User Panel

        InformationUserViewModel GetInformationUser(string username);

        InformationUserViewModel GetInformationUser(int UserId);


        SideBarUserPanelViewModel GetSideBarUserPanelData(string username);

        EditProfileViewModel getEditProfileData(string username);

        void EditProfile(string username,EditProfileViewModel profile);

        bool CompareOldPassword(string username, string password);

        void ChaangePassword(string username, string NewPassword);


        #endregion

        #region Wallet

        int BalanceUserWallet(string username);

        List<walletViewModel> GetWalletUser(string username);

        int ChargeWallet(string username, int amount, string description, bool Ispay = false);

        void AddWallet(Wallet wallet);

        Wallet GetWalletByWalletId(int walletid);

        void UpdateWallet(Wallet wallet);

        #endregion

        #region Admin panel


        ShowUsersForAdminViewModel GetUsersForAdmin(int pageId = 1, string email = "", string username = "");
       
        ShowUsersForAdminViewModel GetDeleteUsers(int pageId = 1, string email = "", string username = "");

        int AddUserForAdmin(CreateUserViewModel user);

        EditUserForAdminViewModel GetUserForEdit(int userId);

        void EditUserFromAdmin(EditUserForAdminViewModel editUser);

        #endregion
   }
}
