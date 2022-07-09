using System;
using System.Threading.Tasks;
using AutoMapper;
using DesafioDotNet.Application.Contracts;
using DesafioDotNet.Application.Dtos;
using DesafioDotNet.Domain;
using DesafioDotNet.Persistence.Contracts;

namespace DesafioDotNet.Application
{
    public class ProductServiceEF: IProductServiceEF
    {
        private readonly IGenericPersistEF _genericPersist;
        private readonly IProductPersistEF _productPersist;
        private readonly IMapper _mapper;

        public ProductServiceEF(IGenericPersistEF genericPersist, IProductPersistEF productPersist, IMapper mapper)
        {
            _genericPersist = genericPersist;
            _productPersist = productPersist;
            _mapper = mapper;
        }

        public async Task<ProductDto> AddProduct(ProductDto model)
        {
            try
            {
                var product = _mapper.Map<Product>(model);
                _genericPersist.Add<Product>(product);
                if(await _genericPersist.SaveChangesAsync())
                {
                    var productReturn = await _productPersist.GetProductByIdAsync(product.Id);
                    return _mapper.Map<ProductDto>(productReturn);
                }
                return null;
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
                if(product == null) return null;

                model.Id = id;

                _mapper.Map(model, product);

                _genericPersist.Update<Product>(product);
                if(await _genericPersist.SaveChangesAsync())
                {
                    var productReturn = await _productPersist.GetProductByIdAsync(product.Id);
                    return _mapper.Map<ProductDto>(productReturn);
                }
                return null;                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                var product = await  _productPersist.GetProductByIdAsync(id);
                if(product == null) throw new Exception("product para delete nao encontrado.");

                _genericPersist.Delete<Product>(product);
                return await _genericPersist.SaveChangesAsync();
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
