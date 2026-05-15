# 🛠️ AMS Helpdesk API

API REST desenvolvida em **.NET 8** para gerenciamento de chamados (tickets), com autenticação JWT, controle de acesso por perfil e arquitetura baseada em casos de uso (Use Cases).

---

## 📌 Sobre o projeto

Este projeto simula um sistema interno de Helpdesk, onde usuários podem abrir chamados e administradores podem gerenciar e atribuir tickets.

O projeto foi desenvolvido com foco em boas práticas de desenvolvimento backend utilizando ASP.NET Core, evoluindo gradualmente para uma estrutura mais organizada, desacoplada e próxima de aplicações utilizadas no mercado.

---

## 🚀 Tecnologias utilizadas

- .NET 8 (ASP.NET Core Web API)
- Entity Framework Core
- SQL Server
- Docker
- Docker Compose
- JWT Authentication
- ASP.NET Identity PasswordHasher
- Swagger (Swashbuckle)

---

## 🧠 Conceitos e arquitetura aplicados

Atualmente o projeto utiliza:

- Arquitetura em camadas
- Use Cases
- DTOs (Request / Response)
- Result Pattern
- Mappers
- Middleware global de exceções
- Autenticação JWT
- Autorização baseada em Roles e Policies
- Separação de responsabilidades
- Regras de negócio centralizadas na camada de aplicação
- Migrations automáticas com EF Core

---

## 📂 Estrutura do projeto

```bash
/Controllers
/Application
    /Tickets
        /AssignTicket
        /CreateTicket
        /DeleteTicket
        /GetTicket
        /TakeTicket
        /UpdateTicket
        /Common
/Infrastructure
    /Data
/Domain
    /Entities
/Migrations
/Services
/Middleware
```

### 📌 Responsabilidades

- **Controllers** → Endpoints HTTP e autenticação/autorização
- **Application** → Casos de uso e regras de negócio
- **Domain** → Entidades da aplicação
- **Infrastructure/Data** → Persistência e contexto do banco
- **Services** → JWT, hash de senha e serviços auxiliares
- **Middleware** → Tratamento global de exceções
- **Migrations** → Versionamento do banco

---

## 🔐 Autenticação e autorização

A API utiliza autenticação baseada em JWT.

### Segurança implementada

- JWT via variáveis de ambiente
- Password hashing com `PasswordHasher`
- Claims customizadas
- Roles (`User` e `Admin`)
- Policies de autorização
- Middleware global para tratamento de exceções

---

## 👥 Perfis de usuário

### 👤 User

- Criar tickets
- Visualizar tickets
- Assumir tickets (`take`)

### 👑 Admin

- Todas as permissões de User
- Atribuir tickets
- Reatribuir tickets
- Deletar tickets

---

## 📌 Funcionalidades

### 👤 Autenticação

- `POST /api/Auth/register` → Cadastro de usuário
- `POST /api/Auth/login` → Login e geração de token JWT

---

### 🎫 Tickets

- `GET /api/Tickets` → Listar tickets
- `POST /api/Tickets` → Criar ticket
- `PUT /api/Tickets/{id}` → Atualizar ticket
- `DELETE /api/Tickets/{id}` → Deletar ticket (**Admin**)

---

### 🔄 Gerenciamento de tickets

- `PUT /api/Tickets/{id}/assign/{userId}` → Atribuir ticket (**Admin**)
- `PUT /api/Tickets/{id}/take` → Usuário assume ticket

---

## 📜 Regras de negócio implementadas

- Não é possível editar tickets fechados
- Não é possível assumir tickets já atribuídos
- Não é possível assumir tickets fechados
- Admin pode reatribuir tickets
- Não é possível atribuir tickets para usuários inexistentes
- Apenas tickets abertos podem ser deletados

---

## ✅ Result Pattern

Os Use Cases utilizam um padrão padronizado de retorno para sucesso e falha:

```csharp
Result<T>
```

Exemplo:

```csharp
return Result<TicketResponse>.Success(data);

return Result<TicketResponse>.Failure("Erro");
```

---

## 🧪 Como executar o projeto

### 1. Clone o repositório

```bash
git clone https://github.com/tacitobatista/ams-helpdesk-api.git
```

---

### 2. Configure as variáveis de ambiente

Crie um arquivo `.env` baseado no `.env.example`

Exemplo:

```env
SA_PASSWORD=Your_password123
DB_NAME=AmsHelpdeskDb
JWT_KEY=sua-chave-super-secreta
JWT_ISSUER=AmsHelpdeskApi
JWT_AUDIENCE=AmsHelpdeskApiUsers
```

---

### 3. Suba o SQL Server com Docker

```bash
docker compose up -d db
```

---

### 4. Execute a API

Via Visual Studio ou:

```bash
dotnet run
```

---

### 5. Acesse o Swagger

```bash
https://localhost:xxxx/swagger
```

---

## 🐳 Executando stack completa com Docker

```bash
docker compose up --build
```

---

## 🔑 Uso do JWT no Swagger

1. Faça login em `/api/Auth/login`
2. Copie o token retornado
3. Clique em **Authorize 🔒**
4. Insira o token JWT

---

## 📈 Próximas melhorias planejadas

- Testes automatizados
- Logs estruturados
- RabbitMQ / mensageria
- Background Services
- Observabilidade
- Cache distribuído
- PostgreSQL
- Kubernetes
- CI/CD
- Deploy em cloud (Azure / AWS)

---

## 👨‍💻 Autor

Desenvolvido por **Tácito Machado Batista**

- Experiência como Analista de Desenvolvimento na Volkswagen (AMS .NET)
- Foco em backend e APIs REST