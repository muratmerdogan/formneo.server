#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5000



FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["vesa.api/vesa.api.csproj", "vesa.api/"]
COPY ["vesa.service/vesa.service.csproj", "vesa.service/"]
COPY ["vesa.repository/vesa.repository.csproj", "vesa.repository/"]
COPY ["vesa.core/vesa.core.csproj", "vesa.core/"]
COPY ["vesa.workflow/vesa.workflow.csproj", "vesa.workflow/"]
RUN dotnet restore "vesa.api/vesa.api.csproj"
COPY . .
WORKDIR "/src/vesa.api"
RUN dotnet build "vesa.api.csproj" -c Release -o /app/build


WORKDIR "/src/vesa.api"
FROM build AS publish
RUN dotnet publish "vesa.api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .


ENTRYPOINT ["dotnet", "vesa.api.dll", "--server.urls", "http://*:5000"]
