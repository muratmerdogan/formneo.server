using FluentValidation;
using formneo.core.DTOs;

namespace formneo.service.Validations
{
    public class WorkFlowContiuneApiDtoValidator : AbstractValidator<WorkFlowContiuneApiDto>
    {
        public WorkFlowContiuneApiDtoValidator()
        {
            // ApproveItem nullable - sadece approverNode için gerekli, formTaskNode için null olabilir
            // Bu yüzden Required kuralı yok - null veya boş string kabul edilir
            RuleFor(x => x.ApproveItem)
                .Must(x => x == null || !string.IsNullOrWhiteSpace(x))
                .WithMessage("ApproveItem must be null or a valid string");
            
            RuleFor(x => x.workFlowItemId)
                .NotEmpty().WithMessage("workFlowItemId is required");
        }
    }
}

