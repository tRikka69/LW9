using FluentValidation;
using AmateurTheaterMongo.Models;

namespace AmateurTheaterMongo.Validators
{
    public class ActorValidator : AbstractValidator<Actor>
    {
        public ActorValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().Must(n => n.Contains(" "));
            RuleFor(x => x.Age).InclusiveBetween(5, 120);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}