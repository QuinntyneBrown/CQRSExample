using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using CQRSExample.Infrastructure.Data;
using CQRSExample.Infrastructure.Services;
using CQRSExample.Infrastructure.Requests;
using CQRSExample.Core.Entities;

namespace CQRSExample.WebAPI.Features.Roles
{
    public class AddOrUpdateRoleCommand
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response>
        {            
            public RoleApiModel Role { get; set; }
        }

        public class Response
        {            
            public int RoleId { get; set; }
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
                var role = await _context.Roles
                    .SingleOrDefaultAsync(x => x.RoleId == request.Role.RoleId);
                
                if (role == null)
                    _context.Roles.Add(role = new Role());

                role.Name = request.Role.Name;
                
                await _context.SaveChangesAsync(cancellationToken, request.Username);

                return new Response() { RoleId = role.RoleId };
            }

            private readonly IAppDataContext _context;
            private readonly ICache _cache;
        }
    }
}
