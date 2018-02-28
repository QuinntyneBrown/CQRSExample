namespace CQRSExample.Core.Entities
{
    public class Product: BaseModel
    {
        public int ProductId { get; set; }           
		public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
    }
}
