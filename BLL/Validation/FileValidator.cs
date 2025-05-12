using Commons.Constants;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace BLL.Validation
{
    public class FileValidator : AbstractValidator<IFormFile>
    {
        public FileValidator(bool isImage = true, int maxSize = FileConstants.MaxProfilePhotoSize)
        {
            RuleFor(x => x.Length)
                .NotNull()
                .LessThanOrEqualTo(maxSize)
                .WithMessage(FileConstants.InvalidFileSize);

            RuleFor(x => x.FileName)
                .Must(x =>
                {
                    var ext = Path.GetExtension(x).ToLower();
                    return isImage
                        ? FileConstants.AllowedImageExtensions.Contains(ext)
                        : FileConstants.AllowedDocumentExtensions.Contains(ext);
                })
                .WithMessage(FileConstants.InvalidFileType);
        }
    }
}