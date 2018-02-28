using MediatR;
using System.Threading.Tasks;
using System.Threading;
using CQRSExample.Infrastructure.Data;
using CQRSExample.Infrastructure.Requests;
using CQRSExample.Infrastructure.Services;

namespace CQRSExample.WebAPI.Features.Roles
{
    public class RemoveRoleCommand
    {
        public class Request : BaseAuthenticatedRequest, IRequest { 
            public int Id { get; set; }            
        }

        public class Handler : IRequestHandler<Request>
        {
            public Handler(IAppDataContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                _context.Roles.Remove(await _context.Roles.FindAsync(request.Id));
                await _context.SaveChangesAsync(cancellationToken, request.Username);
            }

            private readonly IAppDataContext _context;
            private readonly ICache _cache;
        }
    }
}
