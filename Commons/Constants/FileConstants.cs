namespace Commons.Constants
{
    public static class FileConstants
    {
        public const int MaxProfilePhotoSize = 50 * 1024 * 1024; // 50MB
        public const int MaxMedicalFileSize = 100 * 1024 * 1024; // 100MB
        public const string InvalidFileSize = "File size exceeds the maximum limit";
        public const string InvalidFileType = "Invalid file type";
        public const string AllowedImageExtensions = ".jpg,.jpeg,.png";
        public const string AllowedDocumentExtensions = ".pdf,.doc,.docx";
    }
}