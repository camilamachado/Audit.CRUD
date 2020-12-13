# Audit.CRUD

Audit.CRUD é uma biblioteca para .NET que oferece uma interface para persistência de **logs de auditoria** no Elasticsearch. Seu principal objetivo é auxiliar desenvolvedores a aplicar logs de auditoria de forma ágil e padronizada, utilizando tecnologias capazes de suportar e analizar grandes quantidades de dados.

## Características
- Configuração simples e interface enxuta.
- Estrutura dos dados de log de auditoria são baseados no modelo W7 da provêniencia de dados.
- Os logs de auditoria são armazenados no Elasticsearch, permitindo que você consulte e execute análises sobre esses dados.

## Instalação

1. O Audit.CRUD está disponível no NuGet. Instale o pacote em seu projeto.

```powershell
Install-Package Audit.CRUD -version 1.0.0
```

2. No método `ConfigureServices` na classe `Startup.cs`. Adicione o método `UseAuditCRUD` passando por parâmetro o objeto `ElasticsearchSettings`.

```csharp
using Audit.CRUD.Configurations;
using Audit.CRUD.Settings;
```
```csharp
		public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
        
		public void ConfigureServices(IServiceCollection services)
		{
			// Setting AuditCRUD
			var elasticsearchSettings = this.Configuration.GetSection("ElasticsearchSettings").Get<ElasticsearchSettings>();

			services.UseAuditCRUD(elasticsearchSettings);
		}
```
No arquivo `appsettings.json` passe os dados referente ao **nome do index** e **URI** de onde o Elasticsearch está hospedado.

Pronto! O Audit.CRUD está configurado, agora basta aplicar os logs nos pontos necessários.

```json
  "ElasticsearchSettings": {
    "Uri": "http://localhost:9200/",
    "Index": "audit-crud-logs"
  }
```

## Utilização

1. Injete a interface `IAuditCRUD` no construtor da classe que deseja gerar os logs de auditoria. Conforme exemplo abaixo:
```csharp
using Audit.CRUD;
```
```csharp
public class Student
{
    private readonly IAuditCRUD _auditCRUD;

    public Student(IAuditCRUD auditCRUD)
    {
        _auditCRUD = auditCRUD;
    }
}
```

2. Utilize o método da interface `IAuditCRUD` de acordo a ação realizada.

|    Método    |         Quando usar?        |
|:------------:|:---------------------------:|
| ActionCreate | Ao criar uma entidade.      |
| ActionRead   | Ao visualizar uma entidade. |
| ActionUpdate | Ao atualizar uma entidade.  |
| ActionDelete | Ao excluir uma entidade.    |

 ### Exemplos abaixo:

1. Ao realizar a ação de criar uma entidade use o método `ActionCreate`. Exemplo:
```csharp
public class Student
{
    private readonly IAuditCRUD _auditCRUD;
    private readonly IStudentRepository _studentRepository;

    public Student(IAuditCRUD auditCRUD, IStudentRepository studentRepository)
    {
        _auditCRUD = auditCRUD;
        _studentRepository = studentRepository;
    }

    public CreateStudent()
    {
        ...

        await _auditCRUD.ActionCreate(
                        eventName: "CreateStudent",
                        user: new UserAuditCRUD(request.UserId, request.UserName, request.Email),
                        location: typeof(Student).Namespace,
                        ipAddress: request.IpAddress,
                        currentEntity: newStudent);

        ...
    }
}
```
Note que o `ActionCreate` recebe alguns parâmetros, são os dados para gerar o log de auditoria.

| Parâmetro     | Tipo          | Descrição                             | Obrigatório | Exemplos                                      |
|---------------|---------------|---------------------------------------|-------------|-----------------------------------------------|
| eventName     | string        | Nome do evento que aconteceu.         | sim         | CreateStudent, CreateUser, CreateOrganization |
| user          | UserAuditCRUD | Dados do usuário que disparou a ação. | sim         | -                                             |
| location      | string        | Onde a ação aconteceu.                | sim         | Namespace da classe, Nome do microserviço     |
| ipAddress     | string        | Qual IP realizou a ação.              | sim         | 127.0.0.1, 192.168.128.3                      |
| currentEntity | object        | Entidade criada.                      | sim         | Student, User, Organization                   |
| reason        | string        | Motivo pelo qual entidade foi criado.        | não         | Aluno importado do sistema antigo|

2. Ao realizar a ação de atualizar uma entidade use o método `ActionUpdate`. Exemplo:
```csharp
public class Student
{
    private readonly IAuditCRUD _auditCRUD;
    private readonly IStudentRepository _studentRepository;

    public Student(IAuditCRUD auditCRUD, IStudentRepository studentRepository)
    {
        _auditCRUD = auditCRUD;
        _studentRepository = studentRepository;
    }

    public UpdateStudent()
    {
        ...

        await _auditCRUD.ActionUpdate(
                        eventName: "UpdateStudent",
                        user: new UserAuditCRUD(request.UserId, request.UserName, request.Email),
                        location: typeof(Student).Namespace,
                        ipAddress: request.IpAddress,
                        currentEntity: currentStudent,
                        oldEntity: oldStudent);

        ...
    }

}
```

Note que o `ActionUpdate` recebe alguns parâmetros, são os dados para gerar o log de auditoria.

| Parâmetro     | Tipo          | Descrição                             | Obrigatório | Exemplos                                      |
|---------------|---------------|---------------------------------------|-------------|-----------------------------------------------|
| eventName     | string        | Nome do evento que aconteceu.         | sim         | UpdateStudent, UpdateUser, UpdateOrganization |
| user          | UserAuditCRUD | Dados do usuário que disparou a ação. | sim         | -                                             |
| location      | string        | Onde a ação aconteceu.                | sim         | Namespace da classe, Nome do microserviço     |
| ipAddress     | string        | Qual IP realizou a ação.              | sim         | 127.0.0.1, 192.168.128.3                      |
| currentEntity | object        | Entidade atualizada.                  | sim         | Student, User, Organization                   |
| oldEntity     | object        | Entidade antes da atualização.        | sim         | Student, User, Organization                   |
| reason        | string        | Motivo pelo qual entidade foi atualizada.        | não         | Nome do aluno estava incorreto|

3. Ao realizar a ação de visualizar uma entidade use o método `ActionRead`. Exemplo:
```csharp
public class Student
{
    private readonly IAuditCRUD _auditCRUD;
    private readonly IStudentRepository _studentRepository;

    public Student(IAuditCRUD auditCRUD, IStudentRepository studentRepository)
    {
        _auditCRUD = auditCRUD;
        _studentRepository = studentRepository;
    }

    public GetStudent()
    {
        ...

        await _auditCRUD.ActionRead(
                        eventName: "GetStudent",
                        user: new UserAuditCRUD(query.UserId, query.UserName, query.Email),
                        location: typeof(Student).Namespace,
                        ipAddress: query.IpAddress,
                        currentEntity: student);

        ...
    }

}
```

Note que o `ActionRead` recebe alguns parâmetros, são os dados para gerar o log de auditoria.

| Parâmetro     | Tipo          | Descrição                             | Obrigatório | Exemplos                                      |
|---------------|---------------|---------------------------------------|-------------|-----------------------------------------------|
| eventName     | string        | Nome do evento que aconteceu.         | sim         | GetStudent, GetUser, GetOrganization |
| user          | UserAuditCRUD | Dados do usuário que disparou a ação. | sim         | -                                             |
| location      | string        | Onde a ação aconteceu.                | sim         | Namespace da classe, Nome do microserviço     |
| ipAddress     | string        | Qual IP realizou a ação.              | sim         | 127.0.0.1, 192.168.128.3                      |
| currentEntity | object        | Entidade visualizada.                  | sim         | Student, User, Organization                   |
| reason        | string        | Motivo pelo qual entidade foi visualizada.        | não         | Consultando status do aluno |

4. Ao realizar a ação de excluir uma entidade use o método `ActionDelete`. Exemplo:
```csharp
public class Student
{
    private readonly IAuditCRUD _auditCRUD;
    private readonly IStudentRepository _studentRepository;

    public Student(IAuditCRUD auditCRUD, IStudentRepository studentRepository)
    {
        _auditCRUD = auditCRUD;
        _studentRepository = studentRepository;
    }

    public DeleteStudent()
    {
        ...

        await _auditCRUD.ActionDelete(
                    eventName: "DeleteStudent",
                    user: new UserAuditCRUD(request.UserId, request.UserName, request.Email),
                    location: typeof(Student).Namespace,
                    ipAddress: request.IpAddress,
                    reason: request.Reason,
                    oldEntity: student);

        ...
    }

}
```

Note que o `ActionDelete` recebe alguns parâmetros, são os dados para gerar o log de auditoria.

| Parâmetro     | Tipo          | Descrição                             | Obrigatório | Exemplos                                      |
|---------------|---------------|---------------------------------------|-------------|-----------------------------------------------|
| eventName     | string        | Nome do evento que aconteceu.         | sim         | DeleteStudent, DeleteUser, DeleteOrganization |
| user          | UserAuditCRUD | Dados do usuário que disparou a ação. | sim         | -                                             |
| location      | string        | Onde a ação aconteceu.                | sim         | Namespace da classe, Nome do microserviço     |
| ipAddress     | string        | Qual IP realizou a ação.              | sim         | 127.0.0.1, 192.168.128.3                      |
| oldEntity     | object        | Entidade excluída.        | sim         | Student, User, Organization                   |
| reason        | string        | Motivo pelo qual entidade foi excluída.        | não         | Aluno não pretende retornar ao estudos |
