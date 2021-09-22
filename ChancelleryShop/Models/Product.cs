using System;
using System.Collections.Generic;

#nullable disable

namespace ChancelleryShop
{
    public partial class Product
    {
        public Product()
        {
            ProductToReceipts = new HashSet<ProductToReceipt>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public string ProductImage { get; set; }
        public string ProductDescription { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<ProductToReceipt> ProductToReceipts { get; set; }
    }
}
