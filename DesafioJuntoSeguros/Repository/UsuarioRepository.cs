using DesafioJuntoSeguros.Domain;
using DesafioJuntoSeguros.Domain.Entites;
using DesafioJuntoSeguros.Domain.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioJuntoSeguros.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationContext _context;

        public UsuarioRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Usuario Add(Usuario student)
        {
            _context.Usuarios.Add(student);
            _context.SaveChangesAsync();
            return student;
        }

        public List<Usuario?> Get()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario GetById(Guid? id)
        {
            return _context.Usuarios.FirstOrDefault(c => id != null && c.Id == id);
        }

        public bool Remove(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            _context.SaveChangesAsync();
            return true;
        }

        public bool Update(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChangesAsync();
            return true;
        }
    }
}