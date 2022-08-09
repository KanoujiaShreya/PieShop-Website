using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PieShopAssignment2.Models;
using PieShopAssignment2.ViewModels;
using System.Net.Http.Json;

namespace PieShopAssignment2.Controllers
{
    [Authorize]
    public class PieController : Controller
    {
        private readonly IMapper mapper;
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository categorysRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        public PieController(IPieRepository pieRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, ICategoryRepository categorysRepository)
        {
            _pieRepository = pieRepository;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.categorysRepository = categorysRepository;
        }

        public async Task<ViewResult> List(int categoryId)
        {

            IEnumerable<Pie> pies = new List<Pie>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7211/api/pies/GetAllPies"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    pies = JsonConvert.DeserializeObject<IEnumerable<Pie>>(apiResponse);
                }
            }

              //if (categoryId == 1)
              //{
              //    pies = _pieRepository.AllPies.Where(pie => pie.CategoryId == categoryId);

              //}
              //else if()
              //{
              //    pies= _pieRepository.AllPies;
              //}

            PieListViewModel pieListViewModel = new PieListViewModel();
            pieListViewModel.Pies = pies;
            pieListViewModel.CurrentCategory = "All the Pies";


            return View(pieListViewModel);
        }

        public IActionResult ListMini()
        {
            var pies = _pieRepository.AllPies;
            var piemini = mapper.Map<IEnumerable<PieMini>>(pies);
            return View(piemini);
        }

        public async Task<ViewResult> Fruitpies()
        {


            IEnumerable<Pie> pies = new List<Pie>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7211/api/pies/FruitPies"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    pies = JsonConvert.DeserializeObject<IEnumerable<Pie>>(apiResponse);
                }
            }
            PieListViewModel pieListViewModel = new PieListViewModel();
            pieListViewModel.Pies = pies;
            pieListViewModel.CurrentCategory = "Fruit Pies";


            return View(pieListViewModel);

        }

        public async Task<ViewResult> CheesePies()
        {
 
            IEnumerable<Pie> pies = new List<Pie>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7211/api/pies/CheesePies"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    pies = JsonConvert.DeserializeObject<IEnumerable<Pie>>(apiResponse);
                }
            }
            PieListViewModel pieListViewModel = new PieListViewModel();
            pieListViewModel.Pies = pies;
            pieListViewModel.CurrentCategory = "Cheese Pies";


            return View(pieListViewModel);
        }

        public async Task<ViewResult> Seasonalpies()
        {
            IEnumerable<Pie> pies = new List<Pie>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7211/api/pies/SeasonalPies"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    pies = JsonConvert.DeserializeObject<IEnumerable<Pie>>(apiResponse);
                }
            }
            PieListViewModel pieListViewModel = new PieListViewModel();
            pieListViewModel.Pies = pies;
            pieListViewModel.CurrentCategory = "Seasonal Pies";


            return View(pieListViewModel);
        }


        public ViewResult Details(int id)
        {
            var pie = _pieRepository.AllPies.FirstOrDefault(pie => pie.PieId == id);
            return View(pie);

        }

        [Authorize]
        public ViewResult Create()
        {
            var categories = categorysRepository.AllCategories;
            List<SelectListItem> categoryItems = new List<SelectListItem>();
            foreach (var category in categories)
            {
                categoryItems.Add(new SelectListItem { Text = category.CategoryName, Value = category.CategoryId.ToString() });
            }
            ViewBag.categoryItems = categoryItems;
            return View();

        }
        //[HttpPost]
        //public IActionResult AddPie(Pie pie)
        //{
        //    _pieRepository.AddPie(pie);
        //    return RedirectToAction("List");
        //}

        [HttpPost]
        public async Task<IActionResult> AddPie(Pie pie)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsJsonAsync("https://localhost:7211/api/pies/InsertPie", pie))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("List");
        }


            [Authorize]
        public ViewResult Edit(int id)
        {
            var pie = _pieRepository.GetPieById(id);
            return View(pie);
        }

        //[HttpPost]
        //public IActionResult UpdatePie(Pie pie)
        //{
        //    _pieRepository.UpdatePie(pie);
        //    return RedirectToAction("List");
        //}

        [HttpPut]
        public async Task<IActionResult> UpdatePie(Pie pie)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsJsonAsync("https://localhost:7211/api/pies/UpdatePie", pie))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("List");
            //studentRepository.UpdateStudent(student);
            // return RedirectToAction("List");
        }


        [Authorize]
        public ViewResult Remove(int id)
        {
            var pie = _pieRepository.GetPieById(id);
            return View(pie);
        }


        public async Task<IActionResult> DeletePie(Pie pie)
        {
            int id = pie.PieId;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:7211/api/pies/DeletePie?PieId=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("List");
            //studentRepository.RemoveStudent(student);
            //return RedirectToAction("List");
        }



    }
}

