using MediatR;
using System.Threading.Tasks;
using System.Threading;
using CQRSExample.Infrastructure.Data;
using CQRSExample.Infrastructure.Services;
using CQRSExample.Infrastructure.Requests;
using CQRSExample.Core.Entities;
using System;

namespace CQRSExample.Features.Tenants
{
    public class AddOrUpdateTenantCommand
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response>
        {            
            public TenantApiModel Tenant { get; set; }
        }

        public class Response
        {            
            public Guid TenantId { get; set; }
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
                var tenant = await _context.Tenants.FindAsync(request.Tenant.TenantId);
                
                if (tenant == null)
                    _context.Tenants.Add(tenant = new Tenant());

                tenant.Name = request.Tenant.Name;
                
                await _context.SaveChangesAsync(cancellationToken, request.Username);

                return new Response() { TenantId = tenant.TenantId };
            }

            private readonly IAppDataContext _context;
            private readonly ICache _cache;
        }

    }

}
