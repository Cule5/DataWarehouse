using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Common;

namespace Core.Domain.Transaction.Factories
{
    public class TransactionFactory:ITransactionFactory
    {
        public Task<Transaction> CreateAsync(string city, DateTime date, EPaymentType paymentType, string postCode)
        {
            return Task.Factory.StartNew(() =>
            {
                var transaction=new Transaction(city,date,paymentType,postCode);
                return transaction;
            });
        }
    }
}
