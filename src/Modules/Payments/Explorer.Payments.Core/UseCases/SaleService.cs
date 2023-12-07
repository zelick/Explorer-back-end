using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class SaleService : CrudService<SaleDto, Sale>, ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        public SaleService(ISaleRepository saleRepository, ICrudRepository<Sale> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
            _saleRepository = saleRepository;
        }

        public Result Delete(int id, int authorId)
        {
            Sale sale;
            try
            {
                sale = _saleRepository.Get(id);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            if (!sale.IsCreatedByUser(authorId))
                return Result.Fail(FailureCode.Forbidden);
            try
            {
                CrudRepository.Delete(id);
                return Result.Ok();
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<List<SaleDto>> GetActiveSales()
        {
            try
            {
                return MapToDto(_saleRepository.GetActiveSales());
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
    }
}