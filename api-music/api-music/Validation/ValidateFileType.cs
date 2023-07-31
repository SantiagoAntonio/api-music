using System.ComponentModel.DataAnnotations;

namespace api_music.Validation
{
    public class ValidateFileType : ValidationAttribute
    {
        private readonly string[] validTypes;

        public ValidateFileType(string[] validTypes)
        {
            this.validTypes = validTypes;
        }
        public ValidateFileType(GroupTypeFile groupTypeFile)
        {
            if(groupTypeFile == GroupTypeFile.Image)
            {
                validTypes = new[] { "image/jpeg", "image/png", "image/gif" };
            }

        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) { return ValidationResult.Success; }
            IFormFile formFile = value as IFormFile;
            if (formFile == null) { return ValidationResult.Success; }

            if (!validTypes.Contains(formFile.ContentType))
            {
                return new ValidationResult($"El tipo de archivo debe ser uno de los siguientes: {string.Join(", ", validTypes)}");
            }
            return ValidationResult.Success;

        }
    }
}
