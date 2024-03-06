using Discount.API.Entities;

namespace Discount.API.Repositories.Interfaces
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscount(string productName);
        Task CreateDiscount(Coupon coupon);
        Task UpdateDiscount(Coupon coupon);
        Task DeleteDiscount(string productName);
    }
}
