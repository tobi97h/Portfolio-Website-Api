Setting up secrets for local dev

```
dotnet user-secrets init
dotnet user-secrets set "PF:GithubToken" ""
dotnet user-secrets set "PF:GithubUser" ""
dotnet user-secrets set "PF:ValidSourceCodeFiles" ""
dotnet user-secrets set "PF:DBConnectionString" ""
dotnet user-secrets set "PF:GhostToken" ""
dotnet user-secrets set "PF:GhostUrl" ""
```