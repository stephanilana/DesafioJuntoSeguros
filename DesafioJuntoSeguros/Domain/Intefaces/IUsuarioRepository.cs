using DesafioJuntoSeguros.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioJuntoSeguros.Domain.Intefaces
{
    public interface IUsuarioRepository
    {
        Usuario Add(Usuario usuario);
        Usuario GetLogin(string email, string senha);
        List<Usuario?> Get();

        Usuario GetById(Guid? id);
        bool Remove(Usuario usuario);
        bool Update(Usuario usuario);
    }
}
