FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

WORKDIR /app

COPY bin/Debug/netcoreapp3.1 ./
COPY wiseguy.pfx ./certificate/wiseguy.pfx
EXPOSE 5000
EXPOSE 5001

CMD dotnet api.dll