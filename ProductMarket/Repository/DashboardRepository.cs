using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductMarket.Data;
using ProductMarket.Interfaces;
using ProductMarket.Models;

namespace ProductMarket.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, 
            UserManager<AppUser> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<List<Product>> GetAllUserProducts()
        {
            var curUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (curUser == null)
            {
                return new List<Product>();
            }

            var userProducts = await _context.Products
                .Where(x => x.AppUserId == curUser.Id)
                .ToListAsync();

            var userProductRepositories = userProducts.Cast<Product>().ToList();

            return userProductRepositories;
        }


    }
}
