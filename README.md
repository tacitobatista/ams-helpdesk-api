# 🛠️ AMS Helpdesk API

API REST desenvolvida em **.NET 8** para gerenciamento de chamados (tickets), com autenticação JWT, controle de acesso por perfil e arquitetura baseada em casos de uso (Use Cases).

---

## 📌 Sobre o projeto

Este projeto simula um sistema de atendimento interno (Helpdesk), onde usuários podem abrir chamados e administradores podem gerenciar e atribuir tickets.

O projeto começou como uma API simples para estudo da plataforma ASP.NET Core e foi evoluindo gradualmente com foco em:

- Arquitetura backend
- Organização de código
- Separação de responsabilidades
- Regras de negócio
- Padrões utilizados no mercado .NET

---

## 🚀 Tecnologias utilizadas

- .NET 8 (ASP.NET Core Web API)
- Entity Framework Core
- SQL Server (LocalDB)
- JWT (Json Web Token)
- Swagger (Swashbuckle)

---

## 🧠 Conceitos e arquitetura aplicados

Durante o desenvolvimento, o projeto foi refatorado para utilizar uma estrutura mais organizada e escalável.

Atualmente a aplicação utiliza:

- Use Cases
- DTOs (Request / Response)
- Result Pattern
- Mappers
- Separação de responsabilidades
- Regras de negócio centralizadas na camada de aplicação

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
```

### 📌 Responsabilidades

- **Controllers** → Endpoints HTTP e autenticação/autorização
- **Application** → Casos de uso e regras de negócio
- **Domain** → Entidades da aplicação
- **Data** → Contexto do banco e persistência
- **Migrations** → Versionamento do banco

---

## 🔐 Autenticação e autorização

A API utiliza autenticação baseada em **JWT**.

### Perfis de usuário

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

### 2. Acesse a pasta do projeto

```bash
cd AmsHelpdeskApi
```

---

### 3. Execute as migrations

```bash
dotnet ef database update
```

---

### 4. Rode a aplicação

```bash
dotnet run
```

---

### 5. Acesse o Swagger

```bash
https://localhost:xxxx/swagger
```

---

## 🔑 Uso do JWT no Swagger

1. Faça login em `/api/Auth/login`
2. Copie o token retornado
3. Clique em **Authorize 🔒**
4. Insira apenas o token (sem `Bearer`)

---

## 📈 Próximas melhorias planejadas

O projeto continuará evoluindo com foco em tecnologias amplamente utilizadas no mercado backend .NET:

- Docker
- Docker Compose
- PostgreSQL
- Mensageria com RabbitMQ
- Background Services
- Kubernetes
- Logs estruturados
- Testes automatizados
- Observabilidade
- Deploy em cloud (Azure / AWS)

---

## 👨‍💻 Autor

Desenvolvido por **Tácito Machado Batista**

- Experiência como Analista de Desenvolvimento na Volkswagen (AMS .NET)
- Foco em backend e APIs REST
- Estudando arquitetura backend e tecnologias DevOps
