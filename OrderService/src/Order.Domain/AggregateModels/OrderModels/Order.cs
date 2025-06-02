using Order.Domain.SeedWork;

namespace Order.Domain.AggregateModels.OrderModels
{
    public class Order : BaseEntity, IAggregateRoot
    {
        public DateTime OrderDate { get; set; }
        public string Description { get; set; }
        public int BuyerId { get; set; }
        public string OrderStatus{ get; set; }
        public Address Address { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
