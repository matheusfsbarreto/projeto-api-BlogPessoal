# 📝 Blog Pessoal — API REST em ASP.NET Core

API REST para um blog pessoal desenvolvida em C# com ASP.NET Core, autenticação JWT, integração com IA e análise de qualidade com SonarQube. Projeto desenvolvido durante o programa **Acelera Maker da Montreal**.

---

## 🚀 Funcionalidades

- ✅ Cadastro, login e gerenciamento de usuários
- ✅ Criação e gerenciamento de postagens e temas
- ✅ Autenticação com token JWT
- ✅ Senhas criptografadas com BCrypt
- ✅ Filtro de postagens por autor e/ou tema
- ✅ Resumo inteligente, tags e categoria gerados automaticamente por IA (Google Gemini)
- ✅ Validações, tratamento de erros e DTOs
- ✅ Documentação interativa com Swagger
- ✅ Quality Gate aprovado no SonarQube

---

## 🛠️ Tecnologias Utilizadas

- **C# / ASP.NET Core 9** — Framework principal
- **Entity Framework Core 9 + MySQL** — Persistência de dados
- **JWT Bearer + BCrypt** — Autenticação e segurança
- **Google Gemini API** — Inteligência Artificial
- **Swagger/OpenAPI** — Documentação
- **SonarQube + Docker** — Qualidade de código

---

## ⚙️ Como Executar

**Pré-requisitos:** .NET 9 SDK | MySQL 8+ | Chave da [Gemini API](https://aistudio.google.com) (gratuita) | Visual Studio

```bash
# Clone o repositório
git clone https://github.com/matheusfsbarreto/projeto-api-BlogPessoal.git
cd BlogPessoal
```
### Configurar o banco de dados

No MySQL, crie o banco:
```sql
CREATE DATABASE blogpessoal_db;
```

Atualize o `appsettings.json` com as credenciais do banco:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=blogpessoal_db;Uid=root;Pwd=SUA_SENHA;"
  },
  "Jwt": { "Key": "SUA_CHAVE_32_CARACTERES", "Issuer": "BlogPessoal", "Audience": "BlogPessoal" },
  "Gemini": { "ApiKey": "SUA_CHAVE_GEMINI", "Model": "gemini-2.5-flash" }
}
```

```bash
dotnet ef database update
dotnet run
```

Acesse: `https://localhost:xxxx/swagger`

---

## 🖥️ Endpoints

### Autenticação (público)
| Método | Endpoint | Descrição |
|---|---|---|
| `POST` | `/api/usuarios/cadastrar` | Cadastrar usuário |
| `POST` | `/api/usuarios/login` | Login e obter token JWT |

### Usuários (🔒 token)
| Método | Endpoint | Descrição |
|---|---|---|
| `GET` | `/api/usuarios` | Listar todos |
| `GET` | `/api/usuarios/{id}` | Buscar por id |
| `PUT` | `/api/usuarios` | Atualizar |
| `DELETE` | `/api/usuarios/{id}` | Deletar |

### Postagens (🔒 token)
| Método | Endpoint | Descrição |
|---|---|---|
| `GET` | `/api/postagens` | Listar todas |
| `GET` | `/api/postagens/{id}` | Buscar por id |
| `GET` | `/api/postagens/titulo/{titulo}` | Buscar por título |
| `GET` | `/api/postagens/filtro?autor={id}&tema={id}` | Filtrar |
| `POST` | `/api/postagens` | Criar (com IA automática) |
| `PUT` | `/api/postagens` | Atualizar (com IA automática) |
| `DELETE` | `/api/postagens/{id}` | Deletar |

### Temas (🔒 token)
| Método | Endpoint | Descrição |
|---|---|---|
| `GET` | `/api/temas` | Listar todos |
| `GET` | `/api/temas/{id}` | Buscar por id |
| `GET` | `/api/temas/descricao/{descricao}` | Buscar por descrição |
| `POST` | `/api/temas` | Criar |
| `PUT` | `/api/temas` | Atualizar |
| `DELETE` | `/api/temas/{id}` | Deletar |

### IA (🔒 token)
| Método | Endpoint | Descrição |
|---|---|---|
| `POST` | `/api/ia/resumir` | Gerar resumo, tags e categoria de um texto |

---

## 🤖 Inteligência Artificial

Como implementação extra ao desafio. Foi implementado uma sessão que ao criar ou atualizar uma postagem, a API envia o texto automaticamente para o **Google Gemini** e salva os campos gerados:

```json
{
  "resumoIA": "Resumo gerado automaticamente.",
  "tagsIA": "tag1, tag2, tag3",
  "categoriaIA": "Tecnologia"
}
```

---
## 🧪 Exemplos Rápidos

> Todos os endpoints abaixo exigem token, exceto cadastro e login.
> No Swagger, clique em **Authorize 🔓** e cole o token após fazer login.

### 1. Cadastrar usuário
```json
POST /api/usuarios/cadastrar
{
  "nome": "Matheus",
  "usuario": "matheus@email.com",
  "senha": "senha12345"
}
```

### 2. Fazer login e obter token
```json
POST /api/usuarios/login
{
  "usuario": "matheus@email.com",
  "senha": "senha12345"
}
```

### 3. Criar tema
```json
POST /api/temas
{
  "descricao": "Tecnologia"
}
```

### 4. Criar postagem
```json
POST /api/postagens
{
  "titulo": "First Post",
  "texto": "Conteúdo do first post",
  "temaId": 1,
  "usuarioId": 1
}
```

Resposta:
```json
{
  "id": 1,
  "titulo": "First Post",
  "texto": "Conteúdo do first post",
}
```
---
## 🛡️ Validações Implementadas
- E-mail com formato válido obrigatório;
- Senha com mínimo de 6 caracteres;
- Campos obrigatórios validados automaticamente;
- E-mail único;
- Respostas de erro padronizadas em JSON;

---

## 📊 SonarQube — Quality Gate: Passed ✅

| Security | Reliability | Maintainability | Duplications |
|---|---|---|---|
| 🟢 A — 0 | 🟢 A — 0 | 🟢 A - 7 | 🟢 0% |

---

## 👤 Autor

**Matheus F S Barreto**
[![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://linkedin.com/in/matheusfsbarreto)
---