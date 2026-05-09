using FluentValidation;
using PortalHub.Application.DTOs.Auth;

public class ForgotPasswordValidator
    : AbstractValidator<ForgotPasswordDto>
{
    public ForgotPasswordValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}