using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Task1QQQ.Attributes
{
    public class DecimalPrecisionAttribute : ValidationAttribute
    {
        private readonly int _precision;
        private readonly int _scale;

        public DecimalPrecisionAttribute(int precision, int scale)
        {
            _precision = precision;
            _scale = scale;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is decimal decimalValue)
            {
                var stringValue = decimalValue.ToString(CultureInfo.InvariantCulture);

                var parts = stringValue.Split('.');

                if (parts[0].Length + (parts.Length > 1 ? parts[1].Length : 0) > _precision)
                {
                    return new ValidationResult($"Total number of digits cannot exceed {_precision}.");
                }

                if (parts.Length > 1 && parts[1].Length > _scale)
                {
                    return new ValidationResult($"Scale cannot exceed {_scale} digits.");
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid decimal value.");
        }
    }
}
