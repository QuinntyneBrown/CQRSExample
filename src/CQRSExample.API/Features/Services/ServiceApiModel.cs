using CQRSExample.Core.Entities;

namespace CQRSExample.Features.Services
{
    public class ServiceApiModel
    {        
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public float HourlyRate { get; set; }

        public static ServiceApiModel FromService(Service service)
            => new ServiceApiModel
            {
                ServiceId = service.ServiceId,
                Name = service.Name,
                HourlyRate = service.HourlyRate
            };
    }
}
