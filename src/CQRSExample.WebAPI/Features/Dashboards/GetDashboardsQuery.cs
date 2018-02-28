using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using CQRSExample.Infrastructure.Data;
using CQRSExample.Infrastructure.Services;
using CQRSExample.Infrastructure.Requests;

namespace CQRSExample.Features.Dashboards
{
    public class GetDashboardsQuery
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response> { }

        public class Response
        {
            public ICollection<DashboardApiModel> Dashboards { get; set; } = new HashSet<DashboardApiModel>();
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
                    Dashboards = await _context.Dashboards.Select(x => DashboardApiModel.FromDashboard(x)).ToListAsync()
                };
            }

            private readonly IAppDataContext _context;
            private readonly ICache _cache;
        }
    }
}
