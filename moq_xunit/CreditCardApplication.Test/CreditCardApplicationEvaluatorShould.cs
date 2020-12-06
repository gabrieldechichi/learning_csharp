using CreditCardApplications;
using System;
using Xunit;

namespace CreditCardApplications.Test
{
    public class CreditCardApplicationEvaluatorShould
    {
        CreditCardApplicationEvaluator sut;
        public CreditCardApplicationEvaluatorShould()
        {
            sut = new CreditCardApplicationEvaluator(null);
        }

        [Fact]
        public void AcceptHighIncomeApplications()
        {
            var application = new CreditCardApplication() { GrossAnnualIncome = 100_000 };
            var result = sut.Evaluate(application);

            Assert.True(result == CreditCardApplicationDecision.AutoAccepted);
        }

        [Fact]
        public void ReferYoungApplication()
        {
            var application = new CreditCardApplication() { Age = 19 };
            var result = sut.Evaluate(application);

            Assert.True(result == CreditCardApplicationDecision.ReferredToHuman);
        }
    }
}
