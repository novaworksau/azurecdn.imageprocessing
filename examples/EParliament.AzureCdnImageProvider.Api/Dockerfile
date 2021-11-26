ARG RUNTIME_IMAGE_TAG=6.0
ARG SDK_IMAGE_TAG=6.0
ARG HTTP_PORT=8100
ARG HTTPS_PORT=8443


FROM mcr.microsoft.com/dotnet/aspnet:${RUNTIME_IMAGE_TAG} AS base
WORKDIR /app
EXPOSE ${HTTP_PORT}
EXPOSE ${HTTPS_PORT}

RUN groupadd -g 1000 -o appuser
RUN useradd -m -u 1000 -g 1000 -o -s /bin/bash appuser

RUN apt-get update \
	&& apt-get upgrade -y \
	&& apt-get clean \
    && rm -rf /var/lib/apt/lists/*

FROM mcr.microsoft.com/dotnet/sdk:${SDK_IMAGE_TAG} AS build
WORKDIR /src
COPY Directory.Build.targets .
COPY src/ src/
COPY examples/EParliament.AzureCdnImageProvider.Api/ examples/EParliament.AzureCdnImageProvider.Api/ 
RUN dotnet restore "examples/EParliament.AzureCdnImageProvider.Api/EParliament.AzureCdnImageProvider.Api.csproj"
WORKDIR "/src/examples/EParliament.AzureCdnImageProvider.Api"
RUN dotnet build "EParliament.AzureCdnImageProvider.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EParliament.AzureCdnImageProvider.Api.csproj" -c Release -o /app/publish

FROM base AS final
USER appuser
WORKDIR /app
COPY --from=publish --chown=$GID:$UID /app/publish .
ENTRYPOINT ["dotnet", "EParliament.AzureCdnImageProvider.Api.dll"]