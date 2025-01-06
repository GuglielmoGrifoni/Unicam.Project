using FluentValidation;
using Unicam.Project.Application.Extensions;
using Unicam.Project.Application.Models.Requests;

namespace Unicam.Project.Application.Validators
{
    public class CreateCategoriaRequestValidator : AbstractValidator<CreateCategoriaRequest>
    {
        public CreateCategoriaRequestValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Il nome della categoria è obbligatorio")
                .NotNull()
                .WithMessage("Il campo nome non può essere nullo")
                .MaximumLength(100)
                .WithMessage("Il nome della categoria non può superare i 100 caratteri")
                .RegEx(@"^[a-zA-Zà-úÀ-Ú\s]+$", "Il campo nome non può contenere numeri o caratteri speciali");
        }
    }

}
