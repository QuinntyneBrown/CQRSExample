using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using CQRSExample.Infrastructure.Data;
using CQRSExample.Infrastructure.Services;
using CQRSExample.Infrastructure.Requests;
using CQRSExample.Core.Entities;

namespace CQRSExample.Features.ServiceProviders
{
    public class AddOrUpdateServiceProviderCommand
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response>
        {            
            public ServiceProviderApiModel ServiceProvider { get; set; }
        }

        public class Response
        {            
            public int ServiceProviderId { get; set; }
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
                var serviceProvider = await _context.ServiceProviders.FindAsync(request.ServiceProvider.ServiceProviderId);
                
                if (serviceProvider == null)
                    _context.ServiceProviders.Add(serviceProvider = new ServiceProvider());

                serviceProvider.Name = request.ServiceProvider.Name;
                
                await _context.SaveChangesAsync(cancellationToken, request.Username);

                return new Response() { ServiceProviderId = serviceProvider.ServiceProviderId };
            }

            private readonly IAppDataContext _context;
            private readonly ICache _cache;
        }

    }

}
