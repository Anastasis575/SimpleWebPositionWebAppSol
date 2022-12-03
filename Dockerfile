#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./SimpleWebPositionApp/SimpleWebPositionApp.csproj", "./SimpleWebPositionApp/"]
RUN dotnet restore "./SimpleWebPositionApp/SimpleWebPositionApp.csproj"
COPY . .
WORKDIR "/src/SimpleWebPositionApp"
RUN dotnet build "./SimpleWebPositionApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleWebPositionApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY ["./SimpleWebPositionApp/Data","./Data"]
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet","SimpleWebPositionApp.dll"]