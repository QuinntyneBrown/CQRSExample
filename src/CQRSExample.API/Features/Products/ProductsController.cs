using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace CQRSExample.Features.Products
{
    [Authorize]
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ProducesResponseType(typeof(AddOrUpdateProductCommand.Response),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add([FromBody]AddOrUpdateProductCommand.Request request)
            => Ok(await _mediator.Send(request));
        
        [Route("update")]
        [HttpPut]
        [ProducesResponseType(typeof(AddOrUpdateProductCommand.Response),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody]AddOrUpdateProductCommand.Request request)
            => Ok(await _mediator.Send(request));
        
        [Route("get")]
        [HttpGet]
        [ProducesResponseType(typeof(GetProductsQuery.Response),(int)HttpStatusCode.OK)] 
        public async Task<IActionResult> Get()
            => Ok(await _mediator.Send(new GetProductsQuery.Request()));

        [Route("getById")]
        [HttpGet]
        [ProducesResponseType(typeof(GetProductByIdQuery.Response),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById([FromQuery]GetProductByIdQuery.Request request)
            => Ok(await _mediator.Send(request));

        [Route("remove")]
        [HttpDelete]
        [ProducesResponseType(typeof(void),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> Remove([FromQuery]RemoveProductCommand.Request request) {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
