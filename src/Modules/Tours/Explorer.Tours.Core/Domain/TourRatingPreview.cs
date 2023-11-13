using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class TourRatingPreview 
    {
        public long Id { get; init; }
        public int Rating { get; init; }
        public string? Comment { get; init; }
        public int TouristId { get; init; }
        public DateTime TourDate { get; init; }
        public DateTime CreationDate { get; init; }
        public string[]? Pictures { get; init; }

        public TourRatingPreview(TourRating tourRating)
        {
            Id = tourRating.Id;
            Rating = tourRating.Rating;
            Comment = tourRating.Comment;
            TouristId = tourRating.TouristId;
            CreationDate = tourRating.CreationDate;
            TourDate = tourRating.CreationDate;
            Pictures = tourRating.Pictures;
        }
       
    }
}
