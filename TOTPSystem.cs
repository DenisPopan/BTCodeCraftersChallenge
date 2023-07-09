
namespace BTCodeCraftersChallenge
{
    public class TOTPSystem
    {
        private const int ValidityPeriodSeconds = 30;
        public struct OTPData
        {
            public OTPData(string otp, DateTime expiryDateTime)
            {
                Otp = otp;
                ExpiryDateTime = expiryDateTime;
            }

            internal string Otp { get; init; }
            internal DateTime ExpiryDateTime { get; init; }
        }

        public List<OTPData> temporaryStoredOTPS = new List<OTPData>();

        private void RemoveExpiredOTPS()
        {
            for (int i = 0; i < temporaryStoredOTPS.Count; i++)
            {
                if (IsOTPExpired(temporaryStoredOTPS[i]))
                {
                    temporaryStoredOTPS.RemoveAt(i);
                    i--;
                }
            }
        }

        public string GenerateTOTP(long userId, DateTime dateTime)
        {
            string combinedData = $"{userId.ToString()}{dateTime.ToString("yyyyMMddHHmmss")}";
            Console.WriteLine(combinedData);
            int hashCode = combinedData.GetHashCode();
            if (hashCode < 0) hashCode *= -1;

            DateTime currentDateTime = DateTime.Now;

            DateTime expiryTime = currentDateTime.AddSeconds(ValidityPeriodSeconds);

            string otp = (hashCode % 1000000).ToString("D6");
            temporaryStoredOTPS.Add(new OTPData(otp, expiryTime));

            Console.WriteLine($"Generated TOTP: {otp}");
            Console.WriteLine($"Validity period: {ValidityPeriodSeconds} seconds");
            Console.WriteLine($"Expiry time: {expiryTime}");

            return otp;
        }

        private bool IsOTPExpired(OTPData otp)
        {
            DateTime currentTime = DateTime.Now;
            return ((currentTime - otp.ExpiryDateTime).TotalSeconds > ValidityPeriodSeconds);
        }

        public bool ValidateOTP(string otp)
        {
            OTPData issuedOTP = temporaryStoredOTPS.Find(x => x.Otp.Equals(otp));
            if (issuedOTP.Otp == "") return false;

            if (IsOTPExpired(issuedOTP))
            {
                temporaryStoredOTPS.Remove(issuedOTP);
                return false;
            }

            return true;
        }
    }
}
