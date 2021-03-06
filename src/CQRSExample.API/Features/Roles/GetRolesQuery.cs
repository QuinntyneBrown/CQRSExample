using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using CQRSExample.Infrastructure.Data;
using CQRSExample.Infrastructure.Services;
using CQRSExample.Infrastructure.Requests;

namespace CQRSExample.Features.Roles
{
    public class GetRolesQuery
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response> { }

        public class Response
        {
            public ICollection<RoleApiModel> Roles { get; set; } = new HashSet<RoleApiModel>();
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
                    Roles = await _context.Roles.Select(x => RoleApiModel.FromRole(x)).ToListAsync()
                };
            }

            private readonly IAppDataContext _context;
            private readonly ICache _cache;
        }
    }
}
