﻿namespace BTCodeCraftersChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            long userId = 42;
            DateTime dateTime = DateTime.Now;
            const int ValidityPeriodSeconds = 30;
            TOTPSystem totpGenerator = new TOTPSystem();

            string otp = totpGenerator.GenerateTOTP(userId, dateTime);
            bool isTOTPValid = totpGenerator.ValidateOTP(otp);
            Console.WriteLine($"TOTP validity: {isTOTPValid}");
            Console.WriteLine("TOTP will expire soon...");
            Thread.Sleep(1000 * ValidityPeriodSeconds);
            Console.WriteLine($"TOTP validity: {totpGenerator.ValidateOTP(otp)}");
        }

    }
}

