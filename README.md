# Carved Rock - Docker 
This repo contains some simple code that illustrates .NET applications targeting 
Docker containers.

## Initial Creation Notes
The project was created by using the ASP.NET Web API (.NET 5 - C#) template and including Docker Support.

It also added the `.vscode` folder to support running and debugging the project within VS Code.

No other initial changes were made for the initial commit of the repo.


## GitHub Actions
This repo contains a `yaml` file that defines a GitHub
Action that will build the container image and push it to hub.docker.com when changes 
are made to the `main` branch (either by pull request or direct push).  Changes to readme, vscode, and 
gitignore will not trigger this action.

It uses the https://github.com/docker/build-push-action action.