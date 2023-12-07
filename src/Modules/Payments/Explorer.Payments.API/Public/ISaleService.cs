using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using FluentResults;

namespace Explorer.Payments.API.Public
{
    public interface ISaleService
    {
        Result<PagedResult<SaleDto>> GetPaged(int page, int pageSize);
        Result<SaleDto> Create(SaleDto sale);
        Result<SaleDto> Update(SaleDto saleDto);
        Result Delete(int id, int authorId);
        Result<SaleDto> Get(int id);
        Result<List<SaleDto>> GetActiveSales();
    }
}
