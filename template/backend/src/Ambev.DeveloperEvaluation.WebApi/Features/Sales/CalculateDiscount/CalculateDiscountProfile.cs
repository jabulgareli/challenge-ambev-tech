using Ambev.DeveloperEvaluation.Application.Sales.CalculateDiscount;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CalculateDiscount
{
    public class CalculateDiscountProfile : Profile
    {
        public CalculateDiscountProfile()
        {
            CreateMap<CalculateDiscountRequest, CalculateDiscountCommand>();
            CreateMap<CalculateDiscountItemRequest, CalculateDiscountItemCommand>();
            CreateMap<CalculateDiscountResult, CalculateDiscountResponse>();
            CreateMap<CalculateDiscountItemResult, CalculateDiscountItemResponse>();
        }
    }
}