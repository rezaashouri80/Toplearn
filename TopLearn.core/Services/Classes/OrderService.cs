using System;                          
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TopLearn.core.DTOs.Order;
using TopLearn.core.Services.Intefaces;
using TopLearn.Datalayar.Context;
using TopLearn.Datalayar.Entities.Course;
using TopLearn.Datalayar.Entities.Order;
using TopLearn.Datalayar.Entities.Wallet;

namespace TopLearn.core.Services.Classes
{
   public class OrderService:IOrderService
   {
       private TopLearnContext _context;
       private IUserService _userService;

       public OrderService(TopLearnContext context, IUserService userService)
       {
           _context = context;
           _userService = userService;
       }

        public int AddOrder(string username, int courseId)
        {
            int userId = _userService.GetUserIdByUsername(username);

            Course course = _context.Courses.Find(courseId);

            Order order = _context.Orders.FirstOrDefault(o => o.UserId == userId && o.IsFinally == false);

            if (order == null)
            {
                order = new Order()
                {
                    CreateDate = DateTime.Now,
                    IsFinally = false,
                    OrderSum = course.CoursePrice,
                    UserId = userId,
                    OrderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail()
                        {
                            Count = 1,
                            CourseId = courseId,
                            Price = course.CoursePrice
                        }
                    }

                };

                _context.Orders.Add(order);

            }
            else
            {
                OrderDetail detail = _context.OrderDetails
                    .FirstOrDefault(d => d.OrderId == order.OredrId && d.CourseId == courseId);

                if (detail != null)
                {
                    detail.Count += 1;
                    order.OrderSum += detail.Price;
                    _context.OrderDetails.Update(detail);
                }
                else
                {
                    detail = new OrderDetail()
                    {
                        Count = 1,
                        CourseId = courseId,
                        OrderId = order.OredrId,
                        Price = course.CoursePrice,
                    };

                    order.OrderSum += detail.Price;

                    _context.OrderDetails.Add(detail);
                }

                _context.Orders.Update(order);
            }

            _context.SaveChanges();

            return order.OredrId;
        }

        public Order GetOrderForUserPanel(string username, int orderId)
        {
            int userId = _userService.GetUserIdByUsername(username);

            return _context.Orders.Include(o => o.OrderDetails)
                .ThenInclude(od=>od.Course).FirstOrDefault(o => o.OredrId == orderId && o.UserId == userId   );
        }

        public bool FinalyOrder(string username, int orderId)
        {
            int userId = _userService.GetUserIdByUsername(username);

            Order order = _context.Orders.Include(o => o.OrderDetails)
                .ThenInclude(od => od.Course).FirstOrDefault(o => o.OredrId == orderId && o.UserId == userId);

            if (order==null || order.IsFinally==true)
            {
                return false;
            }

            if (_userService.BalanceUserWallet(username) >= order.OrderSum)
            {
                order.IsFinally = true;
                _userService.AddWallet(new Wallet()
                {
                    Amount = order.OrderSum,
                    CreateDate = DateTime.Now,
                    Description = "فاکتور شماره #"+order.OredrId,
                    IsPay = true,
                    TypeId = 2,
                    UserId = userId
                });

                _context.Orders.Update(order);

                foreach (var detail in order.OrderDetails)
                {
                    _context.UserCourses.Add(new UserCourse()
                    {
                        CourseId = detail.CourseId,
                        UserId = userId
                    });
                }

                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public List<Order> GetOrders(string username)
        {
            int userId = _userService.GetUserIdByUsername(username);

            return _context.Orders.Where(o => o.UserId == userId).ToList();
        }

        public bool IsUserBuyCourse(string username, int courseId)
        {
            int userId = _userService.GetUserIdByUsername(username);

            return _context.UserCourses.Any(u => u.UserId == userId && u.CourseId == courseId);
        }

        public int CountUsersByCourse(int courseId)
        {
            return _context.UserCourses.Count(c => c.CourseId == courseId);
        }

        public DiscountType UseDiscount(int orderId, string code)
        {
            var discount = _context.Discounts.SingleOrDefault(d => d.DiscountCode == code);

            if (discount==null)
            {
                return DiscountType.NotFound;
            }

            if (discount.StartDate != null && discount.StartDate>DateTime.Now)
            {
                return DiscountType.ExpiredDate;
            }

            if (discount.EndDate != null && discount.EndDate < DateTime.Now)
            {
                return DiscountType.ExpiredDate;
            }

            if (discount.UsableCount <1)
            {
                return DiscountType.ExpiredDate;
            }

            var order = GetOrderById(orderId);

            if (order.DiscountId != null)
            {
                return DiscountType.Same;
            }

            order.DiscountId = discount.DiscountId;

            int percent = order.OrderSum * discount.DiscountPercent / 100;

            order.OrderSum = order.OrderSum - percent;

            UpdateOrder(order);

            if (discount.UsableCount != null)
            {
                discount.UsableCount -= 1;
            }

            _context.Discounts.Update(discount);
            _context.SaveChanges();

            return DiscountType.Success;

        }

        public void AddDiscount(Discount discount)
        {
            _context.Discounts.Add(discount);
            _context.SaveChanges();
        }

        public List<Discount> GetAllDiscounts()
        {
            return _context.Discounts.ToList();
        }

        public Discount GetDiscountById(int discountId)
        {
            return _context.Discounts.Find(discountId);
        }

        public void UpdateDiscount(Discount discount)
        {
            _context.Discounts.Update(discount);
            _context.SaveChanges();
        }

        public bool IsExistDiscountCode(string code)
        {
            return _context.Discounts.Any(d => d.DiscountCode == code);
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders.Find(orderId);
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }
   }
}
