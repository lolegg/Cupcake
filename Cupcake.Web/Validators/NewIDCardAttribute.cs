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
    public class NewIDCardAttribute : ValidationAttribute, IClientValidatable
    {
        public NewIDCardAttribute()
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(string.Format("{0} 格式有误", name));
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (ValidHelper.ValidIDCard(value.ToString()))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }

        public System.Collections.Generic.IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ValidationType = "newidcard",
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName())
            };
            yield return rule;
        }
    }
}