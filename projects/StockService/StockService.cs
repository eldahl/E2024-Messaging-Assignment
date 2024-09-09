using MessageClient.Drivers.EasyNetQ.MessagingStrategies;
using Messages;
using StockService.Core.Services;

namespace StockService;

using MessageClient;

public class StockService
{
  private readonly MessageClient<OrderRequestMessage> _messageClientStock;
  private readonly MessageClient<OrderRequestMessage> _messageClientShipping;
  private readonly ProductService _productService;
  
  public StockService(MessageClient<OrderRequestMessage> messageClientStock, MessageClient<OrderRequestMessage> messageClientShipping, ProductService productService)
  {
    _messageClientStock = messageClientStock;
    _messageClientShipping = messageClientShipping;
    _productService = productService;
  }
  
  public void PopulateDb()
  {
    // Populate the database with some products
    _productService.PopulateDb();
  }

  public void Start()
  {
    // TODO: Start listening for new orders
    _messageClientStock.ConnectAndListen(HandleNewOrder);
    _messageClientShipping.Connect();
  }

  private void HandleNewOrder(OrderRequestMessage order)
  {
    /*
     * TODO: Handle new orders
     * - Check the stock of the products in the order
     * - Create a new order response with the stock status of the products
     * - Send the order response so the shipping service can calculate the shipping cost
     */
    _messageClientShipping.SendUsingTopic(new OrderRequestMessage(){ CustomerId = order.CustomerId, Status = "In Stock" }, "processShippingOrder");
  }
}