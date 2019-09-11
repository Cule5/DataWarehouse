using System;
using System.Collections.Generic;
using System.Text;
using Core.Domain.Common;

namespace Core.Domain.Shop
{
    public class Shop
    {
        public Shop()
        {
            Transactions = new List<Transaction.Transaction>();
        }
        public Shop(string name,EShopType shopType,string postCode,string city)
        {
            Name = name;
            Type = shopType;
            PostCode = postCode;
            City = city;
        }
        public int ShopId { get; set; }
        public string Name { get; set; }
        public EShopType Type { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public ICollection<Transaction.Transaction> Transactions { get; set; }
        public override bool Equals(object obj)
        {
            if (!(obj is Shop shop))
                return false;
            return shop.Name.Equals(Name) && shop.Type==Type && shop.PostCode.Equals(PostCode) && shop.City.Equals(City);
        }
    }
}
