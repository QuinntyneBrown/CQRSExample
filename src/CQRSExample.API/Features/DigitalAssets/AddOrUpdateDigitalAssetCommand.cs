using MediatR;
using CQRSExample.Core.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using CQRSExample.Infrastructure.Data;
using CQRSExample.Infrastructure.Services;
using CQRSExample.Infrastructure.Requests;


namespace CQRSExample.API.Features.DigitalAssets
{
    public class AddOrUpdateDigitalAssetCommand
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response>
        {
            public DigitalAssetApiModel DigitalAsset { get; set; }
        }

        public class Response { }

        public class AddOrUpdateDigitalAssetHandler : IRequestHandler<Request, Response>
        {
            public AddOrUpdateDigitalAssetHandler(IAppDataContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var entity = await _context.DigitalAssets
                    .SingleOrDefaultAsync(x => x.DigitalAssetId == request.DigitalAsset.DigitalAssetId && x.IsDeleted == false);
                if (entity == null) _context.DigitalAssets.Add(entity = new DigitalAsset());
                entity.Name = request.DigitalAsset.Name;
                entity.Folder = request.DigitalAsset.Folder;

                await _context.SaveChangesAsync(cancellationToken,request.Username);

                return new Response() { };
            }

            private readonly IAppDataContext _context;
            private readonly ICache _cache;
        }
    }
}
