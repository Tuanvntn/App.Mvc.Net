
using App.Services;
using App.Models;

namespace App.Services
{
    public class ProductService : List<ProductModel>
    {
        public ProductService()
        {
            this.AddRange(new ProductModel[]
            {
                new ProductModel(){Id = 1, Name="IPhone X", Price = 1000},
                new ProductModel(){Id = 2, Name="IPhone 13", Price = 1100},
                new ProductModel(){Id = 3, Name="IPhone 14", Price = 1200},
                new ProductModel(){Id = 4, Name="IPhone 15", Price = 1300},
                new ProductModel(){Id = 5, Name="IPhone 12", Price = 1400}
            });
        }
    }
}

