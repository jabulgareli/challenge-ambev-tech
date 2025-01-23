using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController(
        IMediator mediator,
        IMapper mapper,
        ILogger<SalesController> logger) : BaseController
    {

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSaleAsync([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new CreateSaleRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = mapper.Map<CreateSaleCommand>(request);
                var response = await mediator.Send(command, cancellationToken);

                return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
                {
                    Success = true,
                    Message = "Sale created successfully",
                    Data = mapper.Map<CreateSaleResponse>(response)
                });
            }
            catch(ValidationException ex)
            {
                return BadRequest(new ApiResponse { Message = ex.Message, Success = false });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch("{id}/satus/cancel")]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CancelSaleAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var command = new CancelSaleCommand(id);
                var response = await mediator.Send(command, cancellationToken);

                if (response.IsSuccess) return Ok();

                return response.Code switch  {
                    StatusCodes.Status400BadRequest => BadRequest(new ApiResponse { Message = response.Message, Success = false }),
                    StatusCodes.Status404NotFound => NotFound(new ApiResponse { Message = response.Message, Success = false }),
                    _ => StatusCode(response.Code)
                };
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ApiResponse { Message = ex.Message, Success = false });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch("{id}/products/{productId}/cancel")]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CancelItemAsync(
            [FromRoute] Guid id,
            [FromRoute] Guid productId,
            CancellationToken cancellationToken)
        {
            try
            {
                var command = new CancelSaleItemCommand(id, productId);
                var response = await mediator.Send(command, cancellationToken);

                if (response.IsSuccess) return Ok();

                return response.Code switch
                {
                    StatusCodes.Status400BadRequest => BadRequest(new ApiResponse { Message = response.Message, Success = false }),
                    StatusCodes.Status404NotFound => NotFound(new ApiResponse { Message = response.Message, Success = false }),
                    _ => StatusCode(response.Code)
                };
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ApiResponse { Message = ex.Message, Success = false });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
