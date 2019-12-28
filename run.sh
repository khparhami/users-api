docker-compose rm -s -f
docker rmi $(docker images |grep 'users-api-server')
docker-compose up -d
dotnet test "./tests/zip.api.integration/zip.api.integration.csproj"