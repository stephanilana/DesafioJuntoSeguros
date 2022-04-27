using DesafioJuntoSeguros.Application.DTOs;
using DesafioJuntoSeguros.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioJuntoSeguros.Domain.Intefaces
{
    public interface IUsuarioService
    {
        UsuarioViewModel? AddUsuario(AddUsuarioDto studentDto);
        List<UsuarioViewModel?> Get();
        UsuarioViewModel? GetById(Guid? id);
        bool Remove(Guid? id);
        UsuarioViewModel? Update(Guid? id, UpdateUsuarioDto student);
    }
}
