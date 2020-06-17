# https://hub.docker.com/_/microsoft-dotnet-core
# build image
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /source

EXPOSE 80
EXPOSE 443

COPY *.sln .
COPY TableOfNothing/*.csproj ./TableOfNothing/
RUN dotnet restore -r linux-musl-x64

# copy everything else and build app
COPY TableOfNothing/. ./TableOfNothing/
WORKDIR /source/TableOfNothing
RUN dotnet publish -c release -o /app -r linux-musl-x64 --self-contained true --no-restore /p:PublishTrimmed=true /p:PublishReadyToRun=true

# final stage/image
FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.1-alpine
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["./TableOfNothing"]