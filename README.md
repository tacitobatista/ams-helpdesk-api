# 🛠️ AMS Helpdesk API

API REST desenvolvida em **.NET 8** para gerenciamento de chamados (tickets), com autenticação JWT e controle de acesso por perfil (Admin/User).

---

## 📌 Sobre o projeto

Este projeto simula um sistema de atendimento interno (Helpdesk), onde usuários podem abrir chamados e administradores podem gerenciar e atribuir tickets.

A aplicação foi desenvolvida com foco em boas práticas de backend, incluindo:

* Autenticação com JWT
* Controle de acesso por roles
* Validação de dados
* Separação em camadas (Controller / Service)
* Persistência com Entity Framework Core

---

## 🚀 Tecnologias utilizadas

* .NET 8 (ASP.NET Core Web API)
* Entity Framework Core
* SQL Server (LocalDB)
* JWT (Json Web Token)
* Swagger (Swashbuckle)

---

## 🔐 Autenticação e Autorização

A API utiliza autenticação baseada em **JWT**.

### Perfis de usuário:

* **User**

  * Criar tickets
  * Visualizar tickets
  * Assumir tickets (`take`)

* **Admin**

  * Todas as permissões de User
  * Atribuir tickets
  * Deletar tickets

---

## 📂 Estrutura do projeto

```
/Controllers
/Models
/Services
/Data
/Migrations
```

* **Controllers** → Endpoints da API
* **Services** → Regras de negócio
* **Data** → Contexto do banco
* **Models** → Entidades
* **Migrations** → Versionamento do banco

---

## 📌 Funcionalidades

### 👤 Autenticação

* `POST /api/Auth/register` → Cadastro de usuário
* `POST /api/Auth/login` → Login e geração de token

---

### 🎫 Tickets

* `GET /api/Tickets` → Listar tickets
* `POST /api/Tickets` → Criar ticket
* `PUT /api/Tickets/{id}` → Atualizar ticket
* `DELETE /api/Tickets/{id}` → Deletar ticket (**Admin**)

---

### 🔄 Atribuição de tickets

* `PUT /api/Tickets/{id}/assign/{userId}` → Atribuir ticket (**Admin**)
* `PUT /api/Tickets/{id}/take` → Usuário assume ticket

---

## 🧪 Como executar o projeto

1. Clone o repositório:

```bash
git clone https://github.com/tacitobatista/ams-helpdesk-api.git
```

2. Acesse a pasta do projeto:

```bash
cd AmsHelpdeskApi
```

3. Execute as migrations:

```bash
dotnet ef database update
```

4. Rode a aplicação:

```bash
dotnet run
```

5. Acesse o Swagger:

```
https://localhost:xxxx/swagger
```

---

## 🔑 Uso do JWT no Swagger

1. Faça login em `/api/Auth/login`
2. Copie o token retornado
3. Clique em **Authorize 🔒**
4. Insira apenas o token (sem "Bearer")

---

## 📈 Possíveis melhorias

* Hash de senha com BCrypt
* Refresh Token
* Logs estruturados
* Testes automatizados
* Deploy em cloud (Azure / AWS)

---

## 👨‍💻 Autor

Desenvolvido por **Tácito Machado Batista**

* Experiência como Analista de Desenvolvimento na Volkswagen (AMS .NET)
* Foco em backend e APIs REST
