using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;

namespace Explorer.Payments.Core.UseCases
{
    public class CouponService : CrudService<CouponDto, Coupon>, ICouponService
    {
        private readonly ICouponRepository _couponRepository;

        public CouponService(ICrudRepository<Coupon> repository, ICouponRepository couponRepository, IMapper mapper) : base(
            repository, mapper)
        {
            _couponRepository = couponRepository;
        }
    }
}
