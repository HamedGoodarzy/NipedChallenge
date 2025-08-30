using FluentValidation;
using FluentValidation.Results;
using NipedWebApi.Data.Model;

namespace NipedWebApi.Domain.Validations
{
    public class ClientValidator : AbstractValidator<Client>, IClientValidator
    {
        public ClientValidator()
        {
            RuleFor(client => client.ClientId)
                .NotEmpty().WithMessage("ClientId is required.")
                .Matches("^[a-zA-Z0-9-]+$").WithMessage("ClientId must be alphanumeric with optional dashes.");
            RuleFor(client => client.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
            RuleFor(client => client.DateOfBirth)
                .LessThan(DateTime.Now).WithMessage("Date of Birth must be in the past.");
        }
        // Explicit interface implementation, reuses base.Validate()
        ValidationResult IClientValidator.Validate(Client client)
        {
            return Validate(client); // Calls base class implementation
        }
    }

    public interface IClientValidator
    {
        ValidationResult Validate(Client client);
    }
}
