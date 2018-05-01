using FluentValidation;

namespace ondeTem.Domain.EstabelecimentoRoot
{
    public class EstabelecimentoValidator : AbstractValidator<Estabelecimento>
    {
        public EstabelecimentoValidator()
        {
            RuleFor(i => i.Email).NotEmpty()
                                .WithMessage("O campo 'Email' é obrigatório.")
                            .MaximumLength(100)
                                .WithMessage("O campo 'Email' aceita apenas 100 caracteres.");

            RuleFor(i => i.Password).MaximumLength(100)
                                .WithMessage("O campo 'Password' aceita apenas 100 caracteres.");
                                
            RuleFor(i => i.Nome).NotEmpty()
                                .WithMessage("O campo 'Nome' é obrigatório.")
                            .MaximumLength(65)
                                .WithMessage("O campo 'Nome' aceita apenas 65 caracteres.");

            RuleFor(i => i.Rua).NotEmpty()
                                .WithMessage("O campo 'Rua' é obrigatório.")
                            .MaximumLength(65)
                                .WithMessage("O campo 'Rua' aceita apenas 65 caracteres.");

            RuleFor(i => i.Bairro).NotEmpty()
                                .WithMessage("O campo 'Bairro' é obrigatório.")
                            .MaximumLength(65)
                                .WithMessage("O campo 'Bairro' aceita apenas 65 caracteres.");

            RuleFor(i => i.Numero).NotEmpty()
                                .WithMessage("O campo 'Numero' é obrigatório.")
                            .MaximumLength(10)
                                .WithMessage("O campo 'Numero' aceita apenas 10 caracteres.");

            RuleFor(i => i.Complemento).MaximumLength(65)
                                .WithMessage("O campo 'Complemento' aceita apenas 65 caracteres.");

            RuleFor(i => i.Latitude).NotEmpty()
                                .WithMessage("O campo 'Latitude' é obrigatório.");

            RuleFor(i => i.Longitude).NotEmpty()
                                .WithMessage("O campo 'Longitude' é obrigatório.");

            RuleFor(i => i.TelefonePrincipal).NotEmpty()
                                .WithMessage("O campo 'Telefone principal' é obrigatório.")
                            .MaximumLength(14)
                                .WithMessage("O campo 'Telefone principal' aceita apenas 14 caracteres.");

            RuleFor(i => i.TelefoneSecundario).MaximumLength(14)
                                .WithMessage("O campo 'Telefone secundario' aceita apenas 14 caracteres.");

            RuleFor(i => i.MensagemParaClientes).MaximumLength(400)
                                .WithMessage("O campo 'Mensagem para os clientes' aceita apenas 400 caracteres.");
        }
    }
}