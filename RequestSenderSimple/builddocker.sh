#!/bin/bash 
dotnet restore
dotnet build
dotnet publish
docker build -t request-serder-simple .
