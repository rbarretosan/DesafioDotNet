using System.Threading.Tasks;
using DesafioDotNet.Domain;
using DesafioDotNet.Persistence.Contracts;
using DesafioDotNet.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace DesafioDotNet.Persistence
{
    public class ProductPersist: IProductPersist
    {
        private readonly DesafioDotNetContext _context;
        private readonly String _connectionString;
        private readonly SqlConnection _sqlConnection;
        
        public ProductPersist(DesafioDotNetContext context)
        {
            _context = context;
            _connectionString = _context.Database.GetConnectionString();
            _sqlConnection = new SqlConnection(_connectionString);
        }

        public async Task<Product>AddProduct(Product entity)
        {
            SqlCommand sqlCommand = new SqlCommand("InsertProduct", _sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("@createdAt", entity.createdAt));
            sqlCommand.Parameters.Add(new SqlParameter("@name", entity.name));
            sqlCommand.Parameters.Add(new SqlParameter("@price", entity.price));
            sqlCommand.Parameters.Add(new SqlParameter("@brand", entity.brand));
            sqlCommand.Parameters.Add(new SqlParameter("@updatedAt", entity.updatedAt));
            
            await _sqlConnection.OpenAsync();

            int returnId = -1;
            var objectReturn = await sqlCommand.ExecuteScalarAsync();

            sqlCommand.Dispose();

            if (_sqlConnection.State == System.Data.ConnectionState.Open) _sqlConnection.Close();

            if(objectReturn != null)
            {
                int.TryParse(objectReturn.ToString(), out returnId);
                var product = await this.GetProductByIdAsync(returnId);
                return product;
            }
            
            return null;
        }

        public async void DeleteProduct(int id)
        {
            SqlCommand sqlCommand = new SqlCommand("DeleteProduct", _sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("@id", id));
            
            await _sqlConnection.OpenAsync();

            await sqlCommand.ExecuteNonQueryAsync();

            sqlCommand.Dispose();

            if (_sqlConnection.State == System.Data.ConnectionState.Open) _sqlConnection.Close();
        }

        public async Task<Product[]> GetAllProductsAsync()
        {
            SqlCommand sqlCommand = new SqlCommand("GetAllProducts", _sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            
            var result = new List<Product>();                        

            await _sqlConnection.OpenAsync();

            var product = await sqlCommand.ExecuteReaderAsync();            

            while(await product.ReadAsync())
            {
                if(product.HasRows)
                {
                    result.Add
                    (
                        new Product()
                        {
                            Id = (int)product["Id"],
                            createdAt = (DateTime)product["createdAt"],
                            name = (string)product["name"],
                            price = (decimal)product["price"],
                            brand = (string)product["brand"],
                            updatedAt = (DateTime)product["updatedAt"]
                        }
                    );
                }
            }

            await _sqlConnection.CloseAsync();

            return result.ToArray();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            SqlCommand sqlCommand = new SqlCommand("GetProductById", _sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@Id", id));
            
            await _sqlConnection.OpenAsync();

            var product = await sqlCommand.ExecuteReaderAsync();

            await product.ReadAsync();

            if(!product.HasRows) return null;

            var result = new Product()
            {
                Id = (int)product["Id"],
                createdAt = (DateTime)product["createdAt"],
                name = (string)product["name"],
                price = (decimal)product["price"],
                brand = (string)product["brand"],
                updatedAt = (DateTime)product["updatedAt"]
            };

            await _sqlConnection.CloseAsync();

            return result;
        }

        public async Task<Product>UpdateProduct(int id, Product entity)
        {
            SqlCommand sqlCommand = new SqlCommand("UpdateProduct", _sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("@id", id));
            sqlCommand.Parameters.Add(new SqlParameter("@name", entity.name));
            sqlCommand.Parameters.Add(new SqlParameter("@price", entity.price));
            sqlCommand.Parameters.Add(new SqlParameter("@brand", entity.brand));
            sqlCommand.Parameters.Add(new SqlParameter("@updatedAt", entity.updatedAt));
            
            await _sqlConnection.OpenAsync();

            await sqlCommand.ExecuteNonQueryAsync();

            sqlCommand.Dispose();

            if (_sqlConnection.State == System.Data.ConnectionState.Open) _sqlConnection.Close();

            return entity;
        }
    }
}