FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/CuttingArrayProducer/CuttingArrayProducer.csproj","src/CuttingArrayProducer/"]
RUN dotnet restore "src/CuttingArrayProducer/CuttingArrayProducer.csproj"
COPY . .
WORKDIR "/src/src/CuttingArrayProducer"
RUN dotnet build "CuttingArrayProducer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CuttingArrayProducer.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CuttingArrayProducer.dll"]
