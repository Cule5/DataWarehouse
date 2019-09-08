using System;
using System.Collections.Generic;
using System.Text;
using Core.Domain.Common;

namespace Core.Domain.Shop
{
    public class Shop
    {
        public int ShopId { get; set; }
        public string Name { get; set; }
        public EShopType Type { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public ICollection<Transaction.Transaction> Transactions { get; set; }=new List<Transaction.Transaction>();
        public override bool Equals(object obj)
        {
            if (!(obj is Shop shop))
                return false;
            return shop.Name.Equals(Name) && shop.Type==Type && shop.PostalCode.Equals(PostalCode) && shop.City.Equals(City);
        }
    }
}
