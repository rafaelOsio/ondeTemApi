using FluentValidation;

namespace ondeTem.Domain.ProdutoRoot
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator()
        {
            RuleFor(i => i.Nome).NotEmpty()
                                .WithMessage("O campo 'Nome' é obrigatório.")
                            .MaximumLength(100)
                                .WithMessage("O campo 'Nome' aceita apenas 100 caracteres.");

            RuleFor(i => i.Descricao).MaximumLength(500)
                                .WithMessage("O campo 'Descrição' aceita apenas 500 caracteres.");
                        
            RuleFor(i => i.CategoriaId).NotEmpty()
                                .WithMessage("O campo 'CategoriaId' é obrigatório.");

            RuleFor(i => i.EstabelecimentoId).NotEmpty()
                                .WithMessage("O campo 'EstabelecimentoId' é obrigatório.");                                            
        }
    }
}