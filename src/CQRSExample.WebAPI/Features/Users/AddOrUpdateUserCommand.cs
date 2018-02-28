using MediatR;
using System.Threading.Tasks;
using System.Threading;
using CQRSExample.Infrastructure.Data;
using CQRSExample.Infrastructure.Services;
using CQRSExample.Infrastructure.Requests;
using CQRSExample.Core.Entities;

namespace CQRSExample.Features.Users
{
    public class AddOrUpdateUserCommand
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response>
        {            
            public UserApiModel User { get; set; }
        }

        public class Response
        {            
            public int UserId { get; set; }
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
                var user = await _context.Users.FindAsync(request.User.UserId);
                
                if (user == null)
                    _context.Users.Add(user = new User());

                user.Username = request.User.Username;
                
                await _context.SaveChangesAsync(cancellationToken, request.Username);

                return new Response() { UserId = user.UserId };
            }

            private readonly IAppDataContext _context;
            private readonly ICache _cache;
        }
    }
}
