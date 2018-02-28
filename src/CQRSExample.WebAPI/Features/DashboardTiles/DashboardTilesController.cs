using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CQRSExample.Features.DashboardTiles
{
    [Authorize]
    [Route("api/dashboardtiles")]
    public class DashboardTilesController : Controller
    {
        private readonly IMediator _mediator;

        public DashboardTilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]AddOrUpdateDashboardTileCommand.Request request)
            => Ok(await _mediator.Send(request));
        
        [Route("update")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]AddOrUpdateDashboardTileCommand.Request request)
            => Ok(await _mediator.Send(request));
        
        [Route("get")]
        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _mediator.Send(new GetDashboardTilesQuery.Request()));

        [Route("getById")]
        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery]GetDashboardTileByIdQuery.Request request)
            => Ok(await _mediator.Send(request));

        [Route("remove")]
        [HttpDelete]
        public async Task<IActionResult> Remove([FromQuery]RemoveDashboardTileCommand.Request request) {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
