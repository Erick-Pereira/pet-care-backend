namespace Commons.Extensions
{
    public static class StringExtensions
    {
        public static string StringCleaner(this string info)
        {
            info = info.Replace("R", "").Replace("$", "").Replace(",", "").Replace(".", "").Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");

            return info;
        }
    }
}