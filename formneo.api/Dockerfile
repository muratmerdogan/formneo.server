FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["formneo.api/formneo.api.csproj", "formneo.api/"]
COPY ["formneo.service/formneo.service.csproj", "formneo.service/"]
COPY ["formneo.repository/formneo.repository.csproj", "formneo.repository/"]
COPY ["formneo.core/formneo.core.csproj", "formneo.core/"]
COPY ["formneo.ticket/formneo.ticket.csproj", "formneo.ticket/"]
COPY ["formneo.workflow/formneo.workflow.csproj", "formneo.workflow/"]
RUN dotnet restore "formneo.api/formneo.api.csproj"
COPY . .
WORKDIR "/src/formneo.api"
RUN dotnet build "formneo.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "formneo.api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "formneo.api.dll"]