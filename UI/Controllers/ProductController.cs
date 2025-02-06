using Application.Features.Product.Commands;
using Application.Features.Product.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("getProducts")]
        public async Task<IActionResult> GetAllProduct(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllProductQuery(), cancellationToken);
            return Ok(result);
        }
        [HttpPost("createProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductCommand createProduct, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(createProduct, cancellationToken);
            return Ok(result);
        }
        [HttpPut("updateProduct")]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand updateProduct, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(updateProduct, cancellationToken);
            return Ok(result);
        }
        [HttpDelete("deleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteProductCommand() { Id = id }, cancellationToken);
            return Ok(result);
        }
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetProductById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetProductByIdQuery { Id = id }, cancellationToken);
            return Ok(result);
        }
    }
}
