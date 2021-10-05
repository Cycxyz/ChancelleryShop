using System;
using System.Collections.Generic;

#nullable disable

namespace ChancelleryShop
{
    public partial class Receipt
    {
        public Receipt()
        {
            ProductToReceipts = new HashSet<ProductToReceipt>();
        }

        public int ReceiptId { get; set; }
        public DateTime ReceiptDate { get; set; }

        public virtual ICollection<ProductToReceipt> ProductToReceipts { get; set; }
    }
}
