using BlogPessoal.Data;
using BlogPessoal.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.Repositories
{
    public class TemaRepository : ITemaRepository
    {
        private readonly AppDbContext _context;

        public TemaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tema>> GetAll()
        {
            return await _context.Temas
                .Include(t => t.Postagens)
                .ToListAsync();
        }

        public async Task<Tema?> GetById(long id)
        {
            return await _context.Temas
                .Include(t => t.Postagens)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Tema>> GetByDescricao(string descricao)
        {
            return await _context.Temas
                .Include(t => t.Postagens)
                .Where(t => t.Descricao.Contains(descricao))
                .ToListAsync();
        }

        public async Task<Tema?> Create(Tema tema)
        {
            _context.Temas.Add(tema);
            await _context.SaveChangesAsync();
            return await GetById(tema.Id);
        }

        public async Task<Tema?> Update(Tema tema)
        {
            var temaExiste = await GetById(tema.Id);

            if (temaExiste is null)
                return null;

            _context.Entry(temaExiste).State = EntityState.Detached;
            _context.Temas.Update(tema);
            await _context.SaveChangesAsync();

            return await GetById(tema.Id);
        }

        public async Task Delete(Tema tema)
        {
            _context.Temas.Remove(tema);
            await _context.SaveChangesAsync();
        }
    }
}