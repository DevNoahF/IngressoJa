# Comando para geração de migrations:

## Criar uma migration (gera arquivo automaticamente)
```
dotnet ef migrations add NomeDaMigration
```

## Aplicar a migration no banco

```
dotnet ef database update
```

antes de dar run é necessario buildar para gerar os arquivos de migração, caso contrario o comando de update não vai encontrar as migrations e não vai atualizar o banco de dados:

```
dotnet build .\priorizzeProject\priorizzeProject.csproj
```

## No arquivo appsettings.json, configurar a string de conexão com o banco de dados:

```
    {
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Database=priorizze;User= SEU USUARIO;Password= SUA SENHA;"
    },
```


## Docker:
Para rodar o docker é necessario passar as variaveis de ambiente no .env

```
dotnet run --project .\priorizzeProject\priorizzeProject.csproj
```

## comando para atualizar o banco de dados depois de adicionar uma entidade ou modificar o modelo:

```
dotnet build C:\Projects\priorizzeProject\priorizzeProject\priorizzeProject.csproj
dotnet ef migrations add "NomeMigration" --project C:\Projects\priorizzeProject\priorizzeProject\priorizzeProject.csproj --startup-project C:\Projects\priorizzeProject\priorizzeProject\priorizzeProject.csproj --output-dir Migrations
dotnet ef database update --project C:\Projects\priorizzeProject\priorizzeProject\priorizzeProject.csproj --startup-project C:\Projects\priorizzeProject\priorizzeProject\priorizzeProject.csproj
```
## comando para limpeza :

```
dotnet clean .\priorizzeProject\priorizzeProject.csproj
```

## comando reset migrations:

```
dotnet ef database drop --force --project .\priorizzeProject\priorizzeProject.csproj --startup-project .\priorizzeProject\priorizzeProject.csproj
Remove-Item -Recurse -Force .\priorizzeProject\Migrations
dotnet ef migrations add InitialCreate --project .\priorizzeProject\priorizzeProject.csproj --startup-project .\priorizzeProject\priorizzeProject.csproj --output-dir Migrations
docker exec -i priorizze_mysql mysql -u root -proot -e "DROP DATABASE IF EXISTS priorizzeproject; CREATE DATABASE priorizzeproject;"
dotnet ef database update --project .\priorizzeProject\priorizzeProject.csproj --startup-project .\priorizzeProject\priorizzeProject.csproj
```


## Padrão para conexao com o banco de dados:

```
jdbc:mysql://localhost:3306/priorizzeproject?user=seu_user&password=sua_senha
```