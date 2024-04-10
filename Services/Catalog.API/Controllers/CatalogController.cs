using Catalog.Data;
using Catalog.Entities;
using Catalog.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        public ActionResult Get(int take = 10, int skip = 0, string sortBy = "Id")
        {
            if (take < 0 || skip < 0)
            {
                return BadRequest("Parameters take and skip should be positive numbers.");
            }

            try
            {
                var products = _catalogContext.Products.AsQueryable();

                switch (sortBy)
                {
                    case "Name":
                        products = products.OrderBy(p => p.Name);
                        break;
                    case "Category":
                        products = products.OrderBy(p => p.Category);
                        break;
                    default:
                        products = products.OrderBy(p => p.Id);
                        break;
                }

                return Ok(products.Skip(skip).Take(take));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllAsync()
        {
            try
            {
                var result = await _productRepository.GetProducts();
                _logger.LogInformation($"Отримали всі фільми з бази даних!");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetAllAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        [HttpGet("GetProductByIdAsync {id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductByIdAsync(Guid id)
        {
            try
            {
                var result = await _productRepository.GetProductById(id);
                _logger.LogInformation($"Отримали всі фільми з бази даних!");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetProductByIdAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        [HttpGet]
        [Route("[action]/{name}", Name = "GetProductsByNameAsync")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductsByNameAsync(string name)
        {
            try
            {
                var result = await _productRepository.GetProductsByName(name);
                _logger.LogInformation($"Отримали всі фільми з бази даних!");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetProductsByNameAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        [Route("[action]/{category}", Name = "GetProductsByCategoryAsync")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductsByCategoryAsync(string category)
        {
            try
            {
                var result = await _productRepository.GetProductsByCategory(category);
                _logger.LogInformation($"Отримали всі фільми з бази даних!");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetProductsByCategoryAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        // CatalogController.cs

        [HttpGet]
        [Route("[action]", Name = "SearchProducts")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> SearchProducts([FromQuery] string name = null, [FromQuery] string category = null)
        {
            try
            {
                var result = await _productRepository.SearchProducts(name, category);
                _logger.LogInformation($"Отримали всі фільми з бази даних!");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі SearchProducts() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        
        [HttpPost("CreateProductAsync")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateProductAsync([FromBody] Product product)
        {
            try
            {
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

        [HttpPut("UpdateProductAsync")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateProductAsync([FromBody] Product product)
        {
            try
            {
                await _productRepository.UpdateProduct(product);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі UpdateProductAsync - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        [HttpDelete("DeleteProductAsync {id}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
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
