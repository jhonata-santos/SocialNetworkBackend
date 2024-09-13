# SocialNetwork API

## Descrição
A **SocialNetwork API** é um microserviço construído em .NET 8, seguindo os princípios da arquitetura de **Microserviços** com designe de software **DDD (Domain-Driven Design)**. O projeto utiliza **Dapper** para consultas de banco de dados MySQL, **AutoMapper** para mapeamento de entidades e DTOs, e **injeção de dependência** configurada de forma modular através da camada **CrossCutting**. 

### Tecnologias principais:
- .NET 8
- C#
- Dapper (para acesso ao banco de dados)
- MySQL
- AutoMapper (para mapeamento de objetos)
- Swagger (para documentação da API)
- Injeção de Dependência
- DDD (Domain-Driven Design) padrão de design de software
- Arquitetura de Microserviços

## Estrutura do Projeto
A organização do projeto segue uma abordagem modular, separando responsabilidades em camadas distintas:

```
Microserviço/
├── Api/
│   ├── Controllers/              # Endpoints da API
├── Application/
│   ├── DTOs/                     # Objetos de Transferência de Dados
│   ├── Interfaces/               # Interfaces de serviços
│   ├── Mappings/                 # Perfis de mapeamento do AutoMapper
│   ├── Services/                 # Implementação de lógica de aplicação
├── Domain/
│   ├── Entities/                 # Entidades de domínio
│   ├── Interfaces/               # Interfaces de repositórios
│   ├── Exceptions/               # Excessões personalizadas do negócio
├── Infrastructure/
│   ├── Data/                     # Implementação dos repositórios e acesso ao banco
│   ├── CrossCutting/             # Configuração de IoC (Inversão de Controle) e injeção de dependência
├── Tests/                        
│   ├── IntegrationTests/         # Testes Integrados
│   ├── UnitTests/                # Testes unitários
```

## Instalação

### Pré-requisitos
- .NET 8 SDK instalado
- MySQL instalado e configurado
- Ferramenta de gerência de pacotes NuGet (ou IDE que gerencie pacotes, como Visual Studio ou Rider)

### Configuração do Banco de Dados
1. Instale e configure o MySQL.
2. Crie um banco de dados com o seguinte comando:
```
CREATE DATABASE SocialNetwork;
```
3. Execute o script SQL localizado em Infrastructure/Data/Scripts para criar as tabelas necessárias.
4. Configure a connection string no arquivo appsettings.json da camada API:
```
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=SocialNetworkDb;User=root;Password=your_password;"
}
```

### Instalando Dependências
Navegue até a pasta raiz do projeto e execute:
```
dotnet restore
```

### Executando a Aplicação
Para executar a aplicação, rode o seguinte comando:
```
dotnet run --project Api
```
Isso irá inicializar a API localmente. Acesse http://localhost:5000/swagger para visualizar a documentação da API gerada pelo Swagger.

### Rodando Testes
Os testes estão localizados na pasta Tests/. Para rodar os testes:
```
dotnet test
```

## Uso
A API possui endpoints para gerenciar usuários. Abaixo estão os principais endpoints disponíveis.

### Endpoints
**GET /Users**
- Descrição: Retorna a lista de todos os usuários.
- Response:
```
[
  {
    "id": 1,
    "name": "John Doe",
    "dateOfBirth": "1995-09-20T00:00:00",
    "cpf": "65456789765",
    "email": "john.doe@email.com"
  }
]
```

**GET /User/{id}**
- Descrição: Retorna os detalhes de um usuário específico.
- Response:
```
{
    "name": "John Doe",
    "dateOfBirth": "1995-09-20T00:00:00",
    "cpf": "65456789765",
    "email": "john.doe@email.com",
    "createAt": "1995-09-20T00:00:00",
    "updateAt": "1995-09-20T00:00:00",
}
```

## Arquitetura
A API foi desenvolvida seguindo a **arquitetura de microserviços**. Cada serviço é responsável por uma função específica dentro do sistema, o que facilita o desenvolvimento e a escalabilidade, além de permitir a independência entre os serviços. 

### Microserviços
Características da Arquitetura de Microserviços:

- **Descentralização**: Cada serviço funciona de forma independente, podendo ser implantado, escalado e mantido de forma autônoma.
- **Desacoplamento**: Separação clara entre lógica de negócio, aplicação e infraestrutura, permitindo que alterações em uma parte do sistema não afetem as demais.
- **Escalabilidade**: Serviços podem ser escalados de forma independente com base na necessidade de carga ou performance.
- **Comunicação via API**: A comunicação entre os microserviços é feita utilizando APIs REST, possibilitando a integração com outras partes do sistema ou com serviços externos.

### Domain-Driven Design (DDD)
O projeto segue os princípios de Domain-Driven Design, separando as responsabilidades em camadas claras:
- API: Exposição dos endpoints REST para comunicação externa.
- Application: Contém a lógica de aplicação, como serviços que orquestram as operações, além de DTOs para comunicação entre camadas.
- Domain: Contém as regras de negócios, entidades, e interfaces de repositórios.
- Infrastructure: 
    - CrossCutting: injeção de dependência configurada de forma modular
    - Data: Implementação de repositórios e serviços de acesso a dados.

### Injeção de Dependência
As dependências são gerenciadas via injeção de dependência, configurada pela camada CrossCutting. O arquivo IoC.cs registra todos os serviços e repositórios, separando-os em módulos.

### AutoMapper
O AutoMapper é usado para converter entidades em DTOs. Perfis de mapeamento estão localizados na pasta Application/Mappings.

### Dapper e MySQL
Dapper é utilizado para consultas no banco de dados MySQL. A camada de Infrastructure/Data contém os repositórios responsáveis pela comunicação com o banco de dados.

## Contribuição
1. Faça um fork do repositório.
2. Crie uma branch com sua feature:
```
git checkout -b feature/nova-feature
```
3. Commit suas mudanças:
```
git commit -m 'Add nova feature'
```
4. Envie para o repositório remoto:
```
git push origin feature/nova-feature
```
5. Abra um Pull Request.

## Licença
Este projeto é licenciado sob a MIT License.