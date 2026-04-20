using Core.Features.ChildProfile.Commands;
using FluentValidation;

namespace Core.Features.ChildProfile.Validators
{
    public class AddChildProfileValidator : AbstractValidator<AddChildProfileCommand>
    {
        public AddChildProfileValidator()
        {
            RuleFor(x => x.Dto.Nickname)
                .NotEmpty().WithMessage("Child nickname is required.")
                .MaximumLength(100).WithMessage("Nickname cannot exceed 100 characters.");

            RuleFor(x => x.Dto.AgeInYears)
                .InclusiveBetween(0, 18).WithMessage("Age in years must be between 0 and 18.");

            RuleFor(x => x.Dto.AgeInMonths)
                .InclusiveBetween(0, 11).WithMessage("Age in months must be between 0 and 11.");
        }
    }
}