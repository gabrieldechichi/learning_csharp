using CreditCardApplications;
using Moq;
using System;
using Xunit;

namespace CreditCardApplications.Test
{
    public class CreditCardApplicationEvaluatorShould
    {
        CreditCardApplicationEvaluator sut;
        Mock<IFrequentFlyerNumberValidator> mockFrequentFlyerNumberValidator;

        public CreditCardApplicationEvaluatorShould()
        {
            mockFrequentFlyerNumberValidator = new Mock<IFrequentFlyerNumberValidator>();
            sut = new CreditCardApplicationEvaluator(mockFrequentFlyerNumberValidator.Object);
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
            //We need to setup the mock correctly so this test is good
            mockFrequentFlyerNumberValidator
                .Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);

            var application = new CreditCardApplication() { Age = 19 };
            var result = sut.Evaluate(application);

            Assert.True(result == CreditCardApplicationDecision.ReferredToHuman);
        }

        [Fact]
        public void DeclineLowIncomeApplication()
        {
            //mockFrequentFlyerNumberValidator
            //    .Setup(x => x.IsValid("x"))
            //    .Returns(true);
            //mockFrequentFlyerNumberValidator
            //    .Setup(x => x.IsValid(It.IsAny<string>()))
            //    .Returns(true);
            //mockFrequentFlyerNumberValidator
            //    .Setup(x => x.IsValid(It.Is<string>(n => n.Length > 0)))
            //    .Returns(true);
            //mockFrequentFlyerNumberValidator
            //    .Setup(x => x.IsValid(It.IsRegex("[a-z]")))
            //    .Returns(true);
            mockFrequentFlyerNumberValidator
                .Setup(x => x.IsValid(It.IsIn(new string[] { "x", "a" })))
                .Returns(true);

            var application = new CreditCardApplication()
            {
                GrossAnnualIncome = 19_999,
                Age = 42,
                FrequentFlyerNumber = "x"
            };

            var result = sut.Evaluate(application);
            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, result);
        }

        [Fact]
        void DeclineLowIncomeApplicationOutDemo()
        {
            var isValid = true;
            mockFrequentFlyerNumberValidator
                .Setup(x => x.IsValid(It.IsAny<string>(), out isValid));

            var application = new CreditCardApplication()
            {
                GrossAnnualIncome = 19_999,
                Age = 42,
            };

            var result = sut.EvaluateUsingOut(application);

            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, result);
        }

        [Fact]
        void DeclineLowIncomeApplicationRefDemo()
        {
            mockFrequentFlyerNumberValidator
                .Setup(x => x.IsValid(ref It.Ref<string>.IsAny)).Returns(true);

            var application = new CreditCardApplication()
            {
                GrossAnnualIncome = 19_999,
                Age = 42,
                FrequentFlyerNumber = "x",
            };

            var result = sut.EvaluateUsingRef(application);

            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, result);
        }
    }
}
