docker-compose rm -s -f
docker rmi $(docker images --format "{{.Repository}}:{{.Tag}}"|findstr "uasers-api-server")
docker-compose up -d
