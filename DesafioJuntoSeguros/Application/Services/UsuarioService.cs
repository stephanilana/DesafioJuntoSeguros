using DesafioJuntoSeguros.Application.DTOs;
using DesafioJuntoSeguros.Application.ViewModels;
using DesafioJuntoSeguros.Domain.Entites;
using DesafioJuntoSeguros.Domain.Intefaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace DesafioJuntoSeguros.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public UsuarioViewModel? AddUsuario(AddUsuarioDto usuarioDto)
        {
            if (!Validations(usuarioDto)) return null;

            var usuario = 
                new Usuario(
                    Guid.NewGuid(),
                    usuarioDto.Nome,
                    usuarioDto.CPF,
                    usuarioDto.Email);
            usuario.Senha = CriptografiaSenha(usuarioDto.Senha);
            _usuarioRepository.Add(usuario);
            return new UsuarioViewModel(usuario.Id, usuario.Nome, usuario.Email, usuario.CPF);
        }

        private bool Validations(AddUsuarioDto usuarioDto)
        {
            if (usuarioDto.Nome == "" || usuarioDto.Email == ""
                                     || usuarioDto.Senha == "" || usuarioDto.CPF == "")
                throw new Exception("Campos obrigatórios não preenchidos");
            if (_usuarioRepository.Get() != null && _usuarioRepository.Get().Exists(c => c.Email == usuarioDto.Email))
            {
                throw new Exception("Usuário já cadastrado com esse E-mail");
            }
            if (_usuarioRepository.Get() != null && _usuarioRepository.Get().Exists(c => c.CPF == usuarioDto.CPF))
            {
                throw new Exception("Usuário já cadastrado com esse CPF");
            }

            if (usuarioDto.Senha.Length < 6)
                throw new Exception("A senha deve conter mais de 6 caracteres");

            if (!ValidarCPF(usuarioDto.CPF))
                throw new Exception("CPF incorreto");

            return true;
        }

        public List<UsuarioViewModel?> Get()
        {
            var listUsuario = _usuarioRepository.Get();

            return listUsuario.Select(c =>
                 new UsuarioViewModel(c.Id, c.Nome, c.Email, c.CPF)
            ).ToList();
        }

        public UsuarioViewModel? GetById(Guid? id)
        {
            var usuario = _usuarioRepository.GetById(id);
            if (!ValidaExisteUsuario(id))
                return null;

            return new UsuarioViewModel(usuario.Id, usuario.Nome, usuario.Email, usuario.CPF);
        }

        public bool Remove(Guid? id)
        {
            var usuario = _usuarioRepository.GetById(id);
            if (!ValidaExisteUsuario(id))
                return false;

            return _usuarioRepository.Remove(usuario);
        }

        public UsuarioViewModel? Update(Guid? id, UpdateUsuarioDto usuarioDto)
        {
            var usuario = _usuarioRepository.GetById(id);
            if (!ValidaExisteUsuario(id))
                return null;

            usuario.Nome = usuarioDto.Nome;
            if (_usuarioRepository.Get() != null && _usuarioRepository.Get().Exists(c => c.CPF == usuarioDto.CPF && c.Id != usuario.Id))
            {
                throw new Exception("Usuário já cadastrado com esse CPF");
            }
            ValidarCPF(usuarioDto.CPF);
            usuario.CPF = usuarioDto.CPF;
            usuario.Email = usuarioDto.Email;


            ValidaAlteracaoSenha(usuarioDto, usuario);

            var result = _usuarioRepository.Update(usuario);
            if (result)
                return new UsuarioViewModel(usuario.Id, usuario.Nome, usuario.Email, usuario.CPF);
            return null;
        }
        private bool ValidaAlteracaoSenha(UpdateUsuarioDto usuarioDto, Usuario usuario)
        {
            if (usuarioDto.SenhaNova.Length < 6)
                throw new Exception("A senha deve conter mais de 6 caracteres");

            using (MD5 md5Hash = MD5.Create())
            {
                var senha = CriptografiaSenha(usuarioDto.SenhaAtual);
                if (VerificarHash(usuario.Senha, senha))
                {
                    usuario.Senha = CriptografiaSenha(usuarioDto.SenhaNova);
                    return true;
                }
                else
                {
                    throw new Exception("Senha atual incorreta");
                }
            }
        }
        public static bool ValidarCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf)) return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto;
            return cpf.EndsWith(digito);
        }
        public static string CriptografiaSenha(string senha)
        {
            try
            {
                System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(senha);
                byte[] hash = md5.ComputeHash(inputBytes);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                return sb.ToString(); // Retorna senha criptografada 
            }
            catch (Exception)
            {
                return null; // Caso encontre erro retorna nulo
            }
        }
        private bool VerificarHash(string input, string hash)
        {
            StringComparer compara = StringComparer.OrdinalIgnoreCase;

            if (0 == compara.Compare(input, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool ValidaExisteUsuario(Guid? id)
        {
            if (id == null)
            {
                throw new Exception("Id do Usuário é obrigatório");
            }
            if (_usuarioRepository.Get() != null && !_usuarioRepository.Get().Exists(c => c.Id == id))
            {
                throw new Exception("Usuário não encontrado");
            }

            return true;
        }
    }
}