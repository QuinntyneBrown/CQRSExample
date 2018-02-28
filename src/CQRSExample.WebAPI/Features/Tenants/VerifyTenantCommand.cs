using CQRSExample.Infrastructure.Data;
using CQRSExample.Infrastructure.Services;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace CQRSExample.Features.Tenants
{
    public class VerifyTenantCommand
    {
        public class Request : IRequest
        {
            public Guid TenantId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            public Handler(IAppDataContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public Task Handle(Request request, CancellationToken cancellationToken)
            {
                if (request.TenantId != new Guid("bad9a182-ede0-418d-9588-2d89cfd555bd"))
                    throw new Exception("Invalid Request");

                return Task.CompletedTask;
            }

            private readonly IAppDataContext _context;
            private readonly ICache _cache;
        }
    }
}
