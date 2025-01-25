using Application.Features.Product.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllProductCommand(), cancellationToken);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand createProduct, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(createProduct, cancellationToken);
            return Ok(result);
        }
    }
}
