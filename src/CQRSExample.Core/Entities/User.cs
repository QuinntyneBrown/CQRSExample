namespace CQRSExample.Core.Entities
{
    public class User: BaseModel
    {
        public int UserId { get; set; }           
		public string Name { get; set; }        
    }
}
