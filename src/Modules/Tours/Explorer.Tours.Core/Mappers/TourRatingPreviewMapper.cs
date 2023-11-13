using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Mappers
{
    public class TourRatingPreviewMapper
    {
        public TourRatingPreviewMapper() { }

        public TourRatingPreviewDto createDto(TourRatingPreview tourRatingPreview)
        {
            TourRatingPreviewDto result = new TourRatingPreviewDto();
            result.Id = tourRatingPreview.Id;
            result.TourDate = tourRatingPreview.TourDate;
            result.CreationDate= tourRatingPreview.CreationDate;
            result.Rating= tourRatingPreview.Rating;
            result.ImageNames = tourRatingPreview.ImageNames;
            result.Comment= tourRatingPreview.Comment;
            result.TouristId= tourRatingPreview.TouristId;  
            return result;

        }

        public List<TourRatingPreviewDto> createListDto(List<TourRatingPreview> tourRatingPreviews) 
        { 
            List<TourRatingPreviewDto> result = new List<TourRatingPreviewDto>();
            foreach(TourRatingPreview tourRatingPreview in tourRatingPreviews)
            {
                result.Add(createDto(tourRatingPreview));
            }
            return result;
        }
    }
}
