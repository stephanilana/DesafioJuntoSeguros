using System;

namespace DesafioJuntoSeguros.Domain.Entites
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Usuario()
        { }
        public Usuario(Guid id, string nome, string cpf, string email, string senha)
        {
            Id = id;
            Nome = nome;
            CPF = cpf;
            Email = email;
            Senha = senha;
        }

        public Usuario(Guid id, string nome, string cpf, string email)
        {
            Id = id;
            Nome = nome;
            CPF = cpf;
            Email = email;
        }

    }
}
