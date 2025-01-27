# Documentação do Projeto - Controle de Estoque

## 1. Visão Geral do Projeto
O sistema de **Controle de Estoque** foi desenvolvido para gerenciar peças, permitindo operações de cadastro, consulta, consumo e reposição, garantindo controle eficiente sobre o saldo do estoque. Além disso, o sistema evita consumo acima do disponível, calcula o custo médio das peças e registra logs de erros.

## 2. Tecnologias Utilizadas
**Back-end:**
- ASP.NET Core 8.0
- MediatR (CQRS)
- Dapper
- MySQL
- Injeção de Dependência
- xUnit

**Ferramentas de Controle e Versionamento:**
- Git
- GitHub
- Swagger (Swashbuckle)

## 3. Arquitetura do Sistema
Segue o padrão **Clean Architecture** e **CQRS**, garantindo separação de responsabilidades.

### Camadas do Projeto:
1. **ControleEstoque.Application** (Lógica de negócios com CQRS)
2. **ControleEstoque.Domain** (Entidades e regras de negócio)
3. **ControleEstoque.Infrastructure** (Repositórios com Dapper)
4. **ControleEstoque.API** (Exposição de endpoints REST)
5. **ControleEstoque.Tests** (Testes unitários com xUnit)

## 4. Funcionalidades Implementadas
- **Gerenciamento de Produtos:** Criar, atualizar e excluir produtos.
- **Controle de Estoque:** Consumo e reposição com cálculo de custo médio.
- **Consultas:** Obtenção de produtos por ID e listagem.
- **Registro de Logs:** Captura de erros e armazenamento no banco de dados.

## 5. Endpoints da API


| Método | Rota                           | Descrição                          |
|--------|--------------------------------|------------------------------------|
| GET    | /produtos                      | Lista todos os produtos            |
| GET    | /produtos/{id}                 | Obtém um produto por ID            |
| POST   | /produtos                      | Adiciona um novo produto           |
| PUT    | /produtos/{id}                 | Atualiza um produto existente      |
| DELETE | /produtos/{id}                 | Remove um produto                  |
| PUT    | /produtos/{id}/consumir        | Consome uma quantidade de estoque  |
| PUT    | /produtos/{id}/repor           | Repõe o estoque e atualiza preço   |

## 6. Configuração e Execução do Projeto

### Pré-requisitos:
- .NET 8 SDK
- MySQL Server
- Git

### Configuração do Banco de Dados
Crie a base de dados MySQL:

```sql
CREATE DATABASE ControleEstoque;
USE ControleEstoque;

CREATE TABLE Produtos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    PartNumber VARCHAR(50) NOT NULL UNIQUE,
    Quantidade INT NOT NULL,
    Preco DECIMAL(10,2) NOT NULL,
    CustoTotal DECIMAL(10,2) NOT NULL DEFAULT '0.00'
);

CREATE TABLE LogsErros (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Mensagem TEXT NOT NULL,
    DataErro DATETIME NOT NULL
);
```

### Executando o Projeto
1. Clone o repositório:

   ```bash
   git clone https://github.com/EudesAndrade/ControleEstoque.git
   cd ControleEstoque
   ```

2. Após clonar o projeto:

3. Abra a solução usando o Visual Studio 2022
   - abra o arquivo abaixo ControlEstoque/appsettings.json
   ### Configuração do `appsettings.json`
```json
   - Adicione a sua senha do seu banco local no Password.
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ControleEstoque;User=root;Password=suasenha;"
  }
}
```
4. Execute a aplicação:

  -Clicar no botão START, o sistema abrirá uma página web mostrando todos os endPoints

5. Acesse exemplo: http://localhost:5257/swagger/index.html

6. Clicar no último endPoint GET /teste-connection
   -clicar no botão Try it out
      -clicar no botão Execute
         - API deverá retornar uma mensagem "Conexão com o banco de dados está funcionando!"

   -Depois é só seguir no cadastro de um produto endPont POST/produtos.

7. O Usuário terá as seguintes opções abaixo:
   
| Método | Rota                           | Descrição                          |
|--------|--------------------------------|------------------------------------|
| GET    | /produtos                      | Lista todos os produtos            |
| GET    | /produtos/{id}                 | Obtém um produto por ID            |
| POST   | /produtos                      | Adiciona um novo produto           |
| PUT    | /produtos/{id}                 | Atualiza um produto existente      |
| DELETE | /produtos/{id}                 | Remove um produto                  |
| PUT    | /produtos/{id}/consumir        | Consome uma quantidade de estoque  |
| PUT    | /produtos/{id}/repor           | Repõe o estoque e atualiza preço   |

## 8. Testes
Para rodar os testes:
Na interface do Visual Studio va até a aba Teste ->Executar todos os testes

---

**Desenvolvimento: Eudes Andrade**  

