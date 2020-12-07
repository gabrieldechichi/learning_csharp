using System;

namespace CreditCardApplications
{
    public interface ILicenseData
    {
        const string LICENSEKEY_EXPIRED = "EXPIRED";
        string LicenseKey { get; }
    }

    public interface IServiceInformation
    {
        ILicenseData LicenseData { get; }
    }

    public interface IFrequentFlyerNumberValidator
    {
        bool IsValid(string frequentFlyerNumber);
        void IsValid(string frequentFlyerNumber, out bool isValid);

        bool IsValid(ref string frequentFlyerNumber);

        IServiceInformation ServiceInformation { get; }

        ValidationMode ValidationMode { get; set; }

        event EventHandler ValidatorLookupPerformed;
    }
}