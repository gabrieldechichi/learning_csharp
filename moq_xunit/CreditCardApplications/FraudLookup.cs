using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCardApplications
{
    public class FraudLookup
    {
        public virtual bool IsFraudRisk(CreditCardApplication application)
        {
            return application.LastName == "Smith";
        }
    }
}
