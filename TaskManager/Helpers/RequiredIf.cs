using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManager.Helpers
{
    public class RequiredIfAttribute : ValidationAttribute, IClientValidatable
    {
        private RequiredAttribute innerAttribute = new RequiredAttribute();
        public string DependentProperty { get; set; }
        public object TargetValue { get; set; }

        public RequiredIfAttribute(string dependentProperty, object targetValue)
        {
            this.DependentProperty = dependentProperty;
            this.TargetValue = targetValue;
        }

        public override bool IsValid(object value)
        {
            return innerAttribute.IsValid(value);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule modelClientValidationRule = new ModelClientValidationRule()
            {
                ErrorMessage = FormatErrorMessage(metadata.DisplayName),
                ValidationType = "requiredifattribute"
            };
            modelClientValidationRule.ValidationParameters.Add("requiredifattribute", DependentProperty);
            yield return modelClientValidationRule;
        }
    }

    public class RequiredIfValidator : DataAnnotationsModelValidator<RequiredIfAttribute>
    {
        public RequiredIfValidator(ModelMetadata metadata, ControllerContext context, RequiredIfAttribute attribute)
            : base(metadata, context, attribute)
        {
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return base.GetClientValidationRules();
        }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            var field = Metadata.ContainerType.GetProperty(Attribute.DependentProperty);
            if (field != null)
            {
                var value = field.GetValue(container, null);
                if ((value == null && Attribute.TargetValue == null) ||
                    (value.Equals(Attribute.TargetValue)))
                {
                    if (!Attribute.IsValid(Metadata.Model))
                        yield return new ModelValidationResult { Message = ErrorMessage };
                }
            }
        }
    }
}