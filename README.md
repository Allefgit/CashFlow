# Cash FLow

## Sobre
É uma API com o objetivo de ajudar no **controle de despesas**, então o usuário pode cadastrar, alterar, consultar e excluir despesas, além de poder retirar relatórios sobre o gasto total, sendo um deles em excel e o outro em PDF. 
A API foi desenvolvida em C# usando o frameword **.NET 8**. Como o banco de dados, foi utilizado o **MySQL** e o projeto segue os princípios **Domain-Driver Design**, sendo o guia para a organização do projeto, onde modelamos os problemas reais em códigos.

o modelo da arquitetura da API é **REST (Representational State Transfer)**, utilizando os métodos HTTP para comunicação, que é gerenciada pela interface do **Swagger**, que otimiza a utilização das rotas.

Foi utilizado as seguintes **bibliotecas**: 
- [Swashbuckle][swashbuckle]: Auxilia no uso e no gerenciamento das **rotas HTTP** através do Swagger.
- [AutoMapper][autoMapper]: Auxilia no **mapeamento de objetos**.
- [Fluent Assertion][fluent-assertion]: Auxilia nos **testes unitários**.
- [Fluent Validation][fluent-validation]: Auxilia na implementação de **regras de validação**.
- [Entity Framework][entity-framework]: Foi o **ORM** utilizado, afim de facilitar as **interações** com o **banco de dados**;
- [ClosedXML][closedXML]: Auxilia na geração de arquivos **XML**;
- [PDFsharp-MigraDoc][pdfsharp-migraDoc]: Auxilia na geração de arquivos **PDF**.

### Features

- **Exceptions Tratament**: Há **tratamento de exceções**, uma vez que é essêncial e indispenável para uma boa API.
- **Entity Framework**: Utilização da ORM para maior **desempenho e segurança** ao usar o SQL.
- **Relatórios**: Geração de relatórios em **PDF e Excel** usando bibliotecas gratuitas.
- **S.O.L.I.D.**: O projeto foi construido baseado no modelo SOLID, o que ajuda na **leitura e organização** do projeot, além de **otimizar o desempenho**.

## Como usar

Para usar esse projeto, é necessário...

### Requisitos 

- [.NET][dot-net-download-link] instalado no seu sistema operacional;
- Visual Studio 2022+ ou Visual Studio Code;
- [MySQL Server][sql-download-link].

### Instalação

1. Clone este repositório: 
    ```sh
    git clone https://github.com/Allefgit/CashFlow.git
    ``` 

2. Preencha as informações necessárias no arquivo `appsettings.Development.json`
3. Execute a API.

<!-- Links -->
[dot-net-download-link]: https://dotnet.microsoft.com/pt-br/download/
[sql-download-link]: https://www.mysql.com/downloads/
[swashbuckle]: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/
[autoMapper]: https://automapper.org/
[fluent-assertion]: https://fluentassertions.com/introduction
[fluent-validation]: https://docs.fluentvalidation.net/en/latest/
[entity-framework]: https://learn.microsoft.com/pt-br/ef/core/
[closedXML]: https://github.com/ClosedXML/ClosedXML
[pdfsharp-migraDoc]: https://docs.pdfsharp.net/
