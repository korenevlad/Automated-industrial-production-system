FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/KafkaConsumer/KafkaConsumer.csproj","src/KafkaConsumer/"]
RUN dotnet restore "src/KafkaConsumer/KafkaConsumer.csproj"
COPY . .
WORKDIR "/src/src/KafkaConsumer"
RUN dotnet build "KafkaConsumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KafkaConsumer.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 81
EXPOSE 444

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KafkaConsumer.dll"]
