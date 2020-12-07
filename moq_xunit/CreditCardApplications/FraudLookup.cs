using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCardApplications
{
    public class FraudLookup
    {
        public bool IsFraudRisk(CreditCardApplication application)
        {
            return CheckApplicationForFraud(application);
        }

        protected virtual bool CheckApplicationForFraud(CreditCardApplication application)
        {
            return application.LastName == "Smith";
        }
    }
}
