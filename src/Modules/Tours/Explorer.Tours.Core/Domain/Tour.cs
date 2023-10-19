using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Tours.Core.Domain
{
    public class Tour : Entity
    {
        public int AuthorId { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public Demandigness? DemandignessLevel { get; init; }
        public TourStatus Status { get; init; }
        public double Price { get; init; }
        public List<string>? Tags { get; init; }
       

        public Tour(int authorId, string name, string? description, Demandigness? demandignessLevel,List<string>? tags, TourStatus status = TourStatus.Draft,double price=0)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            if (authorId == 0) throw new ArgumentException("Inavlid author");
            if (price < 0) throw new ArgumentException("Invalid price");

            Name = name;
            Description = description;
            DemandignessLevel = demandignessLevel;
            Status = status;
            Price = price;
            AuthorId = authorId;
            Tags = tags;
        }
    }
}

public enum TourStatus
{
    Draft
}

public enum Demandigness
{
    Easy,
    Medium,
    Hard
}
