# Project

This project contains a backend API that gives you various statistics of your github profile, as well as your ghost blog. 

This project uses https://github.com/tobi97h/AspSecretsProvider, you can set the following secrets via the cli for development:

```
dotnet user-secrets init
dotnet user-secrets set "PF:GithubToken" ""
dotnet user-secrets set "PF:GithubUser" ""
dotnet user-secrets set "PF:ValidSourceCodeFiles" ""
dotnet user-secrets set "PF:DBConnectionString" ""
dotnet user-secrets set "PF:GhostToken" ""
dotnet user-secrets set "PF:GhostUrl" ""
dotnet user-secrets set "PF:DroneToken" ""
```

The required tokens are the simple api access tokens that the platforms provide. 

! This api fetches all information once initially on startup, the interval at which the stats are fetched depend on the backup jobs that
stop and start the containers.!