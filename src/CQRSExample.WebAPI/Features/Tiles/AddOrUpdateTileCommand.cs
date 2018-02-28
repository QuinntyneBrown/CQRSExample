using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using CQRSExample.Infrastructure.Data;
using CQRSExample.Infrastructure.Services;
using CQRSExample.Infrastructure.Requests;
using CQRSExample.Core.Entities;

namespace CQRSExample.Features.Tiles
{
    public class AddOrUpdateTileCommand
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response>
        {            
            public TileApiModel Tile { get; set; }
        }

        public class Response
        {            
            public int TileId { get; set; }
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
                var tile = await _context.Tiles
                    .SingleOrDefaultAsync(x => x.TileId == request.Tile.TileId);
                
                if (tile == null)
                    _context.Tiles.Add(tile = new Tile());

                tile.Name = request.Tile.Name;
                
                await _context.SaveChangesAsync(cancellationToken, request.Username);

                return new Response() { TileId = tile.TileId };
            }

            private readonly IAppDataContext _context;
            private readonly ICache _cache;
        }

    }

}
