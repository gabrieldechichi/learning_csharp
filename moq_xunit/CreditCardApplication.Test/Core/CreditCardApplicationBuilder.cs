using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCardApplications.Test.Core
{
    class CreditCardApplicationBuilder
    {
        CreditCardApplication application;

        private CreditCardApplicationBuilder()
        {
            application = new CreditCardApplication();
        }

        public static CreditCardApplicationBuilder New()
        {
            return new CreditCardApplicationBuilder();
        }

        public CreditCardApplicationBuilder WithFraudRisk()
        {
            application.LastName = "Smith";
            return this;
        }

        public CreditCardApplicationBuilder WithLowIncome()
        {
            application.GrossAnnualIncome = 19_999;
            return this;
        }
        public CreditCardApplicationBuilder WithHighIncome()
        {
            application.GrossAnnualIncome = 200_000;
            return this;
        }

        public CreditCardApplicationBuilder WithYoungAge()
        {
            application.Age = 18;
            return this;
        }

        public CreditCardApplicationBuilder WithOldAge()
        {
            application.Age = 42;
            return this;
        }

        public CreditCardApplicationBuilder WithFrequentFlyerNumber(string frequentFlyerNumber)
        {
            application.FrequentFlyerNumber = frequentFlyerNumber;
            return this;
        }

        public CreditCardApplication Build()
        {
            return application;
        }
    }
}
