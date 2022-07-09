using System.Threading.Tasks;
using DesafioDotNet.Domain;

namespace DesafioDotNet.Persistence.Contracts
{
    public interface IProductPersistEF
    {
        Task<Product[]> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
    }
}