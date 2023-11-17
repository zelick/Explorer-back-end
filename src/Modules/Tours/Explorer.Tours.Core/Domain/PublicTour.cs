using Explorer.Stakeholders.Core.Domain.Shopping;
using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class PublicTour
    {
        public long Id { get; init; }
        public int AuthorId { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public double Price { get; init; }
        public List<string>? Tags { get; init; }
        public List<CheckpointPreview> PreviewCheckpoints { get; init; }

        public PublicTour() { }

        public PublicTour(Tour tour)
        {
            Id = tour.Id;
            AuthorId = tour.AuthorId;
            Name = tour.Name;
            Description = tour.Description;
            Price = tour.Price;
            Tags = tour.Tags;
            PreviewCheckpoints = new List<CheckpointPreview>();
           foreach(var checkpoint in tour.Checkpoints)
           {
                var check = new CheckpointPreview(checkpoint);
                PreviewCheckpoints.Add(check);
           }
        }
    }
}
