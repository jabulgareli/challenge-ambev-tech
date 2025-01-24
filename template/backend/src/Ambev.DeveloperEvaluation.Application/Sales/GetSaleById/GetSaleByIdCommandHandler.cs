using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Results;
using AutoMapper;
using MediatR;


namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById
{
    public class GetSaleByIdCommandHandler(
        ISaleRepository repository,
        IMapper mapper) : IRequestHandler<GetSaleByIdCommand, DataResult<GetSaleByIdResult>>
    {
        public async Task<DataResult<GetSaleByIdResult>> Handle(GetSaleByIdCommand request, CancellationToken cancellationToken)
        {
            var sale = await repository.GetByIdAsync(request.Id, cancellationToken);

            if (sale is null) return DataResult<GetSaleByIdResult>.Fail("Sale not found", 404);

            return DataResult<GetSaleByIdResult>.Success(mapper.Map<GetSaleByIdResult>(sale));
        }
    }
}
