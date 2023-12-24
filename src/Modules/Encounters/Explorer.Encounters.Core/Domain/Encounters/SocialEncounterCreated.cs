using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Encounters.Core.Domain.Encounters
{
    public class SocialEncounterCreated : DomainEvent
    {
        public DateTime DateOfCreation { get; private set; }
        public int RequiredPeople { get; private set; }
        public double Range { get; private set; }
        public SocialEncounterCreated(long aggregateId, DateTime dateOfCreation, int requiredPeople, double range) : base(aggregateId)
        {
            DateOfCreation = dateOfCreation;
            RequiredPeople = requiredPeople;
            Range = range;
        }
    }
}
