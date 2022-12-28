#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FE/FE.csproj", "Libraryapp/FE/"]
COPY ["BL/BL.csproj", "Libraryapp/BL/"]
COPY ["DAL/DAL.csproj", "Libraryapp/DAL/"]
COPY ["Infrastructure.EFCore/Infrastructure.EFCore.csproj", "Libraryapp/Infrastructure.EFCore/"]
COPY ["Infrastructure/Infrastructure.csproj", "Libraryapp/Infrastructure/"]
RUN dotnet restore "Libraryapp/FE/FE.csproj"
COPY . .
WORKDIR "/src/Libraryapp"
COPY . .
RUN dotnet build "FE/FE.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FE/FE.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8000
ENV ASPNETCORE_URLS=http://+:8000
ENTRYPOINT ["dotnet", "FE.dll"]