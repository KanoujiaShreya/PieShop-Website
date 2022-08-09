using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PieShopAssignment2.Models;
using PieShopAssignment2.ViewModels;

namespace PieShopAssignment2.Controllers
{
    public class PiesOfTheWeekController : Controller
    {


        private readonly IPieRepository _pieRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        public PiesOfTheWeekController(IPieRepository pieRepository, IHttpContextAccessor httpContextAccessor)
        {
            _pieRepository = pieRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ViewResult> PiesOfTheWeek()
        {
            IEnumerable<Pie> pies = new List<Pie>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7211/api/pies/PiesOfTheWeek"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    pies = JsonConvert.DeserializeObject<IEnumerable<Pie>>(apiResponse);
                }
            }
            PieListViewModel pieListViewModel = new PieListViewModel();
            pieListViewModel.Pies = pies;
            pieListViewModel.CurrentCategory = "Cheese cakes";


            return View(pieListViewModel);
        }
        public ViewResult Details(int id)
        {
            var pie = _pieRepository.AllPies.FirstOrDefault(pie => pie.PieId == id);
            return View(pie);

        }

    }
}

