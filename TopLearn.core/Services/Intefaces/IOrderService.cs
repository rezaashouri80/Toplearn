using System;
using System.Collections.Generic;
using System.Text;
using TopLearn.core.DTOs.Order;
using TopLearn.Datalayar.Entities.Order;

namespace TopLearn.core.Services.Intefaces
{
   public interface IOrderService
   {
       int AddOrder(string username, int courseId);

       Order GetOrderForUserPanel(string username, int orderId);

       bool FinalyOrder(string username, int orderId);

       List<Order> GetOrders(string username);

       Order GetOrderById(int orderId);

       void UpdateOrder(Order order);

       bool IsUserBuyCourse(string username, int courseId);

       int CountUsersByCourse(int courseId);

        #region Discount

        DiscountType UseDiscount(int orderId, string code);

        void AddDiscount(Discount discount);

        List<Discount> GetAllDiscounts();

        Discount GetDiscountById(int discountId);

        void UpdateDiscount(Discount discount);

        bool IsExistDiscountCode(string code);

        #endregion
   }
}
