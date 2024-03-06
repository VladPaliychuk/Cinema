using Discount.API.Data;
using Discount.API.Entities;
using Discount.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DiscountController : Controller
{
    private readonly IDiscountRepository _repository;
    private readonly ILogger<DiscountContext> _logger;

    public DiscountController(IDiscountRepository repository, ILogger<DiscountContext> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger;
    }

    [HttpGet("{productName}", Name = "GetDiscountAsync")]
    [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Coupon>> GetDiscountAsync(string productName)
    {
        try
        {
            var discount = await _repository.GetDiscount(productName);
            return Ok(discount);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetDiscountAsync - {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Coupon>> CreateDiscountAsync([FromBody] Coupon coupon)
    {
        try
        {
            await _repository.CreateDiscount(coupon);
            return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName }, coupon);
        }
        catch(Exception ex)
        {
            _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі CreateDiscountAsync - {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
        }
    }

    [HttpPut]
    [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Coupon>> UpdateBasketAsync([FromBody] Coupon coupon)
    {
        try
        {
            await _repository.UpdateDiscount(coupon);
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі UpdateBasketAsync - {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
        }
    }

    [HttpDelete("{productName}", Name = "DeleteDiscountAsync")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<bool>> DeleteDiscountAsync(string productName)
    {
        try
        {
            await _repository.DeleteDiscount(productName);
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі DeleteDiscountAsync - {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
        }
    }
}