using Order.Domain.SeedWork;

namespace Order.Domain.AggregateModels.BuyerModels
{
    public class Buyer : BaseEntity
    {
        public string UserName { get; private set; }

        public Buyer(string userName)
        {
            if (userName != null)
            {
                UserName = userName;
            }
            else {
                throw new Exception("UserName is must be not null");
            }
        }
    }
}
