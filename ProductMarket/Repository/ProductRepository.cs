using Microsoft.EntityFrameworkCore;
using ProductMarket.Data;
using ProductMarket.Interfaces;
using ProductMarket.Models;

namespace ProductMarket.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Product product)
        {
            _context.Add(product);
            return Save();
        }

        public bool Delete(Product product)
        {
            _context.Remove(product);
            return Save();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();

        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.Include(i=> i.Address).FirstOrDefaultAsync(i => i.ProductId == id);
        }
        public async Task<Product> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Products.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.ProductId == id);
        }

        public async Task<IEnumerable<Product>> GetProductByCity(string city)
        {
            return await _context.Products.Where(c => c.Address.City.Contains(city)).ToListAsync(); ;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Product product)
        {
            _context.Update(product);
            return Save();
        }
    }
}
