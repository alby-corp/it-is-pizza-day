FROM microsoft/dotnet:2.1-aspnetcore-runtime
COPY ./bin/Release/netcoreapp2.1/publish/ .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet ./ItIsPizzaDay.Server.dll