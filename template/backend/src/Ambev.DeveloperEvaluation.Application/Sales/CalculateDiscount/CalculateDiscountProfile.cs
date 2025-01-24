using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CalculateDiscount
{
    internal class CalculateDiscountProfile : Profile
    {
        public CalculateDiscountProfile()
        {
            CreateMap<CalculateDiscountCommand, Sale>();
            CreateMap<CalculateDiscountItemCommand, SaleItem>();
            CreateMap<Sale, SaleDiscountHistory>();
            CreateMap<SaleItem, SaleDiscountItemHistory>();
            CreateMap<Sale, CalculateDiscountResult>();
            CreateMap<SaleItem, CalculateDiscountItemResult>();
        }
    }
}
