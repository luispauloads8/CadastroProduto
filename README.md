# 📦 Produto Serviço

**Descrição**: 

Este projeto de software é uma solução integrada para a gestão de diversos aspectos empresariais. Com ele, é possível realizar o cadastro detalhado de clientes e usuários do sistema, bem como gerenciar produtos, serviços, cidades e fornecedores. Além disso, oferece funcionalidades completas para a criação de grupos de contas e contas contábeis.

O software permite a realização de lançamentos de produtos e serviços de forma eficiente, garantindo um controle preciso das transações. Também possui um módulo robusto de geração de relatórios, que fornece uma visão clara e detalhada das movimentações financeiras e operacionais, facilitando a tomada de decisões estratégicas.

---

## 🚀 Tecnologias Utilizadas

- [Angular](https://angular.io/) — Frontend
- [ASP.NET Core](https://dotnet.microsoft.com/) — Backend
- [MySQL](https://www.mysql.com/) — Banco de Dados
- [Node.js](https://nodejs.org/) — Ambiente de execução
- [Bootstrap](https://getbootstrap.com/) — Estilização
- [TypeScript](https://www.typescriptlang.org/) — Frontend

---

## 📂 Estrutura do Projeto

```plaintext
raiz-do-repositorio/
├── backend/         # API em ASP.NET Core
├── frontend/        # Aplicação Angular
├── docs/            # Documentação
└── README.md        # Este arquivo
```

---

## 🔧 Configuração e Instalação

### Pré-requisitos

Certifique-se de ter as seguintes ferramentas instaladas:

- [Node.js](https://nodejs.org/) (v18+)
- [Angular CLI](https://angular.io/cli)
- [Visual Studio 2022](https://visualstudio.microsoft.com/)
- [Visual Studio Code](https://code.visualstudio.com/)
- [MySQL WorkBench](https://www.mysql.com/products/workbench/)

### Passos para configuração

1. **Clone o repositório:**

   ```bash
   git clone https://github.com/seu-usuario/seu-repositorio.git
   cd seu-repositorio
   ```

2. **Configuração do Frontend:**

   ```bash
   cd frontend
   npm install
   ng serve
   ```

3. **Configuração do Backend:**

   - Abra o projeto `backend` no Visual Studio 2022.
   - Atualize a string de conexão com o banco no arquivo `appsettings.json`.
   - Execute a aplicação.

4. **Banco de Dados:**

   - Certifique-se de que o MySQL está em execução.
   - Execute os scripts SQL necessários para criar o banco e tabelas.

---

## 💻 Como Executar o Projeto

1. Inicie o backend com o Visual Studio ou pelo terminal:
   ```bash
   dotnet run
   ```

2. Inicie o frontend:
   ```bash
   cd frontend
   ng serve
   ```

3. Acesse o projeto no navegador:
   ```
   http://localhost:4200
   ```

---

## 🛠 Funcionalidades

- [x] Autenticação de Usuários (JWT)
- [x] CRUD de Produtos
- [x] CRUD de Categorias
- [x] CRUD de Lançamentos
- [x] CRUD de Usuários
- [x] CRUD de Clientes
- [x] CRUD de Cidades
- [x] CRUD de Empresas
- [x] CRUD de Fornecedores
- [x] CRUD de Contas Contabeis
- [x] CRUD de Grupo Contas Contabeis
- [x] Relatórios Gerenciais
- [x] Interface Responsiva

---

## 🛡️ Testes

### Testes de Unidade (Frontend)

```bash
ng test
```

### Testes de Unidade (Backend)

Execute os testes no Visual Studio ou via terminal:

```bash
dotnet test
```

---

## 🖍️ Licença

Este projeto está sob a licença [MIT](LICENSE).

---

## ✨ Contato

Criado por **Seu Nome** — [seu-email@example.com](mailto:seu-email@example.com)  
[LinkedIn](https://linkedin.com/in/seu-perfil) | [GitHub](https://github.com/seu-usuario)
