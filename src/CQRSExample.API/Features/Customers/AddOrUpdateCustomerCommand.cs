using MediatR;
using System.Threading.Tasks;
using System.Threading;
using CQRSExample.Infrastructure.Data;
using CQRSExample.Infrastructure.Services;
using CQRSExample.Infrastructure.Requests;
using CQRSExample.Core.Entities;

namespace CQRSExample.API.Features.Customers
{
    public class AddOrUpdateCustomerCommand
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response>
        {            
            public CustomerApiModel Customer { get; set; }
        }

        public class Response
        {            
            public int CustomerId { get; set; }
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
                var customer = await _context.Customers.FindAsync(request.Customer.CustomerId);
                
                if (customer == null)
                    _context.Customers.Add(customer = new Customer());

                customer.Firstname = request.Customer.Firstname;
                
                await _context.SaveChangesAsync(cancellationToken, request.Username);

                return new Response() { CustomerId = customer.CustomerId };
            }

            private readonly IAppDataContext _context;
            private readonly ICache _cache;
        }
    }
}
