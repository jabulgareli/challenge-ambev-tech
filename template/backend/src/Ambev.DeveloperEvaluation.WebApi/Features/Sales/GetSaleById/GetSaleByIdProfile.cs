using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById
{
    public class GetSaleByIdProfile : Profile
    {
        public GetSaleByIdProfile()
        {
            CreateMap<GetSaleByIdResult, GetSaleByIdResponse>();
            CreateMap<GetSaleByIdItemResponse, GetSaleByIdItemResponse>();
        }
    }
}
