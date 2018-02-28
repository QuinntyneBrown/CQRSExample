using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using CQRSExample.Infrastructure.Data;
using CQRSExample.Infrastructure.Services;
using CQRSExample.Infrastructure.Requests;

namespace CQRSExample.Features.Tiles
{
    public class GetTilesQuery
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response> { }

        public class Response
        {
            public ICollection<TileApiModel> Tiles { get; set; } = new HashSet<TileApiModel>();
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public Handler(IAppDataContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new Response()
                {
                    Tiles = await _context.Tiles.Select(x => TileApiModel.FromTile(x)).ToListAsync()
                };
            }

            private readonly IAppDataContext _context;
            private readonly ICache _cache;
        }
    }
}
