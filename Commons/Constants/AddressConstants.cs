namespace Commons.Constants
{
    public static class AddressConstants
    {
        public const int ComplementMaxLength = 60;

        public const int NumberMaxLength = 10;

        public const int StreetMaxLength = 100;

        public const string TableName = "address";

        public const int ZipCodeLength = 8;

        public const string ZipCodeRegex = @"^\d{9}$";
    }
}