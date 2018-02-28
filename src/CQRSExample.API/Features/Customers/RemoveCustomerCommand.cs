using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using CQRSExample.Infrastructure.Data;
using CQRSExample.Infrastructure.Services;
using CQRSExample.Infrastructure.Requests;

namespace CQRSExample.API.Features.Customers
{
    public class RemoveCustomerCommand
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
                _context.Customers.Remove(await _context.Customers.FindAsync(request.Id));
                await _context.SaveChangesAsync(cancellationToken, request.Username);
            }

            private readonly IAppDataContext _context;
            private readonly ICache _cache;
        }
    }
}
