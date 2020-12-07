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
        readonly CreditCardApplicationEvaluator sut;
        readonly Mock<IFrequentFlyerNumberValidator> mockFrequentFlyerNumberValidator;
        readonly Mock<FraudLookup> mockFraudLookup;

        public CreditCardApplicationEvaluatorShould()
        {
            //default is Loose
            mockFrequentFlyerNumberValidator = new Mock<IFrequentFlyerNumberValidator>();

            //default values to Mock (default is null)
            mockFrequentFlyerNumberValidator.DefaultValue = DefaultValue.Mock;

            //let's remember all properties
            mockFrequentFlyerNumberValidator.SetupAllProperties();

            mockFraudLookup = new Mock<FraudLookup>();

            sut = new CreditCardApplicationEvaluator(mockFrequentFlyerNumberValidator.Object, mockFraudLookup.Object);
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
        void AlwaysValidateFrequentFlyerNumberInApplication()
        {
            var application1 = CreditCardApplicationBuilder.New().WithLowIncome().WithFrequentFlyerNumber("1").Build();
            var application2 = CreditCardApplicationBuilder.New().WithLowIncome().WithFrequentFlyerNumber("2").Build();
            var application3 = CreditCardApplicationBuilder.New().WithLowIncome().WithFrequentFlyerNumber("3").Build();

            var validateFrequentFlyerNumberPassedParams = new List<string>(3);
            mockFrequentFlyerNumberValidator
                .Setup(x => x.IsValid(Capture.In(validateFrequentFlyerNumberPassedParams)));

            sut.Evaluate(application1);
            sut.Evaluate(application2);
            sut.Evaluate(application3);

            mockFrequentFlyerNumberValidator.Verify(x => x.IsValid(It.IsAny<string>()), Times.Exactly(3));

            var expectedValidateFrequentFlyerNumberPassedParams = new List<string>()
            {
                application1.FrequentFlyerNumber,
                application2.FrequentFlyerNumber,
                application3.FrequentFlyerNumber
            };

            Assert.Equal(expectedValidateFrequentFlyerNumberPassedParams, validateFrequentFlyerNumberPassedParams);
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

        [Fact]
        void CheckLicenseKeyForLowIncomeApplicants()
        {
            var application = CreditCardApplicationBuilder.New().WithLowIncome().Build();

            sut.Evaluate(application);

            mockFrequentFlyerNumberValidator.VerifyGet(x => x.ServiceInformation.LicenseData.LicenseKey, Times.Once);
        }

        [Fact]
        void SetDetailedValidationModeForOlderApplicants()
        {
            var application = CreditCardApplicationBuilder.New().WithOldAge().Build();
            sut.Evaluate(application);
            mockFrequentFlyerNumberValidator.VerifySet(x => x.ValidationMode = ValidationMode.Detailed, Times.Once);
        }

        [Fact]
        void SetQuickValidationModeForYoungApplicants()
        {
            var application = CreditCardApplicationBuilder.New().WithYoungAge().Build();
            sut.Evaluate(application);
            mockFrequentFlyerNumberValidator.VerifySet(x => x.ValidationMode = ValidationMode.Quick, Times.Once);
        }
    }
}
