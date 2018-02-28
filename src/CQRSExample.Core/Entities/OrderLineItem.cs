namespace CQRSExample.Core.Entities
{
    public class OrderLineItem: BaseModel
    {
        public int OrderLineItemId { get; set; }           
		public string Name { get; set; }        
    }
}
