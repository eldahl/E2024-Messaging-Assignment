using MessageClient.Factory;
using Messages;

namespace ShippingService;

public static class ShippingServiceFactory
{
    public static ShippingService CreateShippingService()
    {
        var easyNetQFactory = new EasyNetQFactory();
        var messageClientResponse = easyNetQFactory.CreateTopicMessageClient<OrderResponseMessage>("ShippingService", "orderCompleted");
        var messageClientStockRequest = easyNetQFactory.CreateTopicMessageClient<OrderRequestMessage>("ShippingService", "processShippingOrder");
        
        return new ShippingService(messageClientResponse, messageClientStockRequest);
    }
}