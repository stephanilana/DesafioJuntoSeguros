using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesafioJuntoSeguros.Application.DTOs;
using DesafioJuntoSeguros.Application.Services;
using DesafioJuntoSeguros.Domain.Intefaces;
using Moq;
using Xunit;

namespace Testes.Application.Services
{
    public class UsuarioServiceTeste
    {
        private UsuarioService usuarioService;
        public UsuarioServiceTeste()
        {
            usuarioService = new UsuarioService(new Mock<IUsuarioRepository>().Object);
        }

        [Fact]
        public  void Post_CamposObrigatorios()
        {
            var exception = Assert.Throws<Exception>(() => usuarioService.AddUsuario(new AddUsuarioDto { Nome = "Teste", Email = "teste@teste.com", CPF = "10516090976", Senha = "123456" }));
            Assert.Equal("Campos obrigatórios não preenchidos", exception.Message);
        }

        [Fact]
        public void Post_AddUsuario()
        {
            Assert.NotNull(usuarioService.AddUsuario(new AddUsuarioDto { Nome = "Teste", Email = "teste@teste.com", CPF = "10516090976", Senha = "123456" }));
        }

        [Fact]
        public void GetById_IdNulo()
        {
            var exception = Assert.Throws<Exception>(() => usuarioService.GetById(null));
            Assert.Equal("Id do Usuário é obrigatório", exception.Message);
        }

        [Fact]
        public void GetById_BuscaUsuario()
        {
            Assert.NotNull(usuarioService.GetById(Guid.Parse("8279394f-a384-44a3-a6d9-da06f9271878")));
        }

        [Fact]
        public void Delete_IdNulo()
        {
            var exception = Assert.Throws<Exception>(() => usuarioService.Remove(null));
            Assert.Equal("Id do Usuário é obrigatório", exception.Message);
        }

        [Fact]
        public void Delete_RemoveUsuario()
        {
            Assert.True(usuarioService.Remove(Guid.Parse("8279394f-a384-44a3-a6d9-da06f9271878")));
        }

        [Fact]
        public void Update_IdNulo()
        {
            var exception = Assert.Throws<Exception>(() => usuarioService.Update(null, new UpdateUsuarioDto { Nome = "Teste", Email = "teste@teste.com", CPF = "10516090976", SenhaAtual = "123456", SenhaNova = ""}));
            Assert.Equal("Id do Usuário é obrigatório", exception.Message);
        }

        [Fact]
        public void Update_EditaUsuario()
        {
            Assert.NotNull(usuarioService.Update(Guid.Parse("8279394f-a384-44a3-a6d9-da06f9271878"), new UpdateUsuarioDto { Nome = "Teste", Email = "teste@teste.com", CPF = "10516090976", SenhaAtual = "123456", SenhaNova = "" }));
        }
    }
}
