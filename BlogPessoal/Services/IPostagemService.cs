using BlogPessoal.Models;

namespace BlogPessoal.Services
{
    public interface IPostagemService
    {
        Task<IEnumerable<Postagem>> GetAll();
        Task<Postagem?> GetById(long id);
        Task<IEnumerable<Postagem>> GetByFiltro(long? autorId, long? temaId);
        Task<IEnumerable<Postagem>> GetByTitulo(string titulo);
        Task<Postagem?> Create(Postagem postagem);
        Task<Postagem?> Update(Postagem postagem);
        Task Delete(Postagem postagem);
    }
}