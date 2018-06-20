FROM microsoft/dotnet:2.0-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /src
COPY RequestSenderSimple/RequestSenderSimple.csproj RequestSenderSimple/
RUN dotnet restore RequestSenderSimple/RequestSenderSimple.csproj
COPY . .
WORKDIR /src/RequestSenderSimple
RUN dotnet build RequestSenderSimple.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish RequestSenderSimple.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RequestSenderSimple.dll"]
