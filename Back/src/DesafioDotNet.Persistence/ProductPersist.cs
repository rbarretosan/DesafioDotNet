using System.Threading.Tasks;
using DesafioDotNet.Domain;
using DesafioDotNet.Persistence.Contracts;
using DesafioDotNet.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace DesafioDotNet.Persistence
{
    public class ProductPersist: IProductPersist
    {
        private readonly DesafioDotNetContext _context;
        private readonly String _connectionString;
        private readonly SqlConnection sqlConnection;
        
        public ProductPersist(DesafioDotNetContext context)
        {
            _context = context;
            _connectionString = _context.Database.GetConnectionString();
            sqlConnection = new SqlConnection(_connectionString);
        }

        public async Task<Product>AddProduct(Product entity)
        {
            SqlCommand sqlCommand = new SqlCommand("InsertProduct", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("@createdAt", entity.createdAt));
            sqlCommand.Parameters.Add(new SqlParameter("@name", entity.name));
            sqlCommand.Parameters.Add(new SqlParameter("@price", entity.price));
            sqlCommand.Parameters.Add(new SqlParameter("@brand", entity.brand));
            sqlCommand.Parameters.Add(new SqlParameter("@updatedAt", entity.updatedAt));
            
            await sqlConnection.OpenAsync();

            int returnId = -1;
            var objectReturn = await sqlCommand.ExecuteScalarAsync();

            sqlCommand.Dispose();

            if (sqlConnection.State == System.Data.ConnectionState.Open) sqlConnection.Close();

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
            SqlCommand sqlCommand = new SqlCommand("DeleteProduct", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("@id", id));
            
            await sqlConnection.OpenAsync();

            await sqlCommand.ExecuteNonQueryAsync();

            sqlCommand.Dispose();

            if (sqlConnection.State == System.Data.ConnectionState.Open) sqlConnection.Close();
        }

        public async Task<Product[]> GetAllProductsAsync()
        {
            SqlCommand sqlCommand = new SqlCommand("GetAllProducts", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            
            var result = new List<Product>();                        

            await sqlConnection.OpenAsync();

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

            await sqlConnection.CloseAsync();

            return result.ToArray();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            SqlCommand sqlCommand = new SqlCommand("GetProductById", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@Id", id));
            
            await sqlConnection.OpenAsync();

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

            await sqlConnection.CloseAsync();

            return result;
        }

        public async Task<Product>UpdateProduct(int id, Product entity)
        {
            SqlCommand sqlCommand = new SqlCommand("UpdateProduct", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            sqlCommand.Parameters.Add(new SqlParameter("@id", id));
            sqlCommand.Parameters.Add(new SqlParameter("@name", entity.name));
            sqlCommand.Parameters.Add(new SqlParameter("@price", entity.price));
            sqlCommand.Parameters.Add(new SqlParameter("@brand", entity.brand));
            sqlCommand.Parameters.Add(new SqlParameter("@updatedAt", entity.updatedAt));
            
            await sqlConnection.OpenAsync();

            await sqlCommand.ExecuteNonQueryAsync();

            sqlCommand.Dispose();

            if (sqlConnection.State == System.Data.ConnectionState.Open) sqlConnection.Close();

            return entity;
        }
    }
}