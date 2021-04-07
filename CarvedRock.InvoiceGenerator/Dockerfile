#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CarvedRock.InvoiceGenerator/CarvedRock.InvoiceGenerator.csproj", "CarvedRock.InvoiceGenerator/"]
RUN dotnet restore "CarvedRock.InvoiceGenerator/CarvedRock.InvoiceGenerator.csproj"
COPY . .
WORKDIR "/src/CarvedRock.InvoiceGenerator"
RUN dotnet build "CarvedRock.InvoiceGenerator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarvedRock.InvoiceGenerator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarvedRock.InvoiceGenerator.dll"]