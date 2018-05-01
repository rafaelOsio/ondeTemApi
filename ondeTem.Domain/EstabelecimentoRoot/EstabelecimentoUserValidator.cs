using FluentValidation;

namespace ondeTem.Domain.EstabelecimentoRoot
{
    public class EstabelecimentoUserValidator : AbstractValidator<EstabelecimentoUser>
    {
        public EstabelecimentoUserValidator()
        {
            RuleFor(i => i.Email).NotEmpty()
                                .WithMessage("O campo 'Email' é obrigatório.")
                            .MaximumLength(100)
                                .WithMessage("O campo 'Email' aceita apenas 100 caracteres.");

            RuleFor(i => i.Password).NotEmpty()
                                .WithMessage("O campo 'Password' é obrigatório.")
                            .MaximumLength(100)
                                .WithMessage("O campo 'Password' aceita apenas 100 caracteres.");
        }
    }
}