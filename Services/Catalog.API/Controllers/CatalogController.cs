using System.Net;
using Catalog.BLL.Services.Interfaces.Interfaces;
using Catalog.DAL.Data;
using Catalog.DAL.Entities;
using Catalog.DAL.Entities.DTOs;
using Catalog.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogContext _catalogContext;
        
        private readonly ICatalogService _catalogService;
        
        private readonly ILogger<CatalogController> _logger;
        
        private readonly IProductRepository _productRepository;
        private readonly IActorRepository _actorRepository;

        public CatalogController(CatalogContext catalogContext, IProductRepository productRepository,
            ILogger<CatalogController> logger, ICatalogService catalogService,
            IActorRepository actorRepository)
        {
            _catalogContext = catalogContext;
            _productRepository = productRepository;
            _logger = logger;
            _catalogService = catalogService;
            _actorRepository = actorRepository;
        }
        //TODO change all methods is [Route...] style
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

        [HttpGet]
        [Route("[action]/{productName}", Name = "GetProductDetails")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProductDetails), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductDetails>> GetProductDetails(string productName)
        {
            try
            {
                var result = await _catalogService.GetProductDetails(productName);
                _logger.LogInformation($"Отримали всі фільми з бази даних!");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetProductDetails() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        
        [HttpGet("GetAllProduct")]
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
        
        [HttpGet("GetAllActors")]
        [ProducesResponseType(typeof(IEnumerable<Actor>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Actor>>> GetAllActorsAsync()
        {
            try
            {
                var result = await _actorRepository.GetAll();
                _logger.LogInformation($"Отримали усіх акторів з бази даних!");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetAllActorsAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        [HttpGet("GetProductsByActorName")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByActorName(string actorName)
        {
            try
            {
                var result = await _catalogService.GetProductsByActorName(actorName);
                _logger.LogInformation($"Отримали усі фільми актора {actorName} з бази даних!");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetProductsByActorName - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        [HttpPost("CreateProductActorRelation")]
        [ProducesResponseType(typeof(ProductDetails), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateProductActorRelation(string productName, string actorName)
        {
            try
            {
                await _catalogService.CreateProductActorRelation(productName, actorName);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі CreateProductActorRelation - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        
        [HttpPost("CreateProductDetail")]
        [ProducesResponseType(typeof(ProductDetails), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateProductDetail([FromBody] ProductDetails productDetails)
        {
            try
            {
                await _catalogService.CreateAllRelations(productDetails);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі CreateProductDetail - {ex.Message}");
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
