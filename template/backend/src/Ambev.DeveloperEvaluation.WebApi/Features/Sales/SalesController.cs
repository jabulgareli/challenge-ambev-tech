using Ambev.DeveloperEvaluation.Application.Sales.CanceItem;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Enums.Usres;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
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

        [Authorize(Roles = $"{nameof(UserRole.Admin)}, {nameof(UserRole.Manager)}")]
        [HttpPatch("{id}/satus/cancel")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        [Authorize(Roles = $"{nameof(UserRole.Admin)}, {nameof(UserRole.Manager)}")]
        [HttpPatch("{id}/products/{productId}/cancel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
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

        [Authorize(Roles = $"{nameof(UserRole.Admin)}, {nameof(UserRole.Manager)}")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSaleAsync(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            try
            {
                var command = new DeleteSaleCommand(id);
                var response = await mediator.Send(command, cancellationToken);

                if (response.IsSuccess) return Ok();

                return response.Code switch
                {
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


        [Authorize(Roles = $"{nameof(UserRole.Admin)}, {nameof(UserRole.Manager)}")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSaleAsync(
           [FromRoute] Guid id,
           [FromBody] UpdateSaleRequest request,
           CancellationToken cancellationToken)
        {
            try
            {
                var validator = new UpdateSaleRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = mapper.Map<UpdateSaleCommand>(request);
                command.Id = id;
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

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetSaleByIdResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            try
            {
                var command = new GetSaleByIdCommand(id);
                var response = await mediator.Send(command, cancellationToken);

                if (response.IsSuccess) return Ok(response.Data);

                return response.Code switch
                {
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

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SearchAsync(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            try
            {
                return Ok();
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
