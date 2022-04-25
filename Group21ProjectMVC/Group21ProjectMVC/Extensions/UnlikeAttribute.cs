using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace System.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UnlikeAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "{0} cannot be the same as {1}.";

        public string OtherProperty { get; private set; }
        public string OtherPropertyName { get; private set; }

        public UnlikeAttribute(
            string otherProperty,
            string otherPropertyName)
            : base(DefaultErrorMessage)
        {
            OtherProperty = otherProperty;
            OtherPropertyName = otherPropertyName;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var otherProperty = validationContext.ObjectInstance.GetType().GetProperty(OtherProperty);

                var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance, null);

                if (value.Equals(otherPropertyValue))
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return ValidationResult.Success;

        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, OtherPropertyName);
        }
    }
}

