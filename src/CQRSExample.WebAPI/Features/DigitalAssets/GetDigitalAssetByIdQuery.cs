using MediatR;
using System.Threading.Tasks;
using System;
using System.Threading;
using CQRSExample.Infrastructure.Data;
using CQRSExample.Infrastructure.Services;
using CQRSExample.Infrastructure.Requests;

namespace CQRSExample.WebAPI.Features.DigitalAssets
{
    public class GetDigitalAssetByIdQuery
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response>
        {
            public Guid DigitalAssetId { get; set; }
        }

        public class Response
        {
            public DigitalAssetApiModel DigitalAsset { get; set; }
        }

        public class GetDigitalAssetByUniqueIdHandler : IRequestHandler<Request, Response>
        {
            public GetDigitalAssetByUniqueIdHandler(IAppDataContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new Response()
                {
                    DigitalAsset = DigitalAssetApiModel.FromDigitalAsset(await _context
                    .DigitalAssets
                    .FindAsync(request.DigitalAssetId))
                };
            }

            private readonly IAppDataContext _context;
            private readonly ICache _cache;
        }
    }
}
