using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entities;
using System.Net.Http;
using Newtonsoft.Json;

namespace Website.Controllers
{
    public class GiftsController : Controller
    {
        private Uri BaseEndPoint { get; set; }
        private readonly HttpClient _httpClient;


        public GiftsController()
        {
            BaseEndPoint = new Uri("https://localhost:44308/api/gifts/");
            _httpClient = new HttpClient();
        }

        // GET: Gifts
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync(BaseEndPoint, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return View(JsonConvert.DeserializeObject<List<Gift>>(data));
        }

        public async Task<IActionResult> GirlIndex()
        {
            var response = await _httpClient.GetAsync(BaseEndPoint.ToString() + "Girl", HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return View(JsonConvert.DeserializeObject<List<Gift>>(data));
        }

        public async Task<IActionResult> BoyIndex()
        {
            var response = await _httpClient.GetAsync(BaseEndPoint.ToString() + "Boy", HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return View(JsonConvert.DeserializeObject<List<Gift>>(data));
        }

        // GET: Gifts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _httpClient.GetAsync(BaseEndPoint.ToString() + id, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var gift = JsonConvert.DeserializeObject<Gift>(data);

            if (gift == null)
            {
                return NotFound();
            }

            return View(gift);
        }

        // GET: Gifts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gifts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GiftNumber,Title,Description,CreationDate,BoyGift,GirlGift")] Gift gift)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync(BaseEndPoint, gift);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(gift);
        }

        private async Task<bool> GiftExists(int id)
        {
            var response = await _httpClient.GetAsync(BaseEndPoint, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var context = JsonConvert.DeserializeObject<List<Gift>>(data);
            return context.Any(e => e.GiftNumber == id);
        }
    }
}
