## 1. Informa��es sobre o projeto

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
Comando para execu��o do projeto
```
dotnet run
```

## 4. Coment�rio extra
```
Fiz toda a estrutura de autentica��o de Token por�m por algum motivo de configura��o que n�o consegui descobrir estava dando erro na hora de autenticar o usu�rio.
Mantive a estrutura no projeto, espero que seja considerado.
```