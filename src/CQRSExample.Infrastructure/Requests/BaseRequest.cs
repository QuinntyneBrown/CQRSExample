using System;

namespace CQRSExample.Infrastructure.Requests
{
    public class BaseRequest
    {
        public Guid TenantId { get; set; }
    }
}
