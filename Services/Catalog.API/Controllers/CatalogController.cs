﻿using System.Net;
using Catalog.BLL.DTOs;
using Catalog.BLL.Services.Interfaces;
using Catalog.DAL.Data;
using Catalog.DAL.Entities;
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
        private readonly IPdfService _pdfService;
        
        private readonly ILogger<CatalogController> _logger;
        
        private readonly IProductRepository _productRepository;
        private readonly IActorRepository _actorRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IDirectorRepository _directorRepository;
        private readonly IScreeningRepository _screeningRepository;

        public CatalogController(CatalogContext catalogContext, IProductRepository productRepository,
            ILogger<CatalogController> logger, ICatalogService catalogService, IPdfService pdfService,
            IActorRepository actorRepository,
            IGenreRepository genreRepository,
            IDirectorRepository directorRepository,
            IScreeningRepository screeningRepository)
        {
            _catalogContext = catalogContext;
            _productRepository = productRepository;
            _logger = logger;
            _catalogService = catalogService;
            _pdfService = pdfService;
            _actorRepository = actorRepository;
            _genreRepository = genreRepository;
            _directorRepository = directorRepository;
            _screeningRepository = screeningRepository;
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
        [ProducesResponseType(typeof(ProductDetailsDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductDetailsDto>> GetProductDetails(string productName)
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
        
        [HttpPost("CreateProductDetail")]
        [ProducesResponseType(typeof(ProductDetailsDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateProductDetail([FromBody] ProductDetailsDto productDetailsDto)
        {
            try
            {
                await _catalogService.CreateAllRelations(productDetailsDto);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі CreateProductDetail - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        
        [HttpGet("GetAllProducts")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
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
        
        [HttpGet("GetAllActors")]
        [ProducesResponseType(typeof(IEnumerable<Actor>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Actor>>> GetAllActors()
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
        
        [HttpGet("GetAllGenres")]
        [ProducesResponseType(typeof(IEnumerable<Genre>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Genre>>> GetAllGenres()
        {
            try
            {
                var result = await _genreRepository.GetAll();
                _logger.LogInformation($"Отримали усі жанри з бази даних!");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetAllGenresAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        
        [HttpGet("GetAllDirectors")]
        [ProducesResponseType(typeof(IEnumerable<Director>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Director>>> GetAllDirectors()
        {
            try
            {
                var result = await _directorRepository.GetAll();
                _logger.LogInformation($"Отримали усіх режисерів з бази даних!");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetAllDirectorsAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        
        [HttpGet("GetAllScreenings")]
        [ProducesResponseType(typeof(IEnumerable<Screening>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Screening>>> GetAllScreenings()
        {
            try
            {
                var result = await _screeningRepository.GetAll();
                _logger.LogInformation($"Отримали усі сеанси з бази даних!");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetAllScreeningsAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        
        [HttpGet("GetSortedScreeningsAndMoviesByDateTime")]
        [ProducesResponseType(typeof(IEnumerable<MovieScreeningDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<MovieScreeningDto>>> GetSortedScreeningsAndMoviesByDateTime()
        {
            try 
            {
                var result = await _catalogService.GetSortedScreeningsAndMoviesByDateTime();
                _logger.LogInformation($"Отримали усі сеанси відсортовані за датою та часом з бази даних!");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetSortScreeningsByDateTime - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        
        [HttpGet("GetAllScreeningsWithSeats")]
        [ProducesResponseType(typeof(IEnumerable<ScreeningDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ScreeningDto>>> GetScreeningsWithSeats()
        {
            try
            {
                var result = await _catalogService.GetScreeningsWithSeats();
                _logger.LogInformation("Fetched all screenings with seats from the database!");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Transaction failed! Something went wrong in GetScreeningsWithSeats - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }
        
        [HttpGet("GetScreeningWithSeatsById/{screeningId}")]
        [ProducesResponseType(typeof(ScreeningDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ScreeningDto>> GetScreeningWithSeatsById(Guid screeningId)
        {
            try
            {
                var result = await _catalogService.GetScreeningWithSeatsById(screeningId);

                if (result == null)
                {
                    return NotFound();
                }

                _logger.LogInformation($"Fetched screening with ID {screeningId} along with its seats from the database!");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Transaction failed! Something went wrong in GetScreeningWithSeatsById - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }
        
        [HttpPost]
        [Route("[action]/{screeningId}/{seatId}", Name = "ReserveSeat")]
        public async Task<ActionResult> ReserveSeat(Guid screeningId, Guid seatId, string username)
        {
            try
            {
                await _catalogService.ReserveSeat(screeningId, seatId);
                var pdfFile = await _pdfService.GenerateReservationPdfAsync(seatId, username);
                return File(pdfFile, "application/pdf", "ReservationDetails.pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Transaction failed! Something went wrong in ReserveSeat - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }
        
        [HttpGet]
        [Route("[action]/{id}", Name = "GetProductById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductById(Guid id)
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
        [Route("[action]/{name}", Name = "GetProductsByName")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductsByName(string name)
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
        
        [HttpGet]
        [Route("[action]/{name}", Name = "GetProductsByActorName")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByActorName(string name)
        {
            try
            {
                var result = await _catalogService.GetProductsByActorName(name);
                _logger.LogInformation($"Отримали усі фільми актора {name} з бази даних!");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetProductsByActorName - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        [HttpGet]
        [Route("[action]/{name}", Name = "GetProductsByDirectorName")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByDirectorName(string name)
        {
            try
            {
                var result = await _catalogService.GetProductsByDirectorName(name);
                _logger.LogInformation($"Отримали усі фільми режисера {name} з бази даних!");
                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetProductsByDirector - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        
        [HttpGet]
        [Route("[action]/{name}", Name = "GetProductsByGenre")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByGenre(string name)
        {
            try
            {
                var result = await _catalogService.GetProductsByGenreName(name);
                _logger.LogInformation($"Отримали усі фільми жанру {name} з бази даних!");
                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetProductsByGenre - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        

        [HttpPost("CreateProductActorRelation")]
        [ProducesResponseType(typeof(ProductDetailsDto), (int)HttpStatusCode.OK)]
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
        
        [HttpPost("CreateProductGenreRelation")]
        [ProducesResponseType(typeof(ProductDetailsDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateProductGenreRelation(string productName, string genreName)
        {
            try
            {
                await _catalogService.CreateProductGenreRelation(productName, genreName);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі CreateProductGenreRelation - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        
        [HttpPost("CreateProductDirectorRelation")]
        [ProducesResponseType(typeof(ProductDetailsDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateProductDirectorRelation(string productName, string directorName)
        {
            try
            {
                await _catalogService.CreateProductDirectorRelation(productName, directorName);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі CreateProductDirectorRelation - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        
        [HttpPost("CreateProductScreeningRelation")]
        [ProducesResponseType(typeof(ProductDetailsDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateProductScreeningRelation(string productName, string screeningDate, string screeningTime)
        {
            try
            {
                await _catalogService.CreateProductScreeningRelation(productName, screeningDate, screeningTime);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі CreateProductScreeningRelation - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        
        [HttpDelete("DeleteProductScreeningRelation")]
        [ProducesResponseType(typeof(ProductDetailsDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteProductScreeningRelation(string productName, string scrDate, string scrTime)
        {
            try
            {
                await _catalogService.DeleteProductScreeningRelation(productName, scrDate, scrTime);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі DeleteProductScreeningRelation - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        
        [HttpDelete("DeleteProductActorRelation")]
        [ProducesResponseType(typeof(ProductDetailsDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteProductActorRelation(string productName, string actorName)
        {
            try
            {
                await _catalogService.DeleteProductActorRelation(productName, actorName);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі DeleteProductActorRelation - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        
        [HttpDelete("DeleteProductGenreRelation")]
        [ProducesResponseType(typeof(ProductDetailsDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteProductGenreRelation(string productName, string genreName)
        {
            try
            {
                await _catalogService.DeleteProductGenreRelation(productName, genreName);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі DeleteProductGenreRelation - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        
        [HttpDelete("DeleteProductDirectorRelation")]
        [ProducesResponseType(typeof(ProductDetailsDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteProductDirectorRelation(string productName, string directorName)
        {
            try
            {
                await _catalogService.DeleteProductDirectorRelation(productName, directorName);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі DeleteProductDirectorRelation - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        
        /// <summary>
        /// Id генерується автоматично, тож можна записувати будь-яке значення
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("CreateProduct")]
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
        
        /// <summary>
        /// Id генерується автоматично, тож можна записувати будь-яке значення
        /// </summary>
        /// <param name="actor"></param>
        /// <returns></returns>
        [HttpPost("CreateActor")]
        [ProducesResponseType(typeof(Actor), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateActor([FromBody] Actor actor)
        {
            try
            {
                actor.Id = Guid.NewGuid();
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                    return BadRequest("Обєкт івенту є некоректним");
                }

                await _actorRepository.Create(actor);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі CreateActor - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        
        /// <summary>
        /// Id генерується автоматично, тож можна записувати будь-яке значення
        /// </summary>
        /// <param name="genre"></param>
        /// <returns></returns>
        [HttpPost("CreateGenre")]
        [ProducesResponseType(typeof(Genre), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateGenre([FromBody] Genre genre)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                    return BadRequest("Обєкт івенту є некоректним");
                }

                await _genreRepository.Create(genre);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі CreateGenre - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        
        /// <summary>
        /// Id генерується автоматично, тож можна записувати будь-яке значення
        /// </summary>
        /// <param name="director"></param>
        /// <returns></returns>
        [HttpPost("CreateDirector")]
        [ProducesResponseType(typeof(Director), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateDirector([FromBody] Director director)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                    return BadRequest("Обєкт івенту є некоректним");
                }

                await _directorRepository.Create(director);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі CreateDirector - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        
        [HttpPut("UpdateProduct")]
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
        
        [HttpPut("UpdateActor")]
        [ProducesResponseType(typeof(Actor), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateActor([FromBody] Actor actor)
        {
            try
            {
                await _actorRepository.Update(actor);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі UpdateActor - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        
        [HttpPut("UpdateGenre")]
        [ProducesResponseType(typeof(Genre), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateGenre([FromBody] Genre genre)
        {
            try
            {
                await _genreRepository.Update(genre);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі UpdateGenre - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        
        [HttpPut("UpdateDirector")]
        [ProducesResponseType(typeof(Director), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateDirector([FromBody] Director director)
        {
            try
            {
                await _directorRepository.Update(director);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі UpdateDirector - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        [HttpDelete]
        [Route ("[action]/{id}")]
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
        
        [HttpDelete]
        [Route ("[action]/{id}")]
        [ProducesResponseType(typeof(Actor), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteActor(Guid id)
        {
            try
            {
                await _actorRepository.Delete(id);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Транзакція сфейлилась! Щось пішло не так у методі DeleteActor - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        
        [HttpDelete]
        [Route ("[action]/{id}")]
        [ProducesResponseType(typeof(Genre), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteGenre(Guid id)
        {
            try
            {
                await _genreRepository.Delete(id);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Транзакція сфейлилась! Щось пішло не так у методі DeleteGenre - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        
        [HttpDelete]
        [Route ("[action]/{id}")]
        [ProducesResponseType(typeof(Director), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteDirector(Guid id)
        {
            try
            {
                await _directorRepository.Delete(id);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Транзакція сфейлилась! Щось пішло не так у методі DeleteDirector - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        
        [HttpDelete]
        [Route ("[action]/{id}")]
        [ProducesResponseType(typeof(Screening), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteScreening(Guid id)
        {
            try
            {
                await _screeningRepository.Delete(id);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Транзакція сфейлилась! Щось пішло не так у методі DeleteScreening - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        
        [HttpDelete("DeleteScreeningByDateTime")]
        [ProducesResponseType(typeof(Screening), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteScreeningByDateTime(string screeningDate, string screeningTime)
        {
            try
            {
                await _catalogService.DeleteScreeningByDateTime(screeningDate, screeningTime);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Transaction failed! Something went wrong in DeleteScreeningByDateTime - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }
    }
}
