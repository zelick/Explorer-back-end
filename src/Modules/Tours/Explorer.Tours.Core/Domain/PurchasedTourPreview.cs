using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class PurchasedTourPreview
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public Demandigness? DemandignessLevel { get; init; }
        public double Price { get; init; }
        public List<string>? Tags { get; init; }
        public List<Equipment> Equipment { get; init; }
        public List <Checkpoint> Checkpoints { get; init; }
        public List<TourRatingPreview> TourRatings { get; init; }
        public List<TourTime> TourTimes { get; init; }

        public PurchasedTourPreview(Tour tour)
        {
            Id = tour.Id;
            Name = tour.Name;
            Description = tour.Description;
            DemandignessLevel = tour.DemandignessLevel;
            Price = tour.Price;
            Tags = tour.Tags;
            if (tour.Equipment != null)
                Equipment = tour.Equipment;
            else
                Equipment = new List<Equipment>();
            Checkpoints = tour.Checkpoints;
            TourTimes = tour.TourTimes;
            TourRatings = new List<TourRatingPreview>();

            if (tour.TourRatings != null)
            {
                foreach (TourRating tourRating in tour.TourRatings)
                {
                    TourRatings.Add(new TourRatingPreview(tourRating));
                }
            }

            
        }
    }
}