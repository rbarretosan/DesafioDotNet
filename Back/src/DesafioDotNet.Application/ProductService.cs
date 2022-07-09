using System;
using System.Threading.Tasks;
using AutoMapper;
using DesafioDotNet.Application.Contracts;
using DesafioDotNet.Application.Dtos;
using DesafioDotNet.Domain;
using DesafioDotNet.Persistence.Contracts;

namespace DesafioDotNet.Application
{
    public class ProductService: IProductService
    {
        private readonly IProductPersist _productPersist;
        private readonly IMapper _mapper;

        public ProductService(IProductPersist productPersist, IMapper mapper)
        {
            _productPersist = productPersist;
            _mapper = mapper;
        }

        public async Task<ProductDto> AddProduct(ProductDto model)
        {
            try
            {
                var product = _mapper.Map<Product>(model);
                var productReturn =  await _productPersist.AddProduct(product);
                return _mapper.Map<ProductDto>(productReturn);                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }

        public async Task<ProductDto> UpdateProduct(int id, ProductDto model)
        {
            try
            {
                var product = await  _productPersist.GetProductByIdAsync(id);

                model.Id = id;

                _mapper.Map(model, product);

                var productReturn = await _productPersist.UpdateProduct(id, product);

                return _mapper.Map<ProductDto>(productReturn);                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool>DeleteProduct(int id)
        {
            try
            {
                var product = await  _productPersist.GetProductByIdAsync(id);
                if(product == null) throw new Exception("product para delete nao encontrado.");

                _productPersist.DeleteProduct(id);

                return true;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }                
        }

        public async Task<ProductDto[]> GetAllProductAsync()
        {
            try
            {
                var product = await _productPersist.GetAllProductsAsync();
                if(product == null) return null;
                var result = _mapper.Map<ProductDto[]>(product);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }        
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _productPersist.GetProductByIdAsync(id);
                if(product == null) return null;

                var result = _mapper.Map<ProductDto>(product);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
