using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Server.HttpSys;
using TopLearn.core.Convertor;
using TopLearn.core.DTOs;
using TopLearn.core.Generator;
using TopLearn.Core.Security;
using TopLearn.core.Senders;
using TopLearn.core.Services.Intefaces;
using TopLearn.Datalayar.Entities.User;

namespace TopLearn.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        private IViewRenderService _viewRender;

        public AccountController(IUserService userService, IViewRenderService viewRender)
        {
            _userService = userService;
            _viewRender = viewRender;
        }


        #region Login
        [Route("Login")]
        public IActionResult Login(bool Editprofile=false,string ReturnUrl="")
        {
            ViewBag.Editprofile = Editprofile;
            ViewBag.Url = ReturnUrl;
            return View();
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login(LoginViewModel login,string Url="")
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = _userService.LoginUser(login);

            if (user!=null)
            {
                if (user.IsActive)
                {
                    //TODO Login User

                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties()
                    {
                        IsPersistent = login.RememberMe
                    };

                    HttpContext.SignInAsync(principal, properties);

                    if (!string.IsNullOrEmpty(Url))
                    {
                        return Redirect(Url);
                    }

                    ViewBag.IsSucces = true;


                    return View();
                }
                else
                {
                    ModelState.AddModelError("Email", "حساب کاربری شما فعال نمی باشد");
                    
                }
            }
            
            ModelState.AddModelError("Email","کاربری با مشخصات بالا یافت نشد");
            
            return View(login);
        }

        #endregion

        #region Register

        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("register")]
        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }


            if (!_userService.IsExistEmail(FixedText.FixedEmail(register.Email)))
            {
                ModelState.AddModelError("Email","ایمیل تکراری است");
                return View(register);
            }

            if (!_userService.IsExistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "  نام کاربری تکراری است");
                return View(register);
            }

            //ToDo Register User

            Datalayar.Entities.User.User user = new User()
            {
                Activeocde = NameGenerator.GenerateUniqCode(),
                IsActive = false,
                Email = FixedText.FixedEmail(register.Email),
                Password = PasswordHelper.EncodePasswordMd5(register.Password),
                rigesterDate = DateTime.Now,
                UserAvatar = "Defult.jpg",
                UserName = register.UserName
            };

            _userService.AddUser(user);

            //TODO send Email

            #region Send Activation Email

            string body = _viewRender.RenderToStringAsync("_ActiveEmail", user);

            

            SendEmail.Send(user.Email,"فعال سازی",body);
           
            #endregion

            return View("SuccesRegister", user);

        }

        #endregion

        #region Active Account

        public IActionResult ActiveAccount(string id)
        {
            ViewBag.IsActive = _userService.ActiveAccount(id);

          return View();

       
        }


        #endregion

        #region Logout

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        

        #endregion

        #region ForgetPassword
        [Route("ForgetPassword")]
      
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [Route("ForgetPassword")]
        [HttpPost]
        public IActionResult ForgetPassword(ForgetpasswordViewModel forgot)
        {
            if (!ModelState.IsValid)
                return View(forgot);

            string fixedemail = FixedText.FixedEmail(forgot.Email);

            User user = _userService.GetUserBYEmail(fixedemail);

            if (user == null)
            {
                ModelState.AddModelError("Email", "کاربری با ایمیل وارد شده یافت نشد");
                return View(forgot);
            }

            string bodymail = _viewRender.RenderToStringAsync("_ForgetPassword", user);

            SendEmail.Send(user.Email, "بازیابی کلمه عبوری", bodymail);
            ViewBag.IsSucces = true;



            return View();

          
        }




        #endregion


        #region ResetPassword

        public IActionResult Resetpassword(string id)
        {
            return View(new ResetpasswordViewModel
            {
                Activecode = id
            }) ;
        }

        [HttpPost]
        public IActionResult Resetpassword(ResetpasswordViewModel reset)
        {
            if (!ModelState.IsValid)
                return View(reset);

            User user = _userService.GetUSerByActivecode(reset.Activecode);

            if (user == null)
                return NotFound();

            string hashpassword = PasswordHelper.EncodePasswordMd5(reset.Password);

            user.Password = hashpassword;
            _userService.UpdateUser(user);

            return Redirect("/");

        }

        #endregion


        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
