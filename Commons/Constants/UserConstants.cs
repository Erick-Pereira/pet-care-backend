namespace Commons.Constants
{
    public static class UserConstants
    {
        public const int CpfLength = 11;

        public const string DigitRegex = @"\d";

        public const int EmailMaxLength = 100;

        public const int EmailMinLength = 5;

        public const string EmailUniqueIndexName = "UQ_EMAIL";

        public const int FullNameMaxLength = 100;

        public const int FullNameMinLength = 3;

        public const string LowercaseRegex = "[a-z]";

        public const string NumbersOnlyRegex = "^[0-9]{11}$";

        public const int PasswordMaxLength = 100;

        public const int PasswordMinLength = 8;

        public const int PhoneNumberMaxLength = 15;

        public const string SymbolRegex = @"[\W_]";

        public const string TableName = "user";

        public const string UppercaseRegex = "[A-Z]";
    }
}