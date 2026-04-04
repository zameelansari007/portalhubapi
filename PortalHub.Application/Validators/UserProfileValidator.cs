using FluentValidation;
using PortalHub.Application.Common;
using PortalHub.Application.DTOs.Portal;
using PortalHub.Application.Mappings;
using PortalHub.Domain.Models.Portal;

namespace PortalHub.Application.Validators
{
    public class CreateUserDtoValidator : BaseEntityValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            

            // First Name
            RequireNotEmpty(x => x.FirstName, "First Name");
            RuleFor(x => x.FirstName)
                .MaximumLength(100)
                .WithMessage("First Name must not exceed 100 characters");

            // Last Name
            RequireNotEmpty(x => x.LastName, "Last Name");
            RuleFor(x => x.LastName)
                .MaximumLength(100)
                .WithMessage("Last Name must not exceed 100 characters");

            // Email
            RequireNotEmpty(x => x.Email, "Email");
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Invalid email format")
                .MaximumLength(200);

            // Phone
            RuleFor(x => x.Phone)
                .Matches(@"^[6-9][0-9]{9}$")
                .When(x => !string.IsNullOrEmpty(x.Phone))
                .WithMessage("Phone must be a valid 10 digit mobile number");

            //// Role
            //RequireMin(x => x.RoleId, 1, "RoleId");

            //// CreatedAt
            //RuleFor(x => x.CreatedAt)
            //    .LessThanOrEqualTo(DateTime.UtcNow)
            //    .WithMessage("CreatedAt cannot be in the future");

            // Email verification logic
            //RuleFor(x => x.EmailVerifiedAt)
            //    .NotNull()
            //    .When(x => x.IsEmailVerified)
            //    .WithMessage("EmailVerifiedAt must be set when email is verified");

            // Phone verification logic
            //RuleFor(x => x.PhoneVerifiedAt)
            //    .NotNull()
            //    .When(x => x.IsPhoneVerified)
            //    .WithMessage("PhoneVerifiedAt must be set when phone is verified");
        }
    }


    public class UpdateUserDtoValidator : BaseEntityValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {


            // First Name
            RequireNotEmpty(x => x.FirstName, "First Name");
            RuleFor(x => x.FirstName)
                .MaximumLength(100)
                .WithMessage("First Name must not exceed 100 characters");

            // Last Name
            RequireNotEmpty(x => x.LastName, "Last Name");
            RuleFor(x => x.LastName)
                .MaximumLength(100)
                .WithMessage("Last Name must not exceed 100 characters");

            // Email
            //RequireNotEmpty(x => x.Email, "Email");
            //RuleFor(x => x.Email)
            //    .EmailAddress()
            //    .WithMessage("Invalid email format")
            //    .MaximumLength(200);

            // Phone
            RuleFor(x => x.Phone)
                .Matches(@"^[6-9][0-9]{9}$")
                .When(x => !string.IsNullOrEmpty(x.Phone))
                .WithMessage("Phone must be a valid 10 digit mobile number");

            //// Role
            //RequireMin(x => x.RoleId, 1, "RoleId");

            //// CreatedAt
            //RuleFor(x => x.CreatedAt)
            //    .LessThanOrEqualTo(DateTime.UtcNow)
            //    .WithMessage("CreatedAt cannot be in the future");

            // Email verification logic
            //RuleFor(x => x.EmailVerifiedAt)
            //    .NotNull()
            //    .When(x => x.IsEmailVerified)
            //    .WithMessage("EmailVerifiedAt must be set when email is verified");

            // Phone verification logic
            //RuleFor(x => x.PhoneVerifiedAt)
            //    .NotNull()
            //    .When(x => x.IsPhoneVerified)
            //    .WithMessage("PhoneVerifiedAt must be set when phone is verified");
        }
    }

    public class VerifyEmailDtoValidator : AbstractValidator<VerifyEmailDto>
    {
        public VerifyEmailDtoValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("UserId must be valid");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Email must be valid");

            RuleFor(x => x.OtpCode)
                .NotEmpty().WithMessage("Verification token is required")
                .MaximumLength(200)
                .WithMessage("Invalid verification token");
        }
    }

    public class VerifyMobileDtoValidator : AbstractValidator<VerifyMobileDto>
    {
        public VerifyMobileDtoValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("UserId must be valid");

            RuleFor(x => x.Mobile)
                .NotEmpty().WithMessage("Mobile number is required")
                .Matches(@"^[6-9]\d{9}$")
                .WithMessage("Mobile must be a valid 10 digit number");

            RuleFor(x => x.OtpCode)
                .NotEmpty().WithMessage("OTP is required")
                .Length(4, 6)
                .WithMessage("OTP must be between 4 and 6 digits")
                .Matches(@"^\d+$")
                .WithMessage("OTP must contain only numbers");
        }
    }


}
