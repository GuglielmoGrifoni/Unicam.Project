using FluentValidation;
using Unicam.Project.Application.Extensions;
using Unicam.Project.Application.Models.Requests;

namespace Unicam.Project.Application.Validators
{
    public class CreateUtenteRequestValidator : AbstractValidator<CreateUtenteRequest>
    {
        public CreateUtenteRequestValidator()
        {
            RuleFor(r => r.Nome)
                .NotEmpty()
                .WithMessage("Il campo nome è obbligatorio")
                .NotNull()
                .WithMessage("Il campo nome non può essere nullo")
                .MaximumLength(100)
                .WithMessage("Il campo nome non può superare i 100 caratteri")
                .RegEx(@"^[a-zA-Zà-úÀ-Ú\s]+$", "Il campo nome non può contenere numeri o caratteri speciali");

            RuleFor(r => r.Cognome)
                .NotEmpty()
                .WithMessage("Il campo cognome è obbligatorio")
                .NotNull()
                .WithMessage("Il campo cognome non può essere nullo")
                .MaximumLength(100)
                .WithMessage("Il campo cognome non può superare i 100 caratteri")
                .RegEx(@"^[a-zA-Zà-úÀ-Ú\s]+$", "Il campo cognome non può contenere numeri o caratteri speciali");

            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage("Il campo email è obbligatorio")
                .NotNull()
                .WithMessage("Il campo email non può essere nullo")
                .EmailAddress()
                .WithMessage("L'email inserita non è valida")
                .Matches(@"^[^@\s]+@[^@\s]+\.(it|com|org|net)$")
                .WithMessage("L'email: o non contiene la '@' o non ha caratteri prima della '@' oppure deve terminare con uno dei seguenti domini: .it, .com, .org, .net")
                .MaximumLength(100)
                .WithMessage("L'email non può superare i 100 caratteri");

            RuleFor(r => r.Password)
                .NotEmpty()
                .WithMessage("Il campo password è obbligatorio")
                .NotNull()
                .WithMessage("Il campo password non può essere nullo")
                .MinimumLength(6)
                .WithMessage("Il campo password deve essere almeno lungo 6 caratteri")
                .RegEx(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*()_+{}[\]:;<>,.?~\\-]).{6,}$",
                    "Il campo password deve essere lungo almeno 6 caratteri e deve contenere almeno un carattere maiuscolo, uno minuscolo, un numero e un carattere speciale");
        }
    }
}
