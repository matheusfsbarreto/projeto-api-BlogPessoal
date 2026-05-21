using BlogPessoal.Models;
using BlogPessoal.Repositories;

namespace BlogPessoal.Services
{
    public class TemaService : ITemaService
    {
        private readonly ITemaRepository _repository;

        public TemaService(ITemaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Tema>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Tema?> GetById(long id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<Tema>> GetByDescricao(string descricao)
        {
            return await _repository.GetByDescricao(descricao);
        }

        public async Task<Tema?> Create(Tema tema)
        {
            return await _repository.Create(tema);
        }

        public async Task<Tema?> Update(Tema tema)
        {
            var temaExiste = await _repository.GetById(tema.Id);

            if (temaExiste is null)
                return null;

            return await _repository.Update(tema);
        }

        public async Task Delete(Tema tema)
        {
            await _repository.Delete(tema);
        }
    }
}