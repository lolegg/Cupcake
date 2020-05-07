using Cupcake.Web.Framework;
using Cupcake.Web.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Cupcake.Web.Validators
{
    public class NewPhoneMobileAttribute : ValidationAttribute, IClientValidatable
    {
        public NewPhoneMobileAttribute()
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(string.Format("{0} 格式有误", name));
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (ValidHelper.ValidMobile(value.ToString()))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }
        
        public System.Collections.Generic.IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ValidationType = "newphonemobile",
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName())
            };
            //rule.ValidationParameters["other"] = OtherProperty;
            yield return rule;
        }
    }
}