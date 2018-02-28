using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using CQRSExample.Infrastructure.Data;
using CQRSExample.Infrastructure.Requests;
using CQRSExample.Infrastructure.Services;

namespace CQRSExample.API.Features.DigitalAssets
{
    public class GetDigitalAssetsQuery
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response> { }

        public class Response
        {
            public ICollection<DigitalAssetApiModel> DigitalAssets { get; set; } = new HashSet<DigitalAssetApiModel>();
        }

        public class GetDigitalAssetsHandler : IRequestHandler<Request, Response>
        {
            public GetDigitalAssetsHandler(IAppDataContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var digitalAssets = await _context.DigitalAssets.ToListAsync();

                return new Response()
                {
                    DigitalAssets = digitalAssets.Select(x => DigitalAssetApiModel.FromDigitalAsset(x)).ToList()
                };
            }

            private readonly IAppDataContext _context;
            private readonly ICache _cache;
        }
    }
}