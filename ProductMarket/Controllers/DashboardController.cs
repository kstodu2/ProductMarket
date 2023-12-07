using Microsoft.AspNetCore.Mvc;
using ProductMarket.Data;
using ProductMarket.Interfaces;
using ProductMarket.ViewModels;

namespace ProductMarket.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }
        public async Task<IActionResult> Index()
        {
            var userProducts = await _dashboardRepository.GetAllUserProducts();
            var dashboardViewModel = new DashboardViewModel()
            {
                Products = userProducts
            };
            return View(dashboardViewModel);
        }
    }
}
