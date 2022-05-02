using Group21ProjectMVC.Models.CheckoutViewModels;
using System.Collections;

namespace System.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class EnsureMinimumElementsAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "You must enter all fields for {0}";
        public string seatsRequired { get; private set; }
        public string checkProperty { get; private set; }

        public EnsureMinimumElementsAttribute(string otherProperty) : base(DefaultErrorMessage)
        {
            seatsRequired = otherProperty;
        }

        public EnsureMinimumElementsAttribute(string otherProperty, string checkif) : base(DefaultErrorMessage)
        {
            seatsRequired = otherProperty;
            checkProperty = checkif;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if(checkProperty != null)
            {
                var check = validationContext.ObjectInstance.GetType().GetProperty(checkProperty).GetValue(validationContext.ObjectInstance, null);
                if(check != null && !(bool)check)
                {
                    return ValidationResult.Success;
                }
            }

            if (value != null)
            {
                var otherProperty = validationContext.ObjectInstance.GetType().GetProperty(seatsRequired);

                var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance, null);

                if (value is not IList list || otherPropertyValue == null)
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                if (list.Count == (int)otherPropertyValue)
                {
                    foreach (var item in list)
                    {
                        if (checkProperty != null)
                        {
                            if((int)item == 0) return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                        }
                        if (item == null) return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                    }
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name);
        }
    }
}
