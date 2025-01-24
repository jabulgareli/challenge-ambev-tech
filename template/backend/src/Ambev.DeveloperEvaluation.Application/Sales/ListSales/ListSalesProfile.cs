using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    internal class ListSalesProfile : Profile
    {
        public ListSalesProfile()
        {
            CreateMap<Sale, ListSalesResult>();
            CreateMap<SaleItem, ListSalesItemResult>();
        }
    }
}
