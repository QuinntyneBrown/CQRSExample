using MediatR;
using System.Threading.Tasks;
using System.Threading;
using CQRSExample.Infrastructure.Data;
using CQRSExample.Infrastructure.Services;
using CQRSExample.Infrastructure.Requests;
using CQRSExample.Core.Entities;

namespace CQRSExample.Features.Orders
{
    public class AddOrUpdateOrderCommand
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response>
        {            
            public OrderApiModel Order { get; set; }
        }

        public class Response
        {            
            public int OrderId { get; set; }
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
                var order = await _context.Orders.FindAsync(request.Order.OrderId);
                
                if (order == null)
                    _context.Orders.Add(order = new Order());

                order.Name = request.Order.Name;
                
                await _context.SaveChangesAsync(cancellationToken, request.Username);

                return new Response() { OrderId = order.OrderId };
            }

            private readonly IAppDataContext _context;
            private readonly ICache _cache;
        }
    }
}
