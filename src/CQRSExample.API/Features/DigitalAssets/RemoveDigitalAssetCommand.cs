using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using CQRSExample.Infrastructure.Data;
using CQRSExample.Infrastructure.Requests;
using CQRSExample.Infrastructure.Services;

namespace CQRSExample.API.Features.DigitalAssets
{
    public class RemoveDigitalAssetCommand
    {
        public class Request : BaseAuthenticatedRequest, IRequest
        {
            public Guid Id { get; set; }
        }

        public class Response { }

        public class Handler : IRequestHandler<Request>
        {
            public Handler(IAppDataContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                _context.DigitalAssets.Remove(await _context.DigitalAssets.FindAsync(request.Id));

                await _context.SaveChangesAsync(cancellationToken, request.Username);
            }

            private readonly IAppDataContext _context;
            private readonly ICache _cache;
        }
    }
}
