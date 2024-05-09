using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Basket.API.Services.Interfaces;
using EventBus.Interfaces.Bus;

namespace Basket.API.Services;

/*
public class BasketService : IBasketService, IIntegrationEventHandler<UserLoggedInIntegrationEvent>, IIntegrationEventHandler<UserLoggedOutIntegrationEvent>
{
    private readonly IEventBus _eventBus;
    private readonly IBasketRepository _basketRepository; // Assuming you have a repository for handling basket operations

    public BasketService(IEventBus eventBus, IBasketRepository basketRepository)
    {
        _eventBus = eventBus;
        _basketRepository = basketRepository;
        _eventBus.Subscribe<UserLoggedInIntegrationEvent>(this);
        _eventBus.Subscribe<UserLoggedOutIntegrationEvent>(this);
    }

    public void Handle(UserLoggedInIntegrationEvent @event)
    {
        // Create a new basket for the user
        _basketRepository.GetBasket(@event.Username);
        _basketRepository.UpdateBasket(basket);
    }

    public void Handle(UserLoggedOutIntegrationEvent @event)
    {
        // Delete the user's basket
        _basketRepository.DeleteBasket(@event.Username);
    }
}
*/
