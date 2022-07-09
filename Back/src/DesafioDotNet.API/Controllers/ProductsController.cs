using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DesafioDotNet.Application.Contracts;
using DesafioDotNet.Application.Dtos;

namespace DesafioDotNet.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController: ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var products = await _productService.GetAllProductAsync();
                if(products == null) return NoContent();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar produtos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if(product == null) return NoContent();;
                return Ok(product);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar produto. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductDto model)
        {
            try
            {
                var product = await _productService.AddProduct(model);
                if(product == null) return NoContent();;
                return Ok(product);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar produto. Erro: {ex.Message}");
            }        
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProductDto model)
        {
            try
            {
                var product = await _productService.UpdateProduct(id, model);
                if(product == null) return NoContent();;
                return Ok(product);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar produto. Erro: {ex.Message}");
            }        
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if(product == null) return NoContent();;

                return await _productService.DeleteProduct(id)?
                    Ok("Deletado"):
                    throw new Exception("Ocorreu um problema nao específico ao deletar product.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar excluir produto. Erro: {ex.Message}");
            }        

        }

    }
}
