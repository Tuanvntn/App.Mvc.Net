

namespace App.Models
{
    public class ProductModel
    {
        public int Id { set; get; }
        public required string Name { set; get; } // required them de ko bao loi null
        public double Price { set; get; }

    }
}
