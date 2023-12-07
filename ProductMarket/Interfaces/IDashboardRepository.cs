using ProductMarket.Models;

namespace ProductMarket.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Product>> GetAllUserProducts();
        
    }
}
