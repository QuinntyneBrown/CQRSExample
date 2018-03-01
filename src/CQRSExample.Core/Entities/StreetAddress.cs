using CQRSExample.Core.Abstractions;

namespace CQRSExample.Core.Entities
{
    public class StreetAddress: ValueObject<StreetAddress>
    {
        public int StreetAddressId { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
    }
}
