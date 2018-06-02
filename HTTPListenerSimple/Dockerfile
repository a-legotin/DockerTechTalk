FROM microsoft/dotnet:2.0-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /src
COPY HTTPListenerSimple/HTTPListenerSimple.csproj HTTPListenerSimple/
RUN dotnet restore HTTPListenerSimple/HTTPListenerSimple.csproj
COPY . .
WORKDIR /src/HTTPListenerSimple
RUN dotnet build HTTPListenerSimple.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish HTTPListenerSimple.csproj -c Release -o /app

FROM base AS final
WORKDIR /app

EXPOSE 777

COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HTTPListenerSimple.dll"]
