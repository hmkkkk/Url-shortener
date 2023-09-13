namespace API.Helpers
{
    public static class LinkHelpers
    {
        private static Random random = new Random();
        private static readonly int _shortLength = 8;
        public static string GenerateShortenedUrl()
        {
            const string chars = "abcdefhijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            
            return new string(Enumerable.Repeat(chars, _shortLength)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}