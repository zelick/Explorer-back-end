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
        public SaleService(ICrudRepository<Sale> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {

        }

    }
}
