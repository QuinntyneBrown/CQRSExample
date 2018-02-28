using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using CQRSExample.Infrastructure.Data;
using CQRSExample.Infrastructure.Services;
using CQRSExample.Infrastructure.Requests;
using CQRSExample.Core.Entities;

namespace CQRSExample.Features.DashboardTiles
{
    public class AddOrUpdateDashboardTileCommand
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response>
        {            
            public DashboardTileApiModel DashboardTile { get; set; }
        }

        public class Response
        {            
            public int DashboardTileId { get; set; }
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
                var dashboardTile = await _context.DashboardTiles
                    .SingleOrDefaultAsync(x => x.DashboardTileId == request.DashboardTile.DashboardTileId);
                
                if (dashboardTile == null)
                    _context.DashboardTiles.Add(dashboardTile = new DashboardTile());

                dashboardTile.Name = request.DashboardTile.Name;
                
                await _context.SaveChangesAsync(cancellationToken, request.Username);

                return new Response() { DashboardTileId = dashboardTile.DashboardTileId };
            }

            private readonly IAppDataContext _context;
            private readonly ICache _cache;
        }

    }

}
