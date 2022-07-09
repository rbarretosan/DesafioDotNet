using System.Threading.Tasks;
using DesafioDotNet.Application.Dtos;

namespace DesafioDotNet.Application.Contracts
{
    public interface IProductService
    {
        Task<ProductDto> AddProduct(ProductDto model);
        Task<ProductDto> UpdateProduct(int id, ProductDto model);
        Task<bool> DeleteProduct(int id);
        Task<ProductDto[]> GetAllProductAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
    }
}