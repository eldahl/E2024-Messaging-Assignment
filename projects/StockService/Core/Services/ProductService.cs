using Repository;
using StockService.Core.Entities;

namespace StockService.Core.Services;

public class ProductService
{
    private readonly IRepository<Product> _repository;
    
    public ProductService(IRepository<Product> repository)
    {
        _repository = repository;
    }
    
    public void PopulateDb()
    {
        // Populate the database with some products
        _repository.Add(new Product
        {
            Name = "Apple",
            Description = "Apple",
            Price = 5,
            Stock = 500, 
        });
        _repository.Add(new Product
        {
            Name = "Pear",
            Description = "Pear",
            Price = 5,
            Stock = 200, 
        });
        _repository.Add(new Product
        {
            Name = "Orange",
            Description = "Orange",
            Price = 10,
            Stock = 3000, 
        });
    }
    
    public IEnumerable<Product> GetOrderProducts(int[] productIds)
    {
        // ~~TODO: Implement this method~~
        return _repository.GetAll().Where(p => productIds.Contains(p.Id));
    }
    
    public IEnumerable<Product> GetProducts()
    {
        return _repository.GetAll();
    }
}