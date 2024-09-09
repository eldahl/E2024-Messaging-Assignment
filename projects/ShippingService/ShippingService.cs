using MessageClient;
using Messages;

namespace ShippingService;

public class ShippingService
{
    private readonly MessageClient<OrderResponseMessage> _messageClientResponse;
    private readonly MessageClient<OrderRequestMessage> _messageClientStockRequest;
    
    public ShippingService(MessageClient<OrderResponseMessage> messageClientResponse, MessageClient<OrderRequestMessage> messageClientStockRequest)
    {
        _messageClientResponse = messageClientResponse;
        _messageClientStockRequest = messageClientStockRequest;
    }
    
    public void Start()
    {
        // ~~TODO: Start listening for orders that need to be shipped~~
        _messageClientStockRequest.ConnectAndListen(HandleOrderShippingCalculation);
        _messageClientResponse.Connect();
    }
    
    private void HandleOrderShippingCalculation(OrderRequestMessage orderResponse)
    {
        /*
         * ~~TODO: Handle the calculation of the shipping cost for the order~~
         * - Calculate the shipping cost
         * - Change the status of the order and apply the shipping cost
         * - Send the processed order to the order service for completion
         */
        
        if (orderResponse.Status == "In Stock")
            _messageClientResponse.SendUsingTopic(new OrderResponseMessage()
            {
                CustomerId = orderResponse.CustomerId, 
                Status = "Shipping: " + ShippingCalculator.CalculateShippingCost()
            }, "orderCompleted");
    }
}