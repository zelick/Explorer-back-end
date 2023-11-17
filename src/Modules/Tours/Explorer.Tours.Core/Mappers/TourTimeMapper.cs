using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Mappers
{
    public class TourTimeMapper
    {
        public TourTimeMapper() { }

        public TourTimeDto createDto(TourTime tourTime)
        {
            TourTimeDto result=new TourTimeDto();
            result.Transportation=tourTime.Transportation.ToString();
            result.Distance = tourTime.Distance;
            result.TimeInSeconds = tourTime.TimeInSeconds;
            return result;  
        }

        public List<TourTimeDto> createListDto(List<TourTime>? tourTimes) 
        {
            List<TourTimeDto> result=new List<TourTimeDto>();
            if(tourTimes !=null)
            {
                foreach(TourTime t in tourTimes)
                {
                    result.Add(createDto(t));
                }
            }
            return result;
        }
    }
}
