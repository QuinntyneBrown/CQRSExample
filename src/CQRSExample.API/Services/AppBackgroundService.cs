using System.Threading;
using System.Threading.Tasks;
using CQRSExample.Infrastructure.Data;
using Microsoft.Extensions.Hosting;

namespace CQRSExample.API.Services
{
    public class CacheInvalidatorBackgroundService : BackgroundService
    {
        private readonly IAppDataContext _context;
        public CacheInvalidatorBackgroundService(IAppDataContext context)
        {
            _context = context;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
