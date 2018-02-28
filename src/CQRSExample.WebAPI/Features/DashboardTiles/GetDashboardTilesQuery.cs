using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using CQRSExample.Infrastructure.Data;
using CQRSExample.Infrastructure.Services;
using CQRSExample.Infrastructure.Requests;

namespace CQRSExample.Features.DashboardTiles
{
    public class GetDashboardTilesQuery
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response> { }

        public class Response
        {
            public ICollection<DashboardTileApiModel> DashboardTiles { get; set; } = new HashSet<DashboardTileApiModel>();
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
                    DashboardTiles = await _context.DashboardTiles.Select(x => DashboardTileApiModel.FromDashboardTile(x)).ToListAsync()
                };
            }

            private readonly IAppDataContext _context;
            private readonly ICache _cache;
        }
    }
}
