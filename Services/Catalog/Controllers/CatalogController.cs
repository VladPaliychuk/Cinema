using Catalog.Data;
using Catalog.Entities;
using Catalog.Repositories;
using Catalog.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogContext _catalogContext;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(CatalogContext catalogContext, IProductRepository productRepository,
            ILogger<CatalogController> logger)
        {
            _catalogContext = catalogContext;
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet("GetTakeSkip")]
        public ActionResult Get(int take = 10, int skip = 0)
        {
            return Ok(_catalogContext.Products.OrderBy(p => p.Id).Skip(skip).Take(take));
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllAsync()
        {
            try
            {
                var result = await _productRepository.GetProducts();
                _logger.LogInformation($"Отримали всі пости з бази даних!");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetAllAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        [HttpPost("CreateProductAsync")]
        public async Task<ActionResult> CreateProductAsync([FromBody] Product product)
        {
            try
            {
                if (product == null)
                {
                    _logger.LogInformation($"Ми отримали пустий json зі сторони клієнта");
                    return BadRequest("Обєкт івенту є null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                    return BadRequest("Обєкт івенту є некоректним");
                }

                await _productRepository.CreateProduct(product);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі CreateProductAsync - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        [HttpDelete("DeleteProductAsync {id}")]
        public async Task<ActionResult> DeleteProductAsync(Guid id)
        {
            try
            {
                await _productRepository.DeleteProduct(id);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Транзакція сфейлилась! Щось пішло не так у методі DeleteProductAsync - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
    }
}
