## 1. Informações sobre o projeto

```
- Banco de dados utilizado: PostgreSQL
- Nome do banco deve ser "desafiojuntoseguros"
- Senha do banco deve ser "postgres"
```

## 2. Migrations

Comando para criar as migrations
```
dotnet ef migrations add nomeMigration
```

Comando para executar as migration
```
dotnet ef database update
```

## 3. Executar o projeto
Comando para execução do projeto
```
dotnet run
```

## 4. Comentário extra
```
Fiz toda a estrutura de autenticação de Token porém por algum motivo de configuração que não consegui descobrir estava dando erro na hora de autenticar o usuário.
Mantive a estrutura no projeto, espero que seja considerado.
```