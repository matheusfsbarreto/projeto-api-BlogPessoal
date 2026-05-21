using BlogPessoal.DTOs;
using BlogPessoal.Models;

namespace BlogPessoal.Config
{
    public static class MappingHelper
    {
        public static Usuario ToUsuario(UsuarioRequestDTO dto)
        {
            return new Usuario
            {
                Id = dto.Id ?? 0,
                Nome = dto.Nome,
                Usuario1 = dto.Usuario,
                Senha = dto.Senha,
                Foto = dto.Foto
            };
        }

        public static UsuarioResponseDTO ToUsuarioResponse(Usuario usuario)
        {
            return new UsuarioResponseDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Usuario = usuario.Usuario1,
                Foto = usuario.Foto
            };
        }

        public static Tema ToTema(TemaRequestDTO dto)
        {
            return new Tema
            {
                Id = dto.Id ?? 0,
                Descricao = dto.Descricao
            };
        }

        public static TemaResponseDTO ToTemaResponse(Tema tema)
        {
            return new TemaResponseDTO
            {
                Id = tema.Id,
                Descricao = tema.Descricao
            };
        }

        public static Postagem ToPostagem(PostagemRequestDTO dto)
        {
            return new Postagem
            {
                Id = dto.Id ?? 0,
                Titulo = dto.Titulo,
                Texto = dto.Texto,
                TemaId = dto.TemaId ?? 0,
                UsuarioId = dto.UsuarioId ?? 0
            };
        }

        public static PostagemResponseDTO ToPostagemResponse(Postagem postagem)
        {
            return new PostagemResponseDTO
            {
                Id = postagem.Id,
                Titulo = postagem.Titulo,
                Texto = postagem.Texto,
                Data = postagem.Data,
                Tema = postagem.Tema != null ? ToTemaResponse(postagem.Tema) : null,
                Usuario = postagem.Usuario != null ? ToUsuarioResponse(postagem.Usuario) : null,
                ResumoIA = postagem.ResumoIA,      
                TagsIA = postagem.TagsIA,          
                CategoriaIA = postagem.CategoriaIA 
            };
        }
    }
}