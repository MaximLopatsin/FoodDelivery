using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Dto;

namespace BLL.Interfaces
{
    public interface IOrderService
    {
        List<Order> GetOrders();

        List<Meal> GetMealsByOrderId(int id);

        Task CreateOrderAsync(Order order, List<OrderMeal> orderMeals);

        Task UpdateOrderAsync(Order order, List<OrderMeal> orderMeals);

        Task UpdateJustOrderAsync(Order order);

        Task DeleteOrderAsync(Order order);
    }
}
