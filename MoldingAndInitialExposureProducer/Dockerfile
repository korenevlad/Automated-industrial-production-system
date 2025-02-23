FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/MoldingAndInitialExposureProducer/MoldingAndInitialExposureProducer.csproj","src/MoldingAndInitialExposureProducer/"]
RUN dotnet restore "src/MoldingAndInitialExposureProducer/MoldingAndInitialExposureProducer.csproj"
COPY . .
WORKDIR "/src/src/MoldingAndInitialExposureProducer"
RUN dotnet build "MoldingAndInitialExposureProducer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MoldingAndInitialExposureProducer.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 82
EXPOSE 443

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MoldingAndInitialExposureProducer.dll"]
