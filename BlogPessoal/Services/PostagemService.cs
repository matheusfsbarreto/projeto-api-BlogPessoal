using BlogPessoal.Models;
using BlogPessoal.Repositories;
using BlogPessoal.Services.IA;

namespace BlogPessoal.Services
{
    public class PostagemService : IPostagemService
    {
        private readonly IPostagemRepository _repository;
        private readonly IIAService _iaService;

        public PostagemService(IPostagemRepository repository, IIAService iaService)
        {
            _repository = repository;
            _iaService = iaService;
        }

        public async Task<IEnumerable<Postagem>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Postagem?> GetById(long id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<Postagem>> GetByTitulo(string titulo)
        {
            return await _repository.GetByTitulo(titulo);
        }

        public async Task<Postagem?> Create(Postagem postagem)
        {
            if (postagem.TemaId is null)
                return null;

            var resultadoIA = await _iaService.GerarResumoAsync(postagem.Texto);

            if (resultadoIA is not null)
            {
                postagem.ResumoIA = resultadoIA.Resumo;
                postagem.TagsIA = resultadoIA.Tags;
                postagem.CategoriaIA = resultadoIA.Categoria;
            }

            return await _repository.Create(postagem);
        }

        public async Task<Postagem?> Update(Postagem postagem)
        {
            var postagemExiste = await _repository.GetById(postagem.Id);

            if (postagemExiste is null)
                return null;

            if (postagem.TemaId is null)
                return null;

            var resultadoIA = await _iaService.GerarResumoAsync(postagem.Texto);

            if (resultadoIA is not null)
            {
                postagem.ResumoIA = resultadoIA.Resumo;
                postagem.TagsIA = resultadoIA.Tags;
                postagem.CategoriaIA = resultadoIA.Categoria;
            }

            return await _repository.Update(postagem);
        }

        public async Task Delete(Postagem postagem)
        {
            await _repository.Delete(postagem);
        }

        public async Task<IEnumerable<Postagem>> GetByFiltro(long? autorId, long? temaId)
        {
            return await _repository.GetByFiltro(autorId, temaId);
        }
    }
}