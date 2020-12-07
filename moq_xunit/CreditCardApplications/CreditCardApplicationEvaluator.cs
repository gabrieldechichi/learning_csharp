using System;

namespace CreditCardApplications
{
    public class CreditCardApplicationEvaluator
    {
        private readonly IFrequentFlyerNumberValidator validator;
        private const int AutoReferralMaxAge = 20;
        private const int HighIncomeThreshold = 100_000;
        private const int LowIncomeThreshold = 20_000;

        public int ValidatorLookupCount { get; private set; }
     
        public CreditCardApplicationEvaluator(IFrequentFlyerNumberValidator validator)
        {
            this.validator = validator ?? throw new System.ArgumentNullException($"Argument can't be null {nameof(validator)}");
            validator.ValidatorLookupPerformed += OnValidatorLookup;
            ValidatorLookupCount = 0;
        }

        private void OnValidatorLookup(object sencer, EventArgs eventArgs)
        {
            ValidatorLookupCount++;
        }

        public CreditCardApplicationDecision Evaluate(CreditCardApplication application)
        {
            return EvaluateInternal(application, s => validator.IsValid(s));
        }

        public CreditCardApplicationDecision EvaluateUsingOut(CreditCardApplication application)
        {
            return EvaluateInternal(application, s => 
            {
                var result = false;
                validator.IsValid(s, out result);
                return result;
            });
        }

        public CreditCardApplicationDecision EvaluateUsingRef(CreditCardApplication application)
        {
            return EvaluateInternal(application, s => validator.IsValid(ref s));
        }

        private CreditCardApplicationDecision EvaluateInternal(CreditCardApplication application, Func<string, bool> evaluateFrequentFlyerNumber)
        {
            if (application.GrossAnnualIncome >= HighIncomeThreshold)
            {
                return CreditCardApplicationDecision.AutoAccepted;
            }

            validator.ValidationMode = application.Age > 30 ? ValidationMode.Detailed : ValidationMode.Quick;

            if (validator.ServiceInformation.LicenseData.LicenseKey == ILicenseData.LICENSEKEY_EXPIRED)
            {
                return CreditCardApplicationDecision.ReferredToHuman;
            }

            try
            {
                var isFrequentFlyer = evaluateFrequentFlyerNumber(application.FrequentFlyerNumber);

                if (!isFrequentFlyer)
                {
                    return CreditCardApplicationDecision.ReferredToHuman;
                }
            }
            catch (Exception)
            {
                //log
                return CreditCardApplicationDecision.ReferredToHuman;
            }

            if (application.Age <= AutoReferralMaxAge)
            {
                return CreditCardApplicationDecision.ReferredToHuman;
            }

            if (application.GrossAnnualIncome < LowIncomeThreshold)
            {
                return CreditCardApplicationDecision.AutoDeclined;
            }

            return CreditCardApplicationDecision.ReferredToHuman;
        }
    }
}
