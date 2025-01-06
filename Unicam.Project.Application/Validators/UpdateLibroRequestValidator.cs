using FluentValidation;
using Unicam.Project.Application.Models.Requests;
using System.Text.RegularExpressions;

namespace Unicam.Project.Application.Validators
{
    public class UpdateLibroRequestValidator : AbstractValidator<UpdateLibroRequest>
    {
        public UpdateLibroRequestValidator()
        {
            RuleFor(x => x.Nome)
                .MaximumLength(100)
                .WithMessage("Il nome del libro non può superare i 100 caratteri")
                .Custom((value, context) =>
                {
                    var regEx = new Regex(@"^[a-zA-Zà-úÀ-Ú\s]+$");
                    if (regEx.IsMatch(value) == false)
                    {
                        context.AddFailure("Il campo del nome non può contenere numeri o caratteri speciali");
                    }
                })
                .When(x => !string.IsNullOrWhiteSpace(x.Nome));


            RuleFor(x => x.Autore)
                .MaximumLength(100)
                .WithMessage("Il nome dell'autore non può superare i 100 caratteri")
                .Custom((value, context) =>
                {
                    var regEx = new Regex(@"^[a-zA-Zà-úÀ-Ú\s]+$");
                    if (regEx.IsMatch(value) == false)
                    {
                        context.AddFailure("Il campo dell'autore non può contenere numeri o caratteri speciali");
                    }
                })
                .When(x => !string.IsNullOrWhiteSpace(x.Autore));

            RuleFor(x => x.Editore)
                .MaximumLength(100)
                .WithMessage("Il nome dell'editore non può superare i 100 caratteri")
                .Custom((value, context) =>
                {
                    var regEx = new Regex(@"^[a-zA-Zà-úÀ-Ú\s]+$");
                    if (regEx.IsMatch(value) == false)
                    {
                        context.AddFailure("Il campo dell'editore non può contenere numeri o caratteri speciali");
                    }
                })
                .When(x => !string.IsNullOrWhiteSpace(x.Editore));

            RuleFor(x => x.DataPubblicazione)
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("La data di pubblicazione non può essere nel futuro")
                .When(x => x.DataPubblicazione.HasValue);
        }
    }
}
