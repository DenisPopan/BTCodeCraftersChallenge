using Xunit;
using BTCodeCraftersChallenge;
using System;

namespace BTCodeCraftersChallengeFacts
{
    public class TOTPGenerationTests
    {
        [Fact]
        public void ShouldReturnFalseIfOTPWasNotGenerated()
        {
            TOTPSystem totpSystem = new TOTPSystem();
            Assert.False(totpSystem.ValidateOTP("235663"));
        }

        [Fact]
        public void ShouldReturnFalseIfOTPHasExpired()
        {
            TOTPSystem totpSystem = new TOTPSystem();
            DateTime expiredDateTime = new DateTime();
            string expiredOTPValue = "467221";
            TOTPSystem.OTPData expiredOTP = new TOTPSystem.OTPData(expiredOTPValue, expiredDateTime);
            totpSystem.temporaryStoredOTPS.Add(expiredOTP);
            Assert.False(totpSystem.ValidateOTP(expiredOTPValue));
        }

        [Fact]
        public void ShouldReturnTrueIfOTPHasNotExpiredYet()
        {
            TOTPSystem totpSystem = new TOTPSystem();
            DateTime currentDateTime = DateTime.Now;
            string OTPValue = "467221";
            TOTPSystem.OTPData OTP = new TOTPSystem.OTPData(OTPValue, currentDateTime);
            totpSystem.temporaryStoredOTPS.Add(OTP);
            Assert.True(totpSystem.ValidateOTP(OTPValue));
        }

        [Fact]
        public void ShouldGenerateAValidTOTP()
        {
            TOTPSystem totpSystem = new TOTPSystem();
            int userID = 123;
            int defaultOTPLength = 6;
            DateTime currentDateTime = DateTime.Now;
            string totp =  totpSystem.GenerateTOTP(userID, currentDateTime);
            Assert.NotNull(totp);
            Assert.Equal(totp.Length, defaultOTPLength);
            Assert.True(totpSystem.ValidateOTP(totp));
        }

        [Fact]
        public void ShoulValidateAnExpiredOTP()
        {

            TOTPSystem totpSystem = new TOTPSystem();
            int userID = 123;
            DateTime currentDateTime = DateTime.Now;
            string totp = totpSystem.GenerateTOTP(userID, currentDateTime);
            System.Threading.Thread.Sleep(31000);
            Assert.False(totpSystem.ValidateOTP(totp));
        }
    }
}