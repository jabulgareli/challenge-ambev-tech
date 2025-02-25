﻿using Ambev.DeveloperEvaluation.Domain.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public record CancelSaleCommand(Guid Id) : IRequest<Result>;
}
