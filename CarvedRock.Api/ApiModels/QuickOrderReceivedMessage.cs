using System;

namespace CarvedRock.Api.ApiModels
{
    public class QuickOrderReceivedMessage
    {
        public QuickOrder Order { get; set; }
        public int CustomerId { get; set; }
        public Guid OrderId { get; set; }
    }
}
