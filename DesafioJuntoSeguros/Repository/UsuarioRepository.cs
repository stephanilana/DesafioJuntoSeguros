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

        public Usuario Add(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChangesAsync();
            return usuario;
        }

        public Usuario GetLogin(string email, string senha)
        {
            return _context.Usuarios.FirstOrDefault(c => email != null && c.Email == email && senha != null && c.Senha == CriptografiaSenha(senha));
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
    }
}