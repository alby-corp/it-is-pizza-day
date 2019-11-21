# dotnet --list-sdks
# dotnet new globaljson --sdk-version 2.2.100

dotnet publish -c Release
docker build -t it-is-pizza-day ./

heroku login
heroku container:login

docker tag it-is-pizza-day registry.heroku.com/it-is-pizza-day/web
heroku container:push web -a it-is-pizza-day
heroku container:release web -a it-is-pizza-day


