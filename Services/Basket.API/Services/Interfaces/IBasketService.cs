namespace Basket.API.Services.Interfaces;

public interface IBasketService
{
    void CreateBasket(string userId);
    void DeleteBasket(string userId);
}