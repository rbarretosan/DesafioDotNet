using System.Threading.Tasks;
using DesafioDotNet.Domain;

namespace DesafioDotNet.Persistence.Contracts
{
    public interface IProductPersist
    {
        Task<Product> AddProduct(Product entity);
        Task<Product> UpdateProduct(int id, Product entity);
        void DeleteProduct(int id);
        Task<Product[]> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
    }
}