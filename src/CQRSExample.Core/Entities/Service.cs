namespace CQRSExample.Core.Entities
{
    public class Service: BaseModel
    {
        public int ServiceId { get; set; }           
		public string Name { get; set; }
        public float HourlyRate { get; set; }
    }
}
