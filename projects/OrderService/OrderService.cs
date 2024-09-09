using MessageClient;
using Messages;
using OrderService.Core.Mappers;

namespace OrderService;

public class OrderService
{
  private readonly MessageClient<OrderRequestMessage> _newOrderClient;
  private readonly MessageClient<OrderRequestMessage> _processOrderClient;
  private readonly MessageClient<OrderResponseMessage> _orderCompletionClient;
  private readonly MessageClient<OrderResponseMessage> _shippingCompletionClient;
  
  private readonly Core.Services.OrderService _orderService;
  private readonly OrderResponseMapper _orderResponseMapper;
  public OrderService(
      MessageClient<OrderRequestMessage> newOrderClient, 
      MessageClient<OrderResponseMessage> orderCompletionClient,
      MessageClient<OrderRequestMessage> processOrderClient,
      MessageClient<OrderResponseMessage> shippingCompletionClient,
      Core.Services.OrderService orderService, 
      OrderResponseMapper orderResponseMapper)
  {
    _newOrderClient = newOrderClient;
    _orderCompletionClient = orderCompletionClient;
    _processOrderClient = processOrderClient;
    _shippingCompletionClient = shippingCompletionClient;
    _orderService = orderService;
    _orderResponseMapper = orderResponseMapper;
  }

  public void Start()
  {
    // Start listening for new orders
    _newOrderClient.ConnectAndListen(HandleNewOrder);

    _processOrderClient.Connect();
    _orderCompletionClient.Connect();
    
    // Connect to the order completion topic
    _shippingCompletionClient.ConnectAndListen(HandleOrderCompletion);
  }

  private void HandleNewOrder(OrderRequestMessage order)
  {
    /*
     * ~~TODO: Handle new orders~~
     * - Check if the order is valid
     * - Create the order in the database (optional)
     * - Send the order to the stock service
     */
    // CheckIfValid(order);
    
    // Forward to stock service
    _processOrderClient.Send(order);
  }
  
  private void HandleOrderCompletion(OrderResponseMessage order)
  {
      /*
       * TODO: Handle the order completion, e.g. change the order status
       * - Update the order status in the database
       * - Notify the customer
       */
      // Send to OrderController@StoreAPI
      Console.WriteLine(order.Status);
      if (order.Status == "Shipping: 19333.69")
      {
          order.Status = "Order Completed";
      }
      Console.WriteLine(order.Status);
      _orderCompletionClient.SendUsingTopic(order, order.CustomerId);
  }
}