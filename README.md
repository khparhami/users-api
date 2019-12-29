# Users API
This repo is a test for creating a simple users api using dotnet core 2.2, mongoDb running in docker.

## prerequisite 
dotnet core 2.2 needs to be installed to run integration tests since it's not dockerized.

## Run api and tests
Simply run ./Run.ps1 on powershell or ./run.sh on linux in root directory. It will do build the project, run unit tests in docker, publishes the app in docker and runs integration tests.

## Documentation
When service is running just visit http://localhost:5000 for documentation

