using Microsoft.AspNetCore.Mvc;
using MyRent.Models;

namespace MyRent.Controllers
{
    public class ObjectController : Controller
    {

        private readonly HttpClient _client;
        private readonly ILogger<ObjectController> _logger;

        public ObjectController(IHttpClientFactory httpClientFactory, ILogger<ObjectController> logger)
        {
            _client = httpClientFactory.CreateClient("MyRentAPI");
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetObjects(string query = "", int page = 1, int pageSize = 3)
        {
            try
            {
                var rentObjects = await _client.GetFromJsonAsync<List<RentObject>>("objects/simple_list");

                if (rentObjects == null)
                {
                    return Json(new PaginatedResponse<RentObject>());
                }

                if (!string.IsNullOrEmpty(query))
                {
                    rentObjects = rentObjects
                        .Where(rentObject => rentObject.Name.Contains(query, StringComparison.CurrentCultureIgnoreCase))
                        .ToList();
                }

                int totalItems = rentObjects.Count();
                int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

                var paginatedList = rentObjects
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();

                var response = new PaginatedResponse<RentObject>
                {
                    Data = paginatedList,
                    CurrentPage = page,
                    TotalItems = totalItems,
                    TotalPages = totalPages
                };

                return Json(response);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _logger.LogError("Error fetching objects. Message: {Message}", message);
                return Content(message);
            }
        }

        public async Task<IActionResult> Details(string id_hash)
        {
            try
            {
                var objects = await _client.GetFromJsonAsync<List<RentObject>>($"objects/simple_details?id_hash={id_hash}");
                var item = objects?.FirstOrDefault();

                if (item == null)
                {
                    return NotFound("Object not found");
                }

                var pictures = await _client.GetFromJsonAsync<List<RentObjectPicture>>($"objects/get_pictures_links?id_hash={id_hash}");

                item.gallery = pictures ?? new List<RentObjectPicture>();

                return View(item);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _logger.LogError("Error fetching object. Message: {Message}", message);
                return Content(message);
            }
        }

    }
}
