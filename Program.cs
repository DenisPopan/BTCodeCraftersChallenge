namespace BTCodeCraftersChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            long userId = 42;
            DateTime dateTime = DateTime.Now;
            Console.WriteLine(dateTime);

            TOTPSystem totpGenerator = new TOTPSystem();
            string otp = totpGenerator.GenerateTOTP(userId, dateTime);
            bool isValid = totpGenerator.ValidateOTP(otp);
            Console.WriteLine($"Is TOTP valid? {isValid}");
        }

    }
}

