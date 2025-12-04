using FluentValidation;
using AmateurTheaterMongo.Models;

namespace AmateurTheaterMongo.Validators
{
    public class PlayValidator : AbstractValidator<Play>
    {
        public PlayValidator()
        {
            RuleFor(x => x.Title).NotEmpty().Length(2, 100);
            RuleFor(x => x.Genre).IsInEnum();
            RuleFor(x => x.TheaterId).NotEmpty().Length(24); // ObjectId - 24 символи
        }
    }
}