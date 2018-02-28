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
    public class GetDashboardByIdQuery
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response> { 
            public int Id { get; set; }            
        }

        public class Response
        {
            public DashboardApiModel Dashboard { get; set; }
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
                    Dashboard = DashboardApiModel.FromDashboard(await _context.Dashboards.FindAsync(request.Id))
                };
            }

            private readonly IAppDataContext _context;
            private readonly ICache _cache;
        }
    }
}
