﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.core.Security;
using TopLearn.core.Services.Intefaces;

namespace TopLearn.Web.Pages.admin.Discount
{
    [PermisionChecker(1007)]
    public class CreateDiscountModel : PageModel
    {
        private IOrderService _orderService;

        public CreateDiscountModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [BindProperty]
        public Datalayar.Entities.Order.Discount Discount { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost(string stDate = null ,string edDate = null)
        {
            if (stDate != null)
            {

                string[] StartDate=stDate.Split("/");

               Discount.StartDate=new DateTime(int.Parse(StartDate[0]),int.Parse(StartDate[1]),int.Parse(StartDate[2]),
               new PersianCalendar());
            }

            if (edDate != null)
            {
                string[] EndDate = edDate.Split("/");

                Discount.EndDate = new DateTime(int.Parse(EndDate[0]), int.Parse(EndDate[1]), int.Parse(EndDate[2]),
                    new PersianCalendar());
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_orderService.IsExistDiscountCode(Discount.DiscountCode))
            {
                ModelState.AddModelError("Discount.DiscountCode", "کد وارد شده تکراری میباشد");
                return Page();
            }

            _orderService.AddDiscount(Discount);

          return  RedirectToPage("Index");
        }

        public IActionResult OnGetCheckCode(string code)
        {
            return Content(_orderService.IsExistDiscountCode(code).ToString());
        }
    }
}
