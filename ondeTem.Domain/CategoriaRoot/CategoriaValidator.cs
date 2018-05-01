using FluentValidation;

namespace ondeTem.Domain.CategoriaRoot
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator()
        {
            RuleFor(i => i.Nome).NotEmpty()
                                .WithMessage("O campo 'Nome' é obrigatório.")
                            .MaximumLength(30)
                                .WithMessage("O campo 'Nome' aceita apenas 30 caracteres.");

            RuleFor(i => i.Descricao).MaximumLength(100)
                                .WithMessage("O campo 'Descrição' aceita apenas 100 caracteres.");
        }
    }
}