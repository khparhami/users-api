FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# Copy everything and build
COPY . ./

RUN dotnet restore "./src/zip.api/zip.api.csproj"
RUN dotnet restore "./tests/zip.api.tests/zip.api.tests.csproj"
RUN dotnet test "./tests/zip.api.tests/zip.api.tests.csproj"
RUN dotnet publish "./src/zip.api/zip.api.csproj" -c Release -o out


FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/src/zip.api/out .
ENTRYPOINT ["dotnet", "zip.api.dll"]