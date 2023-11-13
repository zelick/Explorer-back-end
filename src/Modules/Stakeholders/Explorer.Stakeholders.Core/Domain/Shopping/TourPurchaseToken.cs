using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.Shopping
{
    public class TourPurchaseToken : ValueObject
    {
        public long CustomerId { get; set; }
        public long TourId { get; set; }

        public TourPurchaseToken() { }

        [JsonConstructor]
        public TourPurchaseToken(long customerId, long tourId)
        {
            CustomerId = customerId;
            TourId = tourId;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CustomerId;
            yield return TourId;
        }
    }
}