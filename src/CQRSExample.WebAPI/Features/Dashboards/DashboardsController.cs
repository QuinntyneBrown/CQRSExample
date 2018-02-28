using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CQRSExample.Features.Dashboards
{
    [Authorize]
    [Route("api/dashboards")]
    public class DashboardsController : Controller
    {
        private readonly IMediator _mediator;

        public DashboardsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]AddOrUpdateDashboardCommand.Request request)
            => Ok(await _mediator.Send(request));
        
        [Route("update")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]AddOrUpdateDashboardCommand.Request request)
            => Ok(await _mediator.Send(request));
        
        [Route("get")]
        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _mediator.Send(new GetDashboardsQuery.Request()));

        [Route("getById")]
        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery]GetDashboardByIdQuery.Request request)
            => Ok(await _mediator.Send(request));

        [Route("remove")]
        [HttpDelete]
        public async Task<IActionResult> Remove([FromQuery]RemoveDashboardCommand.Request request) {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
