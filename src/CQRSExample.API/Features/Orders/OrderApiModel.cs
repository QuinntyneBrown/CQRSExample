using CQRSExample.Core.Entities;

namespace CQRSExample.Features.Orders
{
    public class OrderApiModel
    {        
        public int OrderId { get; set; }
        public string Name { get; set; }

        public static OrderApiModel FromOrder(Order order)
        {
            var model = new OrderApiModel();
            model.OrderId = order.OrderId;
            model.Name = order.Name;
            return model;
        }
    }
}
