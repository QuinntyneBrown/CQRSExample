using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CQRSExample.API.Features.DigitalAssets
{
    [Authorize]
    [Route("api/digitalassets")]
    public class DigitalAssetController : Controller
    {
        private readonly IMediator _mediator;
        public DigitalAssetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ProducesResponseType(typeof(AddOrUpdateDigitalAssetCommand.Response), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add([FromBody]AddOrUpdateDigitalAssetCommand.Request request)
            => Ok(await _mediator.Send(request));

        [Route("update")]
        [HttpPut]
        [ProducesResponseType(typeof(AddOrUpdateDigitalAssetCommand.Response), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody]AddOrUpdateDigitalAssetCommand.Request request)
            => Ok(await _mediator.Send(request));

        [Route("get")]
        [HttpGet]
        [ProducesResponseType(typeof(GetDigitalAssetsQuery.Response), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
            => Ok(await _mediator.Send(new GetDigitalAssetsQuery.Request()));

        [Route("getMostRecent")]
        [HttpGet]
        [ProducesResponseType(typeof(GetMostRecentDigitalAssetsQuery.Response), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMostRecent()
            => Ok(await _mediator.Send(new GetMostRecentDigitalAssetsQuery.Request()));

        [Route("getById")]
        [HttpGet]
        [ProducesResponseType(typeof(GetDigitalAssetByIdQuery.Response), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById([FromQuery]GetDigitalAssetByIdQuery.Request request)
            => Ok(await _mediator.Send(request));

        [Route("remove")]
        [HttpDelete]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Remove([FromQuery]RemoveDigitalAssetCommand.Request request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [Route("serve")]
        [HttpGet]
        public async Task<IActionResult> Serve(GetDigitalAssetByIdQuery.Request request)
        {
            var response = await _mediator.Send(request);
            return File(response.DigitalAsset.Bytes, response.DigitalAsset.ContentType);
        }

        [HttpPost("upload")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Upload()
        {
            await _mediator.Send(new UploadDigitalAssetsCommand.Request());
            return Ok();
        }
    }
}