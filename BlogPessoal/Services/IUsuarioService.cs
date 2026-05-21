using BlogPessoal.Models;

namespace BlogPessoal.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> GetAll();
        Task<Usuario?> GetById(long id);
        Task<Usuario?> Create(Usuario usuario);
        Task<Usuario?> Update(Usuario usuario);
        Task<UsuarioLogin?> Autenticar(UsuarioLogin usuarioLogin);
        Task Delete(Usuario usuario);
    }
}