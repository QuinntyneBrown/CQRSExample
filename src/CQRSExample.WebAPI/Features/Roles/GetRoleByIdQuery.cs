using MediatR;
using System.Threading.Tasks;
using System.Threading;
using CQRSExample.Infrastructure.Data;
using CQRSExample.Infrastructure.Services;
using CQRSExample.Infrastructure.Requests;

namespace CQRSExample.WepAPI.Features.Roles
{
    public class GetRoleByIdQuery
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response> { 
            public int Id { get; set; }            
        }

        public class Response
        {
            public RoleApiModel Role { get; set; }
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
                return new Response()
                {
                    Role = RoleApiModel.FromRole(await _context.Roles.FindAsync(request.Id))
                };
            }

            private readonly IAppDataContext _context;
            private readonly ICache _cache;
        }
    }
}
