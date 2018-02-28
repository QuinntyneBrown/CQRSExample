namespace CQRSExample.Core.Entities
{
    public class Customer: BaseModel
    {
        public int CustomerId { get; set; }        
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
    }
}
