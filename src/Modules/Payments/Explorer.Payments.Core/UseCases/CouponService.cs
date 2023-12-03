using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Payments.Core.UseCases
{
    public class CouponService : CrudService<CouponDto, Coupon>, ICouponService
    {
        private readonly ICouponRepository _couponRepository;
        private readonly IMapper _mapper;

        public CouponService(ICrudRepository<Coupon> repository, ICouponRepository couponRepository, IMapper mapper) : base(
            repository, mapper)
        {
            _couponRepository = couponRepository;
            _mapper = mapper;
        }

        public Result<CouponDto> Create(CreateCouponDto coupon)
        {
            try
            {
                var result = CrudRepository.Create(_mapper.Map<CreateCouponDto, Coupon>(coupon));
                return _mapper.Map<Coupon, CouponDto>(result);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
    }
}
