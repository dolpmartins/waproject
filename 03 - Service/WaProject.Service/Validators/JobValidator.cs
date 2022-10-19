using FluentValidation;
using WaProject.Domain.Entities;

namespace WaProject.Service.Validators
{
    public class JobValidator : AbstractValidator<Job>
    {
        public JobValidator()
        {
            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("Por favor digite uma descrição")
                .NotNull().WithMessage("Por favor digite uma descrição");
        }
    }
}
