using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace StockService.Core.Entities;

public class Product
{
    // ~~TODO: Add properties~~
    public Product()
    {
        Name = null!;
        Description = null!;
        Price = 0;
        Stock = 0;
    }

    public Product(string name, string desc, decimal price, int stock)
    {
        Name = name;
        Description = desc;
        Price = price;
        Stock = stock;
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}