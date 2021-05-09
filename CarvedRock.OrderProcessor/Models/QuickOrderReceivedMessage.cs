using System;

namespace CarvedRock.OrderProcessor.Models
{
    public class QuickOrderReceivedMessage
    {
        public QuickOrder Order { get; set; }
        public int CustomerId { get; set; }
        public Guid OrderId { get; set; }
    }

    public class QuickOrder
    {
        public int ProductId { get; set; }
        public short Quantity { get; set; }
    }
}
