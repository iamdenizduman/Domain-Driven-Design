using MediatR;

namespace Order.Domain.Events
{
    public class OrderStartedDomainEvent : INotification
    {
        public string BuyerUserName { get; set; }
        public AggregateModels.OrderModels.Order Order { get; set; }

        public OrderStartedDomainEvent(string buyerUserName, AggregateModels.OrderModels.Order order)
        {
            BuyerUserName = buyerUserName;
            Order = order;
        }
    }
}
