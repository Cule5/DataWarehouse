using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Core.Domain.Common;

namespace Core.Domain.Transaction
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public DateTime Date { get; set; }
        public EPaymentType PaymentType { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public Shop.Shop Shop { get; set; }
        public ICollection<TransactionProduct.TransactionProduct> TransactionProducts { get; set; }=new List<TransactionProduct.TransactionProduct>();

    }
}
