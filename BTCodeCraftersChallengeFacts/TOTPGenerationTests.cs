using Xunit;
using BTCodeCraftersChallenge;
using System;

namespace BTCodeCraftersChallengeFacts
{
    public class TOTPGenerationTests
    {
        
        [Fact]
        public void ShouldGenerateAValidTOTP()
        {
            TOTPSystem totpSystem = new TOTPSystem();
            int userID = 123;
            DateTime dateTime = DateTime.Now;
            string totp =  totpSystem.GenerateTOTP(userID, dateTime);
            Assert.NotEmpty(totp);
        }
    }
}