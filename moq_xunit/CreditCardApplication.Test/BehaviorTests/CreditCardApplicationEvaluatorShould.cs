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
