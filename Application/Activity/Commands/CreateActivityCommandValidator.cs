using Application.Activity.DTO;
using FluentValidation;

namespace Application.Activity.Commands
{
    public class CreateActivityCommandValidator : AbstractValidator<CreateActivityRequest>
    {
        public CreateActivityCommandValidator()
        {

            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Category).NotEmpty();
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.Venue).NotEmpty();
        }
    }
}
