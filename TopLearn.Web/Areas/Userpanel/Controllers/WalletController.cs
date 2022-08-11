using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TopLearn.core.DTOs;
using TopLearn.core.Services.Intefaces;

namespace TopLearn.Web.Areas.Userpanel.Controllers
{
    [Area("Userpanel")]
    [Authorize]
    public class WalletController : Controller
    {
        private IUserService _userService;

        public WalletController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("Userpanel/wallet")]
        public IActionResult Index()
        {
            ViewBag.ListWallet = _userService.GetWalletUser(User.Identity.Name);
            return View();
        }

        [Route("Userpanel/wallet")]
        [HttpPost]
        public IActionResult Index(ChargeWalletViewModel charge)
        {
            string username = User.Identity.Name;

            if (!ModelState.IsValid)
            {
                ViewBag.ListWallet = _userService.GetWalletUser(username);
                return View(charge);
            }

         int walletid=_userService.ChargeWallet(username, charge.Amount, "شارژ کیف پول");

            
            #region  Online Payment

            var payment = new ZarinpalSandbox.Payment(charge.Amount);
            var res = payment.PaymentRequest("شارژ کیف پول", "https://localhost:44338/OnlinePayment/" + walletid);

            if (res.Result.Status==100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }

                #endregion
            return null;
        }
    }
}
