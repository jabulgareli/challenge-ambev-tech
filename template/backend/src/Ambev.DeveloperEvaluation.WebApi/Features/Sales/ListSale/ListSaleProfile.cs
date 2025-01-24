using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale
{
    public class ListSaleProfile : Profile
    {
        public ListSaleProfile()
        {
            CreateMap<ListSalesResult, ListSaleResponse>();
            CreateMap<ListSalesItemResult, ListSaleItemResult>();
        }
    }
}
