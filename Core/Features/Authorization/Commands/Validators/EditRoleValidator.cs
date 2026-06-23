using FluentValidation;
using Microsoft.Extensions.Localization;
using Core.Features.Authorization.Commands.Models;
using Core.Resources;
using Service.Abstracts;

namespace Core.Features.Authorization.Commands.Validators
{
    public class EditRoleValidator : AbstractValidator<EditRoleCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion
        #region Constructors

        #endregion
        public EditRoleValidator(IStringLocalizer<SharedResources> stringLocalizer,
                                  IAuthorizationService authorizationService)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
                 .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.Name)
                 .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);
        }

        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.Name)
                .MustAsync(async (command, name, cancellationToken) => 
                {
                    var role = await _authorizationService.GetRoleById(command.Id);
                    if (role == null) return true;
                    if (role.Name == name) return true;
                    return !await _authorizationService.IsRoleExistByName(name);
                })
                .WithMessage(_stringLocalizer[SharedResourcesKeys.IsExist]);
        }

        #endregion
    }
}
