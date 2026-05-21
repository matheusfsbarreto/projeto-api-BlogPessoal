using BlogPessoal.Data;
using BlogPessoal.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await _context.Usuarios
                .Include(u => u.Postagens)
                .ToListAsync();
        }

        public async Task<Usuario?> GetById(long id)
        {
            return await _context.Usuarios
                .Include(u => u.Postagens)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario?> GetByUsuario(string usuario)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Usuario1 == usuario);
        }

        public async Task<Usuario?> Create(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return await GetById(usuario.Id);
        }

        public async Task<Usuario?> Update(Usuario usuario)
        {
            var usuarioExiste = await GetById(usuario.Id);

            if (usuarioExiste is null)
                return null;

            _context.Entry(usuarioExiste).State = EntityState.Detached;
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return await GetById(usuario.Id);
        }

        public async Task Delete(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }
}