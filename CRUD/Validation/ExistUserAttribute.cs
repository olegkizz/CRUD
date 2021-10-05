using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace IdentityNLayer.Validation
{
    public class ExistUserAttribute : ValidationAttribute, IClientModelValidator
    {
        public ExistUserAttribute()
        {
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validateContext)
        {
            if (value == null)
                return ValidationResult.Success;
            ManagerModel manager = (ManagerModel)value;
            UserManager<Person> _userManager = (UserManager<Person>)
                validateContext.GetService(typeof(UserManager<Person>));
            return null;
        }
    }
}