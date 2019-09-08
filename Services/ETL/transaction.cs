using System;
using System.Collections.Generic;
using System.Text;
using Core.Domain.Common;


namespace Services.ETL
{
    public class transaction
    {
        public string ShopCity { get; set; }
        public string ShopName { get; set; }
        public string Product { get; set; }
        public string ShopPostCode { get; set; }
        public int Quantity { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public double Price { get; set; }
        public EShopType ShopType { get; set; }
        public EPaymentType PaymentType { get; set; }
        public int MessagesLeft { get; set; }
        public string ClientPostCode { get; set; }
        public string ClientCity { get; set; }

    }
}
