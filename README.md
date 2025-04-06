# Pet Care Backend

Este é o backend do sistema **Pet Care**, desenvolvido em .NET 8.0, com suporte a autenticação JWT, versionamento de API, health checks e integração com SQL Server.

## Tecnologias Utilizadas

- **.NET 8.0**
- **Entity Framework Core**
- **SQL Server**
- **Docker**
- **Swagger (OpenAPI)**
- **AspNetCoreRateLimit** (Rate Limiting)
- **Health Checks**
- **GitHub Actions** (CI/CD)

---

## Pré-requisitos

Certifique-se de ter as seguintes ferramentas instaladas:

- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)
- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)

---

## Configuração do Ambiente

1. **Clone o repositório**:

   ```bash
   git clone https://github.com/seu-usuario/pet-care-backend.git
   cd pet-care-backend
   ```

2. **Configure as variáveis de ambiente**:
   Certifique-se de que o arquivo `.env` está configurado corretamente. Exemplo:

   ```properties
   SA_PASSWORD=Your_password123
   ACCEPT_EULA=Y
   ASPNETCORE_ENVIRONMENT=Development
   ConnectionStrings__DefaultConnection=Server=db;Database=PetCareDb;User=sa;Password=Your_password123;
   Jwt__Key=YourSuperSecretKey
   Jwt__Issuer=YourIssuer
   Jwt__Audience=YourAudience
   ```

3. **Suba os contêineres com Docker Compose**:  
   Execute o seguinte comando para iniciar os serviços:

   ```bash
   docker-compose up --build
   ```

4. **Acesse a aplicação**:
   - API: [http://localhost:5000](http://localhost:5000)
   - Swagger: [http://localhost:5000/swagger](http://localhost:5000/swagger)

---

## Estrutura do Projeto

- **WebApi**: Contém a API principal.
- **DAL**: Camada de acesso a dados.
- **BLL**: Camada de lógica de negócios.
- **Entities**: Modelos de dados.
- **Commons**: Classes e utilitários compartilhados.

---

## Endpoints Principais

### Autenticação

- **POST** `/api/auth/login`: Autenticação de usuários.
- **POST** `/api/auth/register`: Registro de novos usuários.

### Pets

- **GET** `/api/pets`: Lista todos os pets.
- **POST** `/api/pets`: Adiciona um novo pet.

### Health Checks

- **GET** `/health`: Verifica a saúde da aplicação.

---

## Testes

Para executar os testes, use o seguinte comando:

```bash
dotnet test
```

---

## CI/CD

O projeto utiliza **GitHub Actions** para integração contínua. O pipeline está configurado no arquivo `.github/workflows/ci.yml` e realiza as seguintes etapas:

- Build da aplicação.
- Execução de testes automatizados.
- Verificação de saúde do banco de dados.

---
