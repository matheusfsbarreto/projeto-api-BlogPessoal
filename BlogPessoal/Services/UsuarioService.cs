using BlogPessoal.Config;
using BlogPessoal.Models;
using BlogPessoal.Repositories;

namespace BlogPessoal.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly JwtService _jwtService;

        public UsuarioService(IUsuarioRepository repository, JwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Usuario?> GetById(long id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Usuario?> Create(Usuario usuario)
        {
            var usuarioExiste = await _repository.GetByUsuario(usuario.Usuario1);

            if (usuarioExiste is not null)
                return null; // email já cadastrado

            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);

            return await _repository.Create(usuario);
        }

        public async Task<Usuario?> Update(Usuario usuario)
        {
            var usuarioExiste = await _repository.GetById(usuario.Id);

            if (usuarioExiste is null)
                return null;

            var usuarioComMesmoEmail = await _repository.GetByUsuario(usuario.Usuario1);

            if (usuarioComMesmoEmail is not null && usuarioComMesmoEmail.Id != usuario.Id)
                return null;

            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);

            return await _repository.Update(usuario);
        }

        public async Task<UsuarioLogin?> Autenticar(UsuarioLogin usuarioLogin)
        {
            var usuario = await _repository.GetByUsuario(usuarioLogin.Usuario);

            if (usuario is null)
                return null;

            bool senhaCorreta = BCrypt.Net.BCrypt.Verify(usuarioLogin.Senha, usuario.Senha);

            if (!senhaCorreta)
                return null;

            // Gera o token JWT
            usuarioLogin.Senha = ""; //para limpar a senha
            usuarioLogin.Token = _jwtService.GerarToken(usuario);
            usuarioLogin.Nome = usuario.Nome;
            usuarioLogin.Foto = usuario.Foto ?? string.Empty;
            usuarioLogin.Id = usuario.Id;

            return usuarioLogin;
        }

        public async Task Delete(Usuario usuario)
        {
            await _repository.Delete(usuario);
        }
    }
}