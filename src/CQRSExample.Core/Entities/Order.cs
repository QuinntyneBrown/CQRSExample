namespace CQRSExample.Core.Entities
{
    public class Order: BaseModel
    {
        public int OrderId { get; set; }           
		public string Name { get; set; }        
    }
}
