FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/ReportManager/ReportManager.csproj","src/ReportManager/"]
RUN dotnet restore "src/ReportManager/ReportManager.csproj"
COPY . .
WORKDIR "/src/src/ReportManager"
RUN dotnet build "ReportManager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReportManager.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 87

EXPOSE 444

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportManager.dll"]
