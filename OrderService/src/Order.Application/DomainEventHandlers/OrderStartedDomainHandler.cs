using MediatR;
using Order.Application.Repository;
using Order.Domain.Events;

namespace Order.Application.DomainEventHandlers
{
    public class OrderStartedDomainHandler : INotificationHandler<OrderStartedDomainEvent>
    {
        private readonly IBuyerRepository _buyerRepository;

        public OrderStartedDomainHandler(IBuyerRepository buyerRepository)
        {
            _buyerRepository = buyerRepository;
        }

        public async Task Handle(OrderStartedDomainEvent notification, CancellationToken cancellationToken)
        {
            if (notification.Order.Username != null)
            {
                // _buyerRepository.Add(new Buyer(nofication.BuyerFirstName));
                await _buyerRepository.SaveChangesAsync();
            }
        }
    }
}
