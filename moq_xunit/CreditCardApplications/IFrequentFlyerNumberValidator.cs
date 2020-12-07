using System;

namespace CreditCardApplications
{
    public interface IFrequentFlyerNumberValidator
    {
        const string LICENSEKEY_EXPIRED = "EXPIRED";

        bool IsValid(string frequentFlyerNumber);
        void IsValid(string frequentFlyerNumber, out bool isValid);

        bool IsValid(ref string frequentFlyerNumber);

        string LicenseKey { get; }
    }
}