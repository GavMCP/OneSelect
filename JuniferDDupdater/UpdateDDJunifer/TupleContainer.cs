using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateDDJunifer
{
    public class TupleContainer
    {
        public string _accountID { get; set; }
        public double _directDebitAmount { get; set; }
        public string _paymentSchedulePeriodID { get; set; }


        public TupleContainer()
        {

        }


        /// <summary>
        /// A Tuple container to hold the 3 data items needed to change an accounts Direct Debit monthly payment.
        /// </summary>
        /// <param name="accountID">Account ID for this Account</param>
        /// <param name="DDAmount">The new Direct Debit amount.</param>
        /// <param name="paymentSchedulePeriodId">The paymentSchedulePeriodId for this account.</param>
        public TupleContainer(string accountID, double DDAmount, string paymentSchedulePeriodId)
        {
            _accountID = accountID;
            _directDebitAmount = DDAmount;
            _paymentSchedulePeriodID = paymentSchedulePeriodId;
        }


        public void AddData(string accountID, double DDAmount, string paymentSchedulePeriodID)
        {
            _accountID = accountID;
            _directDebitAmount = DDAmount;
            _paymentSchedulePeriodID = paymentSchedulePeriodID;
        }

        /// <summary>
        /// Return the AccountID
        /// </summary>
        /// <returns></returns>
        public string GetAccountID()
        {
            return _accountID;
        }

        /// <summary>
        /// Return the DD amount
        /// </summary>
        /// <returns></returns>
        public double GetDirectDebitAmount()
        {
            return _directDebitAmount;
        }

        /// <summary>
        /// Return the PaymentSchedulePeriodID
        /// </summary>
        /// <returns></returns>
        public string GetPaymentSchedulePeriodID()
        {
            return _paymentSchedulePeriodID;
        }

        /// <summary>
        /// Removes values from TupleContainer
        /// </summary>
        public void Delete()
        {
            _accountID = null;
            _directDebitAmount = 0;
            _paymentSchedulePeriodID = null;
        }
    }
}
