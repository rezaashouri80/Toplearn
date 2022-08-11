using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TopLearn.core.DTOs.Order;
using TopLearn.core.Services.Intefaces;

namespace TopLearn.Web.Areas.Userpanel.Controllers
{
    [Authorize]
    [Area("Userpanel")]
    public class OrderController : Controller
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View(_orderService.GetOrders(User.Identity.Name));
        }

        
        public IActionResult ShowOrder(int id,bool finaly=false,string type="")
        {

         
            var order=_orderService.GetOrderForUserPanel(User.Identity.Name, id);

            if (order==null)
            {
                return NotFound();
            }

            ViewBag.type = type;
            ViewBag.finaly = finaly;

            return View(order);
        }

        public IActionResult FinalyOrder(int id)
        {
            if (_orderService.FinalyOrder(User.Identity.Name, id))
            {
                return Redirect("/userpanel/order/showorder/" + id + "/?finaly=true");
            }

            return BadRequest();
        }

        public IActionResult UseDiscount(int orderId, string code)
        {
            DiscountType Type = _orderService.UseDiscount(orderId, code);

            return Redirect("/userpanel/order/showorder/" + orderId + "?type=" + Type.ToString());
        }
    }
}
