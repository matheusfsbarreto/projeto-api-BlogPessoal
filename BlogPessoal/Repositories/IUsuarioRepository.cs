using BlogPessoal.Models;

namespace BlogPessoal.Repositories
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAll();
        Task<Usuario?> GetById(long id);

        Task<Usuario?> GetByUsuario(string usuario);

        Task<Usuario?> Create(Usuario usuario);
        Task<Usuario?> Update(Usuario usuario);
        Task Delete(Usuario usuario);
    }
}