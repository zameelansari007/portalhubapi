using FluentValidation;
using System.Linq.Expressions;

namespace PortalHub.Application.Common
{
    public abstract class BaseEntityValidator<T> : AbstractValidator<T>
    {
        protected void RequireNotNull<TProp>(
            Expression<Func<T, TProp>> expression,
            string fieldName)
        {
            RuleFor(expression)
                .NotNull()
                .WithMessage($"{fieldName} is required");
        }

        protected void RequireNotEmpty(
            Expression<Func<T, string>> expression,
            string fieldName)
        {
            RuleFor(expression)
                .NotEmpty()
                .WithMessage($"{fieldName} is required");
        }

        protected void RequireMin(
            Expression<Func<T, int>> expression,
            int min,
            string fieldName)
        {
            RuleFor(expression)
                .GreaterThanOrEqualTo(min)
                .WithMessage($"{fieldName} must be at least {min}");
        }

        protected void RequireMax(
            Expression<Func<T, int>> expression,
            int max,
            string fieldName)
        {
            RuleFor(expression)
                .LessThanOrEqualTo(max)
                .WithMessage($"{fieldName} must not exceed {max}");
        }

        protected void RequireNonNegative(
            Expression<Func<T, decimal>> expression,
            string fieldName)
        {
            RuleFor(expression)
                .GreaterThanOrEqualTo(0)
                .WithMessage($"{fieldName} cannot be negative");
        }

        protected void RequireRange(
            Expression<Func<T, decimal>> expression,
            decimal min,
            decimal max,
            string fieldName)
        {
            RuleFor(expression)
                .InclusiveBetween(min, max)
                .WithMessage($"{fieldName} must be between {min} and {max}");
        }

        protected void RequireDateRange(
            Expression<Func<T, DateTime>> from,
            Expression<Func<T, DateTime>> to,
            string fromName,
            string toName)
        {
            RuleFor(x => new { From = from.Compile()(x), To = to.Compile()(x) })
                .Must(x => x.From <= x.To)
                .WithMessage($"{fromName} must be before {toName}");
        }
    }
}
