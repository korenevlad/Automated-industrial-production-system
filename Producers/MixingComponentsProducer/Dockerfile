FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/MixingComponentsProducer/MixingComponentsProducer.csproj","src/MixingComponentsProducer/"]
RUN dotnet restore "src/MixingComponentsProducer/MixingComponentsProducer.csproj"
COPY . .
WORKDIR "/src/src/MixingComponentsProducer"
RUN dotnet build "MixingComponentsProducer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MixingComponentsProducer.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MixingComponentsProducer.dll"]
