using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Shopping;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class CustomerService: CrudService<CustomerDto, Customer>, ICustomerService, IInternalShoppingService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public CustomerService(ICustomerRepository repository, IShoppingCartRepository shoppingCartRepository, IMapper mapper) : base(repository, mapper)
        {
            _customerRepository =  repository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public Result <CustomerDto> ShopingCartCheckOut(long touristId)
        {
            //var customer = _customerRepository.Get(customerId);
            var customer = _customerRepository.GetCustomerByTouristId(touristId); //
            var shopingCart = _shoppingCartRepository.Get(customer.ShoppingCartId);

            foreach (var orderItem in shopingCart.Items)
            {
                var token = new TourPurchaseToken(customer.Id, orderItem.TourId); //
                customer.CustomersPurchaseTokens(token);
            }

            var result = _customerRepository.Update(customer);
            return MapToDto(result);
        }

        public List<long> getCustomersPurchasedTours(long touristId)
        {
            var customer = _customerRepository.GetCustomerByTouristId(touristId);

            if (customer != null)
            {
                List<long> toursIds = new List<long>();

                foreach (var token in customer.PurchaseTokens)
                {
                    toursIds.Add(token.TourId);
                }

                return toursIds;
            }
            else
            {
                return new List<long>();
            }
        }

        public bool IsTourPurchasedByUser(long touristId, long tourId)
        {
            return this.getCustomersPurchasedTours(touristId).Find(t => t == tourId) != null;
        }
    }
}
