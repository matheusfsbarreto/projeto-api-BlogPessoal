using BlogPessoal.Data;
using BlogPessoal.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.Repositories
{
    public class PostagemRepository : IPostagemRepository
    {
        private readonly AppDbContext _context;

        public PostagemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Postagem>> GetAll()
        {
            return await _context.Postagens
                .Include(p => p.Tema)
                .Include(p => p.Usuario)
                .ToListAsync();
        }

        public async Task<Postagem?> GetById(long id)
        {
            return await _context.Postagens
                .Include(p => p.Tema)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Postagem>> GetByTitulo(string titulo)
        {
            return await _context.Postagens
                .Include(p => p.Tema)
                .Include(p => p.Usuario)
                .Where(p => p.Titulo.Contains(titulo))
                .ToListAsync();
        }

        public async Task<Postagem?> Create(Postagem postagem)
        {
            postagem.Data = DateTime.Now;

            _context.Postagens.Add(postagem);
            await _context.SaveChangesAsync();

            return await GetById(postagem.Id);
        }

        public async Task<Postagem?> Update(Postagem postagem)
        {
            var postagemExiste = await GetById(postagem.Id);

            if (postagemExiste is null)
                return null;

            postagem.Data = DateTime.Now;

            _context.Entry(postagemExiste).State = EntityState.Detached;
            _context.Postagens.Update(postagem);
            await _context.SaveChangesAsync();

            return await GetById(postagem.Id);
        }

        public async Task Delete(Postagem postagem)
        {
            _context.Postagens.Remove(postagem);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Postagem>> GetByFiltro(long? autorId, long? temaId)
        {
            var query = _context.Postagens
                .Include(p => p.Tema)
                .Include(p => p.Usuario)
                .AsQueryable();

            if (autorId.HasValue)
                query = query.Where(p => p.UsuarioId == autorId);

            if (temaId.HasValue)
                query = query.Where(p => p.TemaId == temaId);

            return await query.ToListAsync();
        }
    }
}