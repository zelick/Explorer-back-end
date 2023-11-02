using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos.Shopping;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Shopping;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ShoppingCartService : CrudService<ShoppingCartDto, ShoppingCart>, IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        public ShoppingCartService(IShoppingCartRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _shoppingCartRepository = repository;
        }

        public Result<bool> CheckIfShoppingCartExists(int touristId)
        {
            try
            {
                bool exists = _shoppingCartRepository.ShoppingCartExists(touristId);
                return Result.Ok(exists);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<ShoppingCartDto> GetShoppingCart(int touristId)
        {
            try
            {
                var result = _shoppingCartRepository.GetShoppingCart(touristId);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<ShoppingCartDto> AddItemToShoppingCart(OrderItemDto item, int touristId)
        {
            var cart = _shoppingCartRepository.Get(touristId);
            var newItem = new OrderItem(item.TourId, item.Price);
            cart.AddItemToShoppingCart(newItem);
            var result = _shoppingCartRepository.Update(cart);
            return MapToDto(result);
        }
    }
}
