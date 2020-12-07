using CreditCardApplications.Test.Core;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CreditCardApplications.Test.BehaviorTests
{
    public class CreditCardApplicationEvaluatorShould
    {
        CreditCardApplicationEvaluator sut;
        readonly Mock<IFrequentFlyerNumberValidator> mockFrequentFlyerNumberValidator;

        public CreditCardApplicationEvaluatorShould()
        {
            //default is Loose
            mockFrequentFlyerNumberValidator = new Mock<IFrequentFlyerNumberValidator>();

            //default values to Mock (default is null)
            mockFrequentFlyerNumberValidator.DefaultValue = DefaultValue.Mock;

            //let's remember all properties
            mockFrequentFlyerNumberValidator.SetupAllProperties();

            sut = new CreditCardApplicationEvaluator(mockFrequentFlyerNumberValidator.Object);
        }

        [Fact]
        void ValidateFrequentFlyerNumberForLowIncomeApplicantions()
        {
            var application = CreditCardApplicationBuilder
                .New()
                .WithFrequentFlyerNumber("q")
                .WithLowIncome()
                .WithOldAge()
                .Build();

            sut.Evaluate(application);

            mockFrequentFlyerNumberValidator
                .Verify(x => x.IsValid(application.FrequentFlyerNumber), Times.Once, "Frequent flyer number should be validated");
        }

        [Fact]
        void NotValidateFrequentFlyerNumberForHighIncomeApplications()
        {
            var application = CreditCardApplicationBuilder
                .New()
                .WithFrequentFlyerNumber("q")
                .WithHighIncome()
                .WithOldAge()
                .Build();

            sut.Evaluate(application);

            mockFrequentFlyerNumberValidator.Verify(x => x.IsValid(application.FrequentFlyerNumber), Times.Never);
        }
    }
}
