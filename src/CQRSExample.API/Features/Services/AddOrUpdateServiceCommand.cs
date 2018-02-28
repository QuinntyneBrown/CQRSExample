using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using CQRSExample.Infrastructure.Data;
using CQRSExample.Infrastructure.Services;
using CQRSExample.Infrastructure.Requests;
using CQRSExample.Core.Entities;

namespace CQRSExample.Features.Services
{
    public class AddOrUpdateServiceCommand
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response>
        {            
            public ServiceApiModel Service { get; set; }
        }

        public class Response
        {            
            public int ServiceId { get; set; }
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
                var service = await _context.Services.FindAsync(request.Service.ServiceId);
                
                if (service == null)
                    _context.Services.Add(service = new Service());

                service.Name = request.Service.Name;
                
                await _context.SaveChangesAsync(cancellationToken, request.Username);

                return new Response() { ServiceId = service.ServiceId };
            }

            private readonly IAppDataContext _context;
            private readonly ICache _cache;
        }

    }

}
