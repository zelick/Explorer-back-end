using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface ICustomerService
    {
        Result<PagedResult<CustomerDto>> GetPaged(int page, int pageSize);
        Result<CustomerDto> Create(CustomerDto customer);
        Result<CustomerDto> Update(CustomerDto customer);
        Result Delete(int id);
        Result<CustomerDto> ShopingCartCheckOut(long customerId);
        List<long> getCustomersPurchasedTours (long customerId);
    }
}
