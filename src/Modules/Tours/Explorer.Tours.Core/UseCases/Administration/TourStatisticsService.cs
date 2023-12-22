using AutoMapper;
using Explorer.Payments.API.Internal;
using Explorer.Payments.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourStatisticsService: ITourStatisticsService
    {
        private readonly IItemOwnershipService _tourOwnershipService; //tvoja implemenirana metoda iz InternalToirOwnershipServis
        //ostali rep
        public TourStatisticsService(IItemOwnershipService tourOwnershipService)
        {
           _tourOwnershipService = tourOwnershipService;
        }

    }
}
