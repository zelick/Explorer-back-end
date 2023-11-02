using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Shopping;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class CustomerService: CrudService<CustomerDto, Customer>, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _customerRepository =  repository;
        }

        //mi cemo imati neku post metodu u kontroleru - to je checkout 
        //i on kad klikne checkout meni treba da se doda toliko tokena koliko imam itemsa
        //u kontroleru poziv servisa - tj ovog servisa
     
        //ovo da vrti kroz neku for petlju, za svaki orderItem 
        public Result <CustomerDto> ShopingCartCheckOut(long customerId)
        {
            var customer = _customerRepository.Get(customerId);
            var token = new TourPurchaseToken(customerId, 2);
            customer.CustomersPurchaseTokens(token);
            var result = _customerRepository.Update(customer);
            return MapToDto(result);
        }

        //umesto id - TourDto
        public List<long> getCustomersPurchasedTours(long customerId)
        {
            var customer = _customerRepository.Get(customerId);
            List<long> toursIds = new List<long>();

            foreach (var token in customer.PurchaseTokens)
            {
                toursIds.Add(token.TourId);
            }

            return toursIds;
        }
    }
}
