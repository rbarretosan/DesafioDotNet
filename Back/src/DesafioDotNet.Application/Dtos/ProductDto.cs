using System;

namespace DesafioDotNet.Application.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public DateTime createdAt { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string brand { get; set; }
        public DateTime? updatedAt { get; set; }
    }
}