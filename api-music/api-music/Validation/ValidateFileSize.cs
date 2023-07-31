using System.ComponentModel.DataAnnotations;

namespace api_music.Validation
{
    public class ValidateFileSize : ValidationAttribute
    {
        public readonly int sizeMaxInMegaBytes;
        public ValidateFileSize( int SizeMaxInMegaBytes )
        {
            sizeMaxInMegaBytes = SizeMaxInMegaBytes;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ( value == null ) { return ValidationResult.Success; }
            IFormFile formFile = value as IFormFile;
            if ( formFile == null ) { return ValidationResult.Success; }

            if(formFile.Length > sizeMaxInMegaBytes*1024*1024)
            {
                return new ValidationResult($"El peso del archivo, no debe ser mayor a {sizeMaxInMegaBytes} mb");
            }
            return ValidationResult.Success;
        }

    }
}
