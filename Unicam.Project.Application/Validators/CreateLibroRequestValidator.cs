using FluentValidation;
using Unicam.Project.Application.Extensions;
using Unicam.Project.Application.Models.Requests;

namespace Unicam.Project.Application.Validators
{
    public class CreateLibroRequestValidator : AbstractValidator<CreateLibroRequest>
    {
        public CreateLibroRequestValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Il nome del libro è obbligatorio")
                .NotNull()
                .WithMessage("Il campo nome non può essere nullo")
                .MaximumLength(100)
                .WithMessage("Il nome del libro non può superare i 100 caratteri")
                .RegEx(@"^[a-zA-Zà-úÀ-Ú\s]+$", "Il campo nome non può contenere numeri o caratteri speciali");

            RuleFor(x => x.Autore)
                .NotEmpty()
                .WithMessage("Il nome dell'autore è obbligatorio")
                .NotNull()
                .WithMessage("Il campo autore non può essere nullo")
                .MaximumLength(100)
                .WithMessage("Il nome dell'autore non può superare i 100 caratteri")
                .RegEx(@"^[a-zA-Zà-úÀ-Ú\s]+$", "Il campo autore non può contenere numeri o caratteri speciali");

            RuleFor(x => x.Editore)
                .NotEmpty()
                .WithMessage("Il nome dell'editore è obbligatorio")
                .NotNull()
                .WithMessage("Il campo editore non può essere nullo")
                .MaximumLength(100)
                .WithMessage("Il nome dell'editore non può superare i 100 caratteri")
                .RegEx(@"^[a-zA-Zà-úÀ-Ú\s]+$", "Il campo editore non può contenere numeri o caratteri speciali");

            RuleFor(x => x.DataPubblicazione)
                .NotEmpty()
                .WithMessage("la data di pubblicazione è obbligatorio")
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("La data di pubblicazione non può essere nel futuro");

            RuleFor(x => x.CategorieNomi)
                .NotEmpty()
                .WithMessage("Devi specificare almeno una categoria");
        }
    }
}
