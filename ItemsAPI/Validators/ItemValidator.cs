using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using ItemsAPI.Model;

namespace ItemsAPI.Validators

{
    public class ItemValidator : AbstractValidator<Items>
    {
        public ItemValidator()
        {
            

            RuleFor(m => m.ItemName).NotNull().Matches("^[a-zA-Z0-9 ]*$").WithMessage("Please specify a valid Name - accepts only alphanumeric values.");

            RuleFor(m => m.Price).NotEmpty().GreaterThan(0).InclusiveBetween(0 , 1000).WithMessage("Please specify a price.");
        }


        protected override bool PreValidate(ValidationContext<Items> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure("", "Please submit a non-null model."));

                return false;
            }
            return true;
        }
    }
}
