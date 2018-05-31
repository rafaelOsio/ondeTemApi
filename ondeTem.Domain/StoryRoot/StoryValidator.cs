using FluentValidation;

namespace ondeTem.Domain.StoryRoot
{
    public class StoryValidator : AbstractValidator<Story>
    {
        public StoryValidator()
        {
            RuleFor(i => i.Descricao).NotEmpty()
                                .WithMessage("O campo 'Descricao' é obrigatório.")
                            .MaximumLength(500)
                                .WithMessage("O campo 'Descricao' aceita apenas 500 caracteres.");
            
            RuleFor(i => i.CategoriaId).NotEmpty()
                                .WithMessage("O campo 'CategoriaId' é obrigatório.");
        }
    }
}