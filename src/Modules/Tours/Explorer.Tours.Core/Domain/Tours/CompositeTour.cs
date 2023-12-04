using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class CompositeTour : Entity
    {
        public long OwnerId { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public List<long>? TourIds { get; init; }
    }
}
