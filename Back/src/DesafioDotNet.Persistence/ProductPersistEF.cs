using System.Linq;
using System.Threading.Tasks;
using DesafioDotNet.Domain;
using DesafioDotNet.Persistence.Contracts;
using DesafioDotNet.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DesafioDotNet.Persistence
{
    public class ProductPersistEF: IProductPersistEF
    {
        private readonly DesafioDotNetContext _context;
        public ProductPersistEF(DesafioDotNetContext context)
        {
            _context = context;
        }

        public async Task<Product[]> GetAllProductsAsync()
        {
            IQueryable<Product> query = _context.Products;

            query = query.OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            IQueryable<Product> query = _context.Products;

            query = query.Where(e => e.Id == id);

            return await query.FirstOrDefaultAsync();        
        }

    }
}