docker-compose rm -s -f
docker rmi $(docker images --format "{{.Repository}}:{{.Tag}}"|findstr "users-api-server")
docker-compose up -d
