using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.DTOs;
using formneo.core.DTOs.Budget.JobCodeRequest;

namespace formneo.service.Validations
{


    public class BudgetJobValidator : AbstractValidator<BudgetJobCodeRequestInsertDto>
    {

        public BudgetJobValidator()
        {
            RuleFor(x => x.Grade)
                .NotNull().WithMessage("Ücret Derecesi.")
                .NotEmpty().WithMessage("Ücret Derecesi.")
                .WithName("Ücret Derecesi");

            //RuleFor(x => x.Price).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0");
            //RuleFor(x => x.Stock).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0");

        }



    }
}
