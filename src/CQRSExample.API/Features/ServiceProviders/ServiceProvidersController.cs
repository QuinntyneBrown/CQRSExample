using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace CQRSExample.Features.ServiceProviders
{
    [Authorize]
    [Route("api/serviceproviders")]
    public class ServiceProvidersController : Controller
    {
        private readonly IMediator _mediator;

        public ServiceProvidersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ProducesResponseType(typeof(AddOrUpdateServiceProviderCommand.Response),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add([FromBody]AddOrUpdateServiceProviderCommand.Request request)
            => Ok(await _mediator.Send(request));
        
        [Route("update")]
        [HttpPut]
        [ProducesResponseType(typeof(AddOrUpdateServiceProviderCommand.Response),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody]AddOrUpdateServiceProviderCommand.Request request)
            => Ok(await _mediator.Send(request));
        
        [Route("get")]
        [HttpGet]
        [ProducesResponseType(typeof(GetServiceProvidersQuery.Response),(int)HttpStatusCode.OK)] 
        public async Task<IActionResult> Get()
            => Ok(await _mediator.Send(new GetServiceProvidersQuery.Request()));

        [Route("getById")]
        [HttpGet]
        [ProducesResponseType(typeof(GetServiceProviderByIdQuery.Response),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById([FromQuery]GetServiceProviderByIdQuery.Request request)
            => Ok(await _mediator.Send(request));

        [Route("remove")]
        [HttpDelete]
        [ProducesResponseType(typeof(void),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> Remove([FromQuery]RemoveServiceProviderCommand.Request request) {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
