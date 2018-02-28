using MediatR;
using System.Threading.Tasks;
using System.Threading;
using CQRSExample.Infrastructure.Data;
using CQRSExample.Infrastructure.Requests;
using CQRSExample.Core.Entities;

namespace CQRSExample.Features.Products
{
    public class AddOrUpdateProductCommand
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response>
        {            
            public ProductApiModel Product { get; set; }
        }

        public class Response
        {            
            public int ProductId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public Handler(IAppDataContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.FindAsync(request.Product.ProductId);
                
                if (product == null) _context.Products.Add(product = new Product());

                product.Name = request.Product.Name;
                
                await _context.SaveChangesAsync(cancellationToken, request.Username);

                return new Response() { ProductId = product.ProductId };
            }

            private readonly IAppDataContext _context;
        }
    }
}
