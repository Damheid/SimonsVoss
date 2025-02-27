FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://*:$PORT

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["RegistrationLibrary/RegistrationLibrary.csproj", "RegistrationLibrary/"]
RUN dotnet restore "RegistrationLibrary/RegistrationLibrary.csproj"
COPY ["RegistrationLibrary", "./RegistrationLibrary"]
RUN dotnet build "RegistrationLibrary/RegistrationLibrary.csproj"

COPY ["LicenseSignatureService/LicenseSignatureService.csproj", "LicenseSignatureService/"]
RUN dotnet restore "LicenseSignatureService/LicenseSignatureService.csproj"
COPY ["LicenseSignatureService", "./LicenseSignatureService"]
RUN dotnet build "LicenseSignatureService/LicenseSignatureService.csproj"

COPY ["RegistrationServiceApi/RegistrationServiceApi.csproj", "RegistrationServiceApi/"]
RUN dotnet restore "./RegistrationServiceApi/RegistrationServiceApi.csproj"
COPY ["RegistrationServiceApi", "./RegistrationServiceApi"]
RUN dotnet build "RegistrationServiceApi/RegistrationServiceApi.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR "/src/RegistrationServiceApi/."
RUN dotnet publish "RegistrationServiceApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RegistrationServiceApi.dll", "SignatureServiceAddress=https://simonsvoss-signature-service.herokuapp.com"]
