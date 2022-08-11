using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.core.Security;
using TopLearn.core.Services.Intefaces;

namespace TopLearn.Web.Pages.admin.Discount
{
    [PermisionChecker(1006)]
    public class IndexModel : PageModel
    {
        private IOrderService _orderService;

        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public List<Datalayar.Entities.Order.Discount> Discounts { get; set; }

        public void OnGet()
        {
            Discounts = _orderService.GetAllDiscounts();
        }
    }
}
