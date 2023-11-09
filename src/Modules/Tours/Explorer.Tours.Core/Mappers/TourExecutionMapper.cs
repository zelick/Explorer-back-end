using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.Mappers
{
    public class TourExecutionMapper
    {
        private PurchasedTourPreviewMapper _mapper;
        public TourExecutionMapper()
        {
            _mapper = new PurchasedTourPreviewMapper();
        }

        public TourExecutionDto createDto(TourExecution tourExecution)
        {
            TourExecutionDto result = new TourExecutionDto();
            result.TouristId = tourExecution.TouristId;
            result.Tour = _mapper.createDto(tourExecution.Tour.FilterPurchasedTour(tourExecution.Tour));
            result.ExecutionStatus = tourExecution.ExecutionStatus.ToString();
            result.Start = tourExecution.Start;
            result.LastActivity = tourExecution.LastActivity;

            return result;
        }
   
    }
}
