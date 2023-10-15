namespace TB.WebApi.Helper
{
    public static class HashPassword
    {
        public static string Generate(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password , GetSalt(12));
        }

        public static bool Verify(string password , string hash)
        {
           return BCrypt.Net.BCrypt.Verify(password , hash);
        }

        private static string GetSalt(int workFactor)
        {
           return BCrypt.Net.BCrypt.GenerateSalt(workFactor);
        }
    }
}
