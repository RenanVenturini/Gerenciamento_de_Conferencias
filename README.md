# Sistema de Gerenciamento de Conferências

Este projeto é um sistema de gerenciamento de conferências projetado para ajudar a organizar uma conferência de programação. Ele distribui diversas palestras em várias trilhas, respeitando os horários de início e término das sessões matutinas e vespertinas, além de garantir que o evento de networking ocorra no horário adequado.

## Configuração do Banco de Dados

A API utiliza um banco de dados SQL Server. Certifique-se de configurar corretamente a conexão com o banco de dados no arquivo `appsettings.json` antes de iniciar a API. 

```json
{
  "ConnectionStrings": {
    "PredictWeatherConnection": "Data Source=SQLEXPRESS;Initial Catalog=PredictWeather;User ID=usuario;password=senha;Encrypt=True;Trust Server Certificate=True"
  }
}
```

Substitua os valores em `"Data Source"`, `"Initial Catalog"`, `"User ID"` e `"password"` pelos detalhes de conexão do seu banco de dados SQL Server.

## Endpoints

### Conferência

#### Listagem de Conferências

- **Descrição**: Retorna uma lista de Conferências.
- **Verbo HTTP**: GET
- **Endpoint**: `/listar`

#### Cadastro de Conferência

- **Descrição**: Cadastra uma nova Conferência.
- **Verbo HTTP**: POST
- **Endpoint**: `/adicionar`
- **Corpo da Requisição**: 
```json
{
  "nome": "string",
  "local": "string"
}
```

#### Detalhes da Conferência

- **Descrição**: Retorna os detalhes de uma Conferência específica.
- **Verbo HTTP**: GET
- **Endpoint**: `/Conferencia/{id}` (onde {id} é o identificador da Conferência)

#### Atualização de uma Conferência

- **Descrição**: Atualiza os dados de uma Conferência existente.
- **Verbo HTTP**: PUT
- **Endpoint**: `/atualizar/{id}` (onde {id} é o identificador da Conferência)
- **Corpo da Requisição**: 
```json
{
  "id": 0,
  "nome": "string",
  "local": "string"
}
```

#### Remoção de uma Conferência

- **Descrição**: Remove os detalhes de uma Conferência.
- **Verbo HTTP**: DELETE
- **Endpoint**: `/Excluir/{id}` (onde {id} é o identificador da Conferência)

### Palestra

#### Listagem de Palestras

- **Descrição**: Retorna uma lista de Palestras.
- **Verbo HTTP**: GET
- **Endpoint**: `/listar`

#### Cadastro de Palestra

- **Descrição**: Cadastra uma nova Palestra.
- **Verbo HTTP**: POST
- **Endpoint**: `/adicionar`
- **Corpo da Requisição**: 
```json
{
  "nome": "string",
  "inicio": "string",
  "duracao": "string",
  "trilhaId": 0
}
```

#### Detalhes da Palestra

- **Descrição**: Retorna os detalhes de uma Palestra específica.
- **Verbo HTTP**: GET
- **Endpoint**: `/Palestra/{id}` (onde {id} é o identificador da Palestra)

#### Atualização de uma Palestra

- **Descrição**: Atualiza os dados de uma Palestra existente.
- **Verbo HTTP**: PUT
- **Endpoint**: `/atualizar/{id}` (onde {id} é o identificador da Palestra)
- **Corpo da Requisição**: 
```json
{
  "id": 0,
  "nome": "string",
  "inicio": "string",
  "duracao": "string",
  "trilhaId": 0
}
```

#### Remoção de uma Palestra

- **Descrição**: Remove os detalhes de uma Palestra.
- **Verbo HTTP**: DELETE
- **Endpoint**: `/Excluir/{id}` (onde {id} é o identificador da Palestra)

### Trilha

#### Listagem de Trilhas

- **Descrição**: Retorna uma lista de Trilhas.
- **Verbo HTTP**: GET
- **Endpoint**: `/listar`

#### Cadastro de Trilha

- **Descrição**: Cadastra uma nova Trilha.
- **Verbo HTTP**: POST
- **Endpoint**: `/adicionar`
- **Corpo da Requisição**: 
```json
{
  "conferenciaId": 0,
  "nome": "string",
  "networkingEvent": {
    "inicio": "string"
}
```

#### Detalhes da Trilha

- **Descrição**: Retorna os detalhes de uma Trilha específica.
- **Verbo HTTP**: GET
- **Endpoint**: `/Conferencia/{id}` (onde {id} é o identificador da Trilha)

#### Atualização de uma Trilha

- **Descrição**: Atualiza os dados de uma Trilha existente.
- **Verbo HTTP**: PUT
- **Endpoint**: `/atualizar/{id}` (onde {id} é o identificador da Trilha)
- **Corpo da Requisição**: 
```json
{
  "id": 0,
  "nome": "string",
  "networkingEvent": {
    "id": 0,
    "inicio": "string"
  }
}
```

#### Remoção de uma Trilha 

- **Descrição**: Remove os detalhes de uma Trilha.
- **Verbo HTTP**: DELETE
- **Endpoint**: `/Excluir/{id}` (onde {id} é o identificador da Trilha)

## Explicação de Decisões de Design e Implementação

### Padrão de Arquitetura MVC

Este projeto segue o padrão de arquitetura MVC (Model-View-Controller) para organizar os componentes da aplicação em camadas separadas de responsabilidade, facilitando a manutenção e escalabilidade do projeto.

### Padrão de Arquitetura DDD

Os componentes da aplicação são organizados de acordo com os princípios do Domain-Driven Design (DDD), proporcionando uma melhor modelagem do domínio do problema e uma arquitetura mais flexível e orientada ao negócio.

### Injeção de Dependência

Utilizamos a injeção de dependência para injetar as dependências necessárias nos controladores e serviços, tornando o código mais modular, testável e de fácil manutenção.

### Utilização do AutoMapper

Foi utilizado o AutoMapper para mapear os objetos de request e response para as entidades do domínio.

## Sugestões de Melhorias e Avanços Futuros

1. Implementar testes automatizados para garantir a qualidade e robustez do código.
2. Adicionar tratamento de erros mais robusto e consistente em toda a aplicação.
3. Implementar logging para registrar eventos e informações importantes da aplicação.

## Contribuindo

Sinta-se à vontade para contribuir com melhorias ou correções neste projeto. Basta enviar um pull request com suas alterações.
