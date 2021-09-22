using System;
using System.Collections.Generic;

#nullable disable

namespace ChancelleryShop
{
    public partial class ProductToReceipt
    {
        public int ProductToReceiptId { get; set; }
        public int ReceiptId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }

        public virtual Product Product { get; set; }
        public virtual Receipt Receipt { get; set; }
    }
}
