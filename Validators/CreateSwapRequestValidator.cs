using FluentValidation;
using SeniorWepApiProject.Contracts.V1.Requests;

namespace SeniorWepApiProject.Validators
{
    public class CreateSwapRequestValidator : AbstractValidator<CreateSwapRequest>
    {
        public CreateSwapRequestValidator()
        {
            RuleFor(x => x.SenderUser)
                .NotEmpty();
            RuleFor(x => x.Address)
                .NotEmpty();
            RuleFor(x => x.RecieverUser)
                .NotEmpty();
            RuleFor(x => x.SwapDate)
                .NotEmpty();
        }
    }
}