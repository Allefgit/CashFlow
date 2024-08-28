# Cash FLow

## Sobre
É uma API com o objetivo de ajudar no **controle de despesas**, então o usuário pode cadastrar, alterar, consultar e excluir despesas, além de poder retirar relatórios sobre o gasto total, sendo um deles em excel e o outro em PDF. 
A API foi desenvolvida em C# usando o frameword **.NET 8**. Como o banco de dados, foi utilizado o **MySQL** e o projeto segue os princípios **Domain-Driver Design**, sendo o guia para a organização do projeto, onde modelamos os problemas reais em códigos.

o modelo da arquitetura da API é **REST (Representational State Transfer)**, utilizando os métodos HTTP para comunicação, que é gerenciada pela interface do **Swagger**, que otimiza a utilização das rotas.

Foi utilizado as seguintes **bibliotecas**: 
- <a href="https://automapper.org/"> Swashbuckle </a>: Auxilia no uso e no gerenciamento das **rotas HTTP** através do Swagger.
- <a href="https://automapper.org/"> AutoMapper </a>: Auxilia no **mapeamento de objetos**.
- <a href="https://fluentassertions.com/introduction"> Fluent Assertion </a>: Auxilia nos **testes unitários**.
- <a href="https://docs.fluentvalidation.net/en/latest/"> Fluent Validation </a>: Auxilia na implementação de **regras de validação**.
- <a href="https://learn.microsoft.com/pt-br/ef/core/"> Entity Framework </a>: Foi o **ORM** utilizado, afim de facilitar as **interações** com o **banco de dados**;
- <a href="https://github.com/ClosedXML/ClosedXML"> ClosedXML </a>: Auxilia na geração de arquivos **XML**;
- <a href="https://docs.pdfsharp.net/"> PDFsharp-MigraDoc </a>: Auxilia na geração de arquivos **PDF**.

### Features

- **Exceptions Tratament**: Há **tratamento de exceções**, uma vez que é essêncial e indispenável para uma boa API.
- **Entity Framework**: Utilização da ORM para maior **desempenho e segurança** ao usar o SQL.
- **Relatórios**: Geração de relatórios em **PDF e Excel** usando bibliotecas gratuitas.
- **S.O.L.I.D.**: O projeto foi construido baseado no modelo SOLID, o que ajuda na **leitura e organização** do projeot, além de **otimizar o desempenho**.

## Como usar

Para usar esse projeto, é necessário...

### Requisitos 

- [.NET](https://dotnet.microsoft.com/pt-br/download) instalado no seu sistema operacional;
- Visual Studio 2022+ ou Visual Studio Code;
- [MySQL Server](https://www.mysql.com/downloads/).

### Instalação

1. Clone este repositório: 
    ```sh
    git clone https://github.com/Allefgit/CashFlow.git
    ``` 

2. Preencha as informações necessárias no arquivo `appsettings.Development.json`
3. Execute a API.

