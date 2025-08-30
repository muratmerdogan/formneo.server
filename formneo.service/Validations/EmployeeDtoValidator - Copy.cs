using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.DTOs;
using vesa.core.DTOs.Budget.JobCodeRequest;

namespace vesa.service.Validations
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
