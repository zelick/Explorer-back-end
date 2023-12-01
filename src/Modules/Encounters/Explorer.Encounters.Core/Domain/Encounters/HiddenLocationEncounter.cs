using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.Encounters
{
    public class HiddenLocationEncounter:Encounter
    {
        public double LocationLongitude { get; init; }
        public double LocationLatitude { get; init; }
        public string Image {  get; init; }
        public double Range { get; init; }
        public HiddenLocationEncounter() { }



    }
}
