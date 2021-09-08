using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityNLayer.Validation
{
    public class BirthDateAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly int minAge;
        private readonly int maxAge;

        public BirthDateAttribute(int minAge, int maxAge = 130)
        {
            this.minAge = minAge;
            this.maxAge = maxAge;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-birthdate", "Must be older than 18");
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;
            var age = (DateTime.Today - (DateTime)value).TotalDays / 365;

            return age > minAge && age < maxAge;
        }
    }
}