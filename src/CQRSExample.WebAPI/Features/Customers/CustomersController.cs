using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CQRSExample.WepAPI.Features.Customers
{
    [Authorize]
    [Route("api/customers")]
    public class CustomersController : Controller
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]AddOrUpdateCustomerCommand.Request request)
            => Ok(await _mediator.Send(request));
        
        [Route("update")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]AddOrUpdateCustomerCommand.Request request)
            => Ok(await _mediator.Send(request));
        
        [Route("get")]
        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _mediator.Send(new GetCustomersQuery.Request()));

        [Route("getById")]
        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery]GetCustomerByIdQuery.Request request)
            => Ok(await _mediator.Send(request));

        [Route("remove")]
        [HttpDelete]
        public async Task<IActionResult> Remove([FromQuery]RemoveCustomerCommand.Request request) {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
