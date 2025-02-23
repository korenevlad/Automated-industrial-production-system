FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/AutoclavingProducer/AutoclavingProducer.csproj","src/AutoclavingProducer/"]
RUN dotnet restore "src/AutoclavingProducer/AutoclavingProducer.csproj"
COPY . .
WORKDIR "/src/src/AutoclavingProducer"
RUN dotnet build "AutoclavingProducer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AutoclavingProducer.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 83
EXPOSE 443

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AutoclavingProducer.dll"]
