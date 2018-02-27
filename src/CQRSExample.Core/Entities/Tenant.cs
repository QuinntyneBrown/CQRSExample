using System;

namespace CQRSExample.Core.Entities
{
    public class Tenant
    {
        public Guid TenantId { get; set; }           
		public string Name { get; set; }        
    }
}
