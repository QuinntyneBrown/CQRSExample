using CQRSExample.Core.Entities;

namespace CQRSExample.WepAPI.Features.Customers
{
    public class CustomerApiModel
    {        
        public int CustomerId { get; set; }
        public string Firstname { get; set; }

        public static CustomerApiModel FromCustomer(Customer customer)
        {
            var model = new CustomerApiModel();
            model.CustomerId = customer.CustomerId;
            model.Firstname = customer.Firstname;
            return model;
        }
    }
}
