using System.ComponentModel.DataAnnotations;

namespace Task1QQQ.Attributes
{
    public sealed class NotNullObj : ValidationAttribute
    {
        public NotNullObj()
        {

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            object
                instance = validationContext.ObjectInstance;

            if (value != null)
            {
                return ValidationResult.Success;
            }

            return new("");
        }
    }
}
