using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using CQRSExample.Infrastructure.Data;
using CQRSExample.Infrastructure.Services;
using CQRSExample.Infrastructure.Requests;
using CQRSExample.Core.Entities;

namespace CQRSExample.Features.Dashboards
{
    public class AddOrUpdateDashboardCommand
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response>
        {            
            public DashboardApiModel Dashboard { get; set; }
        }

        public class Response
        {            
            public int DashboardId { get; set; }
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
                var dashboard = await _context.Dashboards.FindAsync(request.Dashboard.DashboardId);
                
                if (dashboard == null)
                    _context.Dashboards.Add(dashboard = new Dashboard());

                dashboard.Name = request.Dashboard.Name;
                
                await _context.SaveChangesAsync(cancellationToken, request.Username);

                return new Response() { DashboardId = dashboard.DashboardId };
            }

            private readonly IAppDataContext _context;
            private readonly ICache _cache;
        }

    }

}
