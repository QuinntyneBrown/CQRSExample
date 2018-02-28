using CQRSExample.Core.Entities;

namespace CQRSExample.Features.Products
{
    public class ProductApiModel
    {        
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public static ProductApiModel FromProduct(Product product)
            => new ProductApiModel()
            {
                Name = product.Name,
                Description = product.Description
            };
    }
}
