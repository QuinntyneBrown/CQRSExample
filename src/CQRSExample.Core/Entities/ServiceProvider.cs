namespace CQRSExample.Core.Entities
{
    public class ServiceProvider: BaseModel
    {
        public int ServiceProviderId { get; set; }           
		public string Name { get; set; }
        public Service Service { get; set; }
    }
}
