using CQRSExample.Core.Entities;

namespace CQRSExample.Features.ServiceProviders
{
    public class ServiceProviderApiModel
    {        
        public int ServiceProviderId { get; set; }
        public string Name { get; set; }

        public static ServiceProviderApiModel FromServiceProvider(ServiceProvider serviceProvider)
        {
            var model = new ServiceProviderApiModel();
            model.ServiceProviderId = serviceProvider.ServiceProviderId;
            model.Name = serviceProvider.Name;
            return model;
        }
    }
}
