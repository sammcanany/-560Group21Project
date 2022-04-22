using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Web.Mvc;
namespace System.ComponentModel.DataAnnotations
{
    public class RequiredIfAttribute : ValidationAttribute, IClientValidatable
    {
        private const string DefaultErrorMessage = "{0} is required";

        public string OtherProperty { get; private set; }
        public string EqualTo { get; private set; }

        public RequiredIfAttribute(
            string otherProperty,
            string equalto)
            : base(DefaultErrorMessage)
        {
            OtherProperty = otherProperty;
            EqualTo = equalto;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var otherProperty = validationContext.ObjectInstance.GetType().GetProperty(OtherProperty);

            var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance, null);

            if (value == null && otherPropertyValue.Equals(EqualTo))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;

        }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule()
            {
                ValidationType = "requiredif",
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
            };

            rule.ValidationParameters.Add("otherproperty", OtherProperty);

            yield return rule;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name);
        }
    }
}
