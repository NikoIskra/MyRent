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
                var response = await _client.GetAsync("objects/simple_list");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("API Greška: {StatusCode}, Body: {Body}", response.StatusCode, errorContent);

                    return StatusCode((int)response.StatusCode, errorContent);
                }

                var rentObjects = await response.Content.ReadFromJsonAsync<List<RentObject>>();

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

                var paginatedResponse = new PaginatedResponse<RentObject>
                {
                    Data = paginatedList,
                    CurrentPage = page,
                    TotalItems = totalItems,
                    TotalPages = totalPages
                };

                return Json(paginatedResponse);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                _logger.LogError("Error fetching objects. Message: {Message}", message);


                return StatusCode(500, new { status = "error", message = message });
            }
        }

        public async Task<IActionResult> Details(string id_hash)
        {
            try
            {
                var response = await _client.GetAsync($"objects/simple_details?id_hash={id_hash}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("API Greška: {StatusCode}, Body: {Body}", response.StatusCode, errorContent);

                    return StatusCode((int)response.StatusCode, errorContent);
                }

                var objects = await response.Content.ReadFromJsonAsync<List<RentObject>>();


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
