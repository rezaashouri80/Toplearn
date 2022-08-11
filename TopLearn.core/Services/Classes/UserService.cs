using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TopLearn.core.Convertor;
using TopLearn.core.DTOs;
using TopLearn.core.Generator;
using TopLearn.Core.Security;
using TopLearn.Datalayar.Context;
using TopLearn.Datalayar.Entities.User;
using TopLearn.Datalayar.Entities.Wallet;

namespace TopLearn.core.Services.Intefaces
{
  public  class UserService:IUserService
  {
        private TopLearnContext _context;

        public UserService(TopLearnContext context)
        {
            _context = context;
        }

        public bool IsExistEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public bool IsExistUserName(string username)
        {
            return _context.Users.Any(u => u.UserName == username);
        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return user.UserId;
        }

        public User LoginUser(LoginViewModel login)
        {
            string hashpassword = PasswordHelper.EncodePasswordMd5(login.Password);
            string email = FixedText.FixedEmail(login.Email);

            return _context.Users.SingleOrDefault(x => x.Email == email && x.Password == hashpassword);
        }

        public bool ActiveAccount(string ActiveCode)
        {
            var user = _context.Users.SingleOrDefault(x => x.Activeocde == ActiveCode);

            if (user == null || user.IsActive == true)
                return false;

            user.IsActive = true;
            user.Activeocde = NameGenerator.GenerateUniqCode();
            _context.SaveChanges();
            return true;
        }

        public User GetUserBYEmail(string email)
        {
          return  _context.Users.SingleOrDefault(x => x.Email == email);
        }

        public User GetUSerByActivecode(string activecode)
        {
            return _context.Users.SingleOrDefault(x => x.Activeocde == activecode);
        }

        public void UpdateUser(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }

        public User GetUserByUserId(int userId)
        {
            return _context.Users.Find(userId);
        }

        public int GetUserIdByUsername(string username)
        {
            return _context.Users.Single(u => u.UserName == username).UserId;
        }

        public void SaveUserAvatar(IFormFile Avatar, User user)
        {

            user.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(Avatar.FileName);
           string imagepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", user.UserAvatar);

            using (FileStream stream = new FileStream(imagepath, FileMode.Create))
            {
                Avatar.CopyTo(stream);
            }

        }

        public void DeleteUserAvatar(string imageName)
        {
           string imagepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", imageName);
            if (File.Exists(imagepath))
            {
                File.Delete(imagepath);
            }
        }

        public void DeleteUser(int userId)
        {
            User user = GetUserByUserId(userId);
            user.IsDelete = true;
            UpdateUser(user);
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.SingleOrDefault(x => x.UserName == username);
        }

        public InformationUserViewModel GetInformationUser(string username)
        {
            User user = GetUserByUsername(username);

            InformationUserViewModel information = new InformationUserViewModel()
            {
                Email = user.Email,
                Registerdate = user.rigesterDate,
                Username = user.UserName,
                Wallet = BalanceUserWallet(user.UserName)
            };

            return information;
        }

        public InformationUserViewModel GetInformationUser(int UserId)
        {
            User user = GetUserByUserId(UserId);

            InformationUserViewModel information = new InformationUserViewModel()
            {
                Email = user.Email,
                Registerdate = user.rigesterDate,
                Username = user.UserName,
                Wallet = BalanceUserWallet(user.UserName)
            };

            return information;
        }

        public SideBarUserPanelViewModel GetSideBarUserPanelData(string username)
        {
            return _context.Users.Where(x => x.UserName == username)
                .Select(u => new SideBarUserPanelViewModel()
            {
                    Username = u.UserName,
                    Imagename = u.UserAvatar,
                    RegisterDate = u.rigesterDate
            }).Single();
        }

        public EditProfileViewModel getEditProfileData(string username)
        {
            return _context.Users.Where(x => x.UserName == username)
                .Select(x => new EditProfileViewModel()
                {
                    ImageName = x.UserAvatar,
                    Email = x.Email,
                    UserName = x.UserName
                }).Single();
        }

        public void EditProfile(string username,EditProfileViewModel profile)
        {
            if (profile.Avatar!=null)
            {
                string imagepath;
                if (profile.ImageName!="Defult.jpg")
                {
                    imagepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", profile.ImageName);
                    if (File.Exists(imagepath))
                    {
                        File.Delete(imagepath);
                    }

                }

                profile.ImageName  = NameGenerator.GenerateUniqCode() + Path.GetExtension(profile.Avatar.FileName);
                imagepath= Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", profile.ImageName);

                using (FileStream stream=new FileStream(imagepath,FileMode.Create))
                {
                    profile.Avatar.CopyTo(stream);
                }
            }

            User user = GetUserByUsername(username);
            user.UserName = profile.UserName;
            user.Email = profile.Email;
            user.UserAvatar = profile.ImageName;

            UpdateUser(user);
        }

        public bool CompareOldPassword(string username, string password)
        {
            string hasholdpassword = PasswordHelper.EncodePasswordMd5(password);

            return _context.Users.Any(x => x.UserName == username && x.Password == hasholdpassword);
        }

        public void ChaangePassword(string username, string NewPassword)
        {
            User user = GetUserByUsername(username);

            user.Password = PasswordHelper.EncodePasswordMd5(NewPassword);

            UpdateUser(user);
        }

        public int BalanceUserWallet(string username)
        {
            int UserId = GetUserIdByUsername(username);

            //واریزی ها
            var enter = _context.Wallets
                .Where(w => w.UserId == UserId && w.TypeId == 1 && w.IsPay)
                .Select(w => w.Amount).ToList();

            //برداشت ها
            var exit= _context.Wallets
                .Where(w => w.UserId == UserId && w.TypeId == 2 && w.IsPay)
                .Select(w => w.Amount).ToList();

            return (enter.Sum() - exit.Sum());
        }

        public List<walletViewModel> GetWalletUser(string username)
        {
            int userId = GetUserIdByUsername(username);

            return _context.Wallets
                .Where(w => w.UserId == userId && w.IsPay == true)
                .Select(w => new walletViewModel()
                {
                    Amount = w.Amount,
                    DateTime = w.CreateDate,
                    Description = w.Description,
                    TypeId = w.TypeId
                }).ToList();
        }

        public int ChargeWallet(string username, int amount, string description, bool Ispay = false)
        {
            Wallet wallet = new Wallet()
            {
                Amount = amount,
                CreateDate = DateTime.Now,
                Description = description,
                IsPay = Ispay,
                TypeId = 1,
                UserId = GetUserIdByUsername(username)
            };

            AddWallet(wallet);

            return wallet.WalletId;
        }

        public void AddWallet(Wallet wallet)
        {
            _context.Wallets.Add(wallet);
            _context.SaveChanges();
        }

        public Wallet GetWalletByWalletId(int walletid)
        {
            return _context.Wallets.Find(walletid);
        }

        public void UpdateWallet(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
            _context.SaveChanges();
        }

        public ShowUsersForAdminViewModel GetUsersForAdmin(int pageId = 1, string email = "", string username = "")
        {
            IQueryable<User> Result = _context.Users;

            if (!string.IsNullOrEmpty(email))
            {
                Result = Result.Where(u => u.Email.Contains(email));
            }

            if (!string.IsNullOrEmpty(username))
            {
                Result = Result.Where(u => u.UserName.Contains(username));
            }

            //Show Item in Page
            int take = 20;
            int skip = (pageId - 1) * take;

            ShowUsersForAdminViewModel list = new ShowUsersForAdminViewModel();

            list.CurrentPage = pageId;
            list.PageCount = Result.Count() / take;
            list.Users = Result.OrderByDescending(u => u.rigesterDate)
                .Skip(skip).Take(take).ToList();

            return list;

        }

        public ShowUsersForAdminViewModel GetDeleteUsers(int pageId = 1, string email = "", string username = "")
        {
            IQueryable<User> Result = _context.Users.IgnoreQueryFilters().Where(u=> u.IsDelete);

            if (!string.IsNullOrEmpty(email))
            {
                Result = Result.Where(u => u.Email.Contains(email));
            }

            if (!string.IsNullOrEmpty(username))
            {
                Result = Result.Where(u => u.UserName.Contains(username));
            }

            //Show Item in Page
            int take = 20;
            int skip = (pageId - 1) * take;

            ShowUsersForAdminViewModel list = new ShowUsersForAdminViewModel();

            list.CurrentPage = pageId;
            list.PageCount = Result.Count() / take;
            list.Users = Result.OrderByDescending(u => u.rigesterDate)
                .Skip(skip).Take(take).ToList();

            return list;
        }

        public int AddUserForAdmin(CreateUserViewModel user)
        {
            User addUser = new User()
            {
                UserName = user.UserName,
                Email = user.Email,
                Activeocde = NameGenerator.GenerateUniqCode(),
                Password = PasswordHelper.EncodePasswordMd5(user.Password),
                IsActive = true,
                rigesterDate = DateTime.Now
            };

            #region Save Avatar

            if (user.UserAvatar != null)
            {
                string imagepath = "";

                addUser.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(user.UserAvatar.FileName);
                imagepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", addUser.UserAvatar);

                using (FileStream stream = new FileStream(imagepath, FileMode.Create))
                {
                    user.UserAvatar.CopyTo(stream);
                }
            }

            #endregion


            return AddUser(addUser);
        }

        public EditUserForAdminViewModel GetUserForEdit(int userId)
        {
            return _context.Users.Where(u => u.UserId == userId)
                .Select(u => new EditUserForAdminViewModel()
                {
                    UserId = u.UserId,
                    Email = u.Email,
                    ImageName = u.UserAvatar,
                    UseerRoles = u.UserRoles.Select(r => r.RoleId).ToList(),
                    UserName = u.UserName
                }).Single();
        }

        public void EditUserFromAdmin(EditUserForAdminViewModel editUser)
        {
            User user = GetUserByUserId(editUser.UserId);
            user.Email = editUser.Email;
            if (!string.IsNullOrEmpty(editUser.Password))
            {
                user.Password = PasswordHelper.EncodePasswordMd5(editUser.Password);
            }

            if (editUser.Avatar != null)
            {
                if (editUser.ImageName != "Defult.jpg")
                {
                    DeleteUserAvatar(editUser.ImageName);
                }

                SaveUserAvatar(editUser.Avatar,user);
            }

            _context.Users.Update(user);
            _context.SaveChanges();

        }
  }
}
 