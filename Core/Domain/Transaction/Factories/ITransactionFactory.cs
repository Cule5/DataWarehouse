using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Common;

namespace Core.Domain.Transaction.Factories
{
    public interface ITransactionFactory
    {
        Task<Transaction> CreateAsync(string city, DateTime date, EPaymentType paymentType, string postCode);
    }
}
