FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
ENV SimpleProperty="hello-from-BASE-dockerfile"

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CarvedRock.Api/CarvedRock.Api.csproj", "CarvedRock.Api/"]
RUN dotnet restore "CarvedRock.Api/CarvedRock.Api.csproj"
COPY . .
WORKDIR "/src/CarvedRock.Api"
RUN dotnet build "CarvedRock.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarvedRock.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV SimpleProperty="hello-from-FINAL-dockerfile"
ENTRYPOINT ["dotnet", "CarvedRock.Api.dll"]
