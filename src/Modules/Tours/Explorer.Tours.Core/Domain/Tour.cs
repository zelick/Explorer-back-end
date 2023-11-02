using Explorer.BuildingBlocks.Core.Domain;
using System.ComponentModel.DataAnnotations;

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

        public List<Equipment> Equipment { get; init; }

        public List<Checkpoint> Checkpoints { get; init; }

        public double Distance { get; init; }

        public Tour AddEquipment(Equipment equip)
        {
            Equipment.Add(equip);
            return this;
        }

        public Tour(int authorId, string name, string? description, Demandigness? demandignessLevel, List<string>? tags, TourStatus status = TourStatus.Draft,double price=0)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            if (authorId == 0) throw new ArgumentException("Invalid author");
            if (price < 0) throw new ArgumentException("Invalid price");

            Name = name;
            Description = description;
            DemandignessLevel = demandignessLevel;
            Status = status;
            Price = price;
            AuthorId = authorId;
            Tags = tags;
            Equipment = new List<Equipment>();
            Checkpoints = new List<Checkpoint>();
            Distance = 0;
        }


        public Tour FilterView(Tour tour)
        {
            if (tour.Checkpoints.Count > 0) {
                Checkpoint cp = tour.Checkpoints[0];
                tour.Checkpoints.Clear();
                tour.Checkpoints.Add(cp);
            };


            return tour;
        }

    }
}

public enum TourStatus
{
    Draft,
    Published,
    Archived
}

public enum Demandigness
{
    Easy,
    Medium,
    Hard
}
