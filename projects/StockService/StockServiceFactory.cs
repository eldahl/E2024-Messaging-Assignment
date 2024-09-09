using Messages;
using MessageClient.Factory;
using StockService.Core.Helpers;
using StockService.Core.Repositories;
using StockService.Core.Services;

namespace StockService;

public static class StockServiceFactory
{
  public static StockService CreateStockService()
  {
    var easyNetQFactory = new EasyNetQFactory();
    var messageClientStock = easyNetQFactory.CreateTopicMessageClient<OrderRequestMessage>("StockService", "processStockOrder");
    var messageClientShipping = easyNetQFactory.CreateTopicMessageClient<OrderRequestMessage>("StockService", "processShippingOrder");
    
    var productRepository = new ProductRepository(new DataContext());
    var productService = new ProductService(productRepository);
    
    return new StockService(
      messageClientStock,
      messageClientShipping,
      productService
    );
  }
}