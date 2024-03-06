using Discount.API.Data;
using Discount.API.Entities;
using Discount.API.Exeptions;
using Discount.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Discount.API.Repositories;

public class DiscountRepository : IDiscountRepository
{
    protected readonly DiscountContext _context;

    public DiscountRepository(DiscountContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Coupon> GetDiscount(string productName)
    {
        var coupon = await _context.Coupones
                .FirstOrDefaultAsync(c => c.ProductName == productName)
               ?? throw new EntityNotFoundException($"Coupon with productName {productName} not found.");
        return coupon;
    }

    public async Task CreateDiscount(Coupon coupon)
    {
        if (coupon == null)
        {
            throw new ArgumentNullException($"Coupon entity must not be null");
        }
        coupon.Id = Guid.NewGuid();
        _context.Set<Coupon>().Add(coupon);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateDiscount(Coupon coupon)
    {
        var existingCoupon = await _context.Coupones
            .FirstOrDefaultAsync(c => c.ProductName == coupon.ProductName)
            ?? throw new EntityNotFoundException($"Coupon not found");
        _context.Entry(coupon).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteDiscount(string productName)
    {
        var couponToDelete = await _context.Coupones
            .FirstOrDefaultAsync(c => c.ProductName == productName)
                             ?? throw new EntityNotFoundException($"Coupon not found");

        _context.Coupones.Remove(couponToDelete);
        await _context.SaveChangesAsync();
    }
}