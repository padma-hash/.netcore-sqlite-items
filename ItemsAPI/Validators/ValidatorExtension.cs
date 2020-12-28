using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using ItemsAPI.Model;

namespace ItemsAPI.Validators
{
    public static class ValidatorExtension
    {

        public static bool IsValid(this Items item, out IEnumerable<string> errors)
        {
            var validator = new ItemValidator();

            var validationResult = validator.Validate(item);

            errors = AggregateErrors(validationResult);

            return validationResult.IsValid;
        }

        private static List<string> AggregateErrors(ValidationResult validationResult)
        {
            var errors = new List<string>();

            if (!validationResult.IsValid)
                foreach (var error in validationResult.Errors)
                    errors.Add(error.ErrorMessage);

            return errors;
        }
    }
}
