#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Group21ProjectMVC/Group21ProjectMVC.csproj", "Group21ProjectMVC/"]
RUN dotnet restore "Group21ProjectMVC/Group21ProjectMVC.csproj"
COPY . .
WORKDIR "/src/Group21ProjectMVC"
RUN dotnet build "Group21ProjectMVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Group21ProjectMVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Group21ProjectMVC.dll"]