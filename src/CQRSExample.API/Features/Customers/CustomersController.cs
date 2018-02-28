using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace CQRSExample.API.Features.Customers
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
        [ProducesResponseType(typeof(AddOrUpdateCustomerCommand.Response),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add([FromBody]AddOrUpdateCustomerCommand.Request request)
            => Ok(await _mediator.Send(request));
        
        [Route("update")]
        [HttpPut]
        [ProducesResponseType(typeof(AddOrUpdateCustomerCommand.Response), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody]AddOrUpdateCustomerCommand.Request request)
            => Ok(await _mediator.Send(request));
        
        [Route("get")]
        [HttpGet]
        [ProducesResponseType(typeof(GetCustomersQuery.Response), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
            => Ok(await _mediator.Send(new GetCustomersQuery.Request()));

        [Route("getById")]
        [HttpGet]
        [ProducesResponseType(typeof(GetCustomerByIdQuery.Response), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById([FromQuery]GetCustomerByIdQuery.Request request)
            => Ok(await _mediator.Send(request));

        [Route("remove")]
        [HttpDelete]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Remove([FromQuery]RemoveCustomerCommand.Request request) {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
