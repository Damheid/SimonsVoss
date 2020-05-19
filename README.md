# SimonsVoss
Coding case for SimonsVoss

# Build Instruction
From the root directory

## Registration Library
```
cd RegistrationLibrary
dotnet build
```

## License Signature Service
```
cd LicenseSignatureService
dotnet build
dotnet run
```

## Registration API
```
cd RegistrationServiceApi
dotnet build
dotnet run (optional parameter SignatureServiceAddress. defaults is https://localhost:5002)
```

## Registration Portal
```
cd RegistrationPortal
dotnet build
dotnet run (optional parameter ApiAddress, default is https://localhost:5001)
```

# Docker Instructions
From the root folder run the command, both Registration API and Signature Services will be built.
```
docker-compose -f "docker-compose.yml" up -d --build
```