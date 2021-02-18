# How to get everything running
- Add environmental variable MyConnectionString pointing to your database, please do not point it to an existing database as this will apply my migrations. This is a code first database
- install dotnet ef using the following command dotnet tool install --global dotnet-ef
- run visual studio application as an administrator as you need access to event viewer for some loggin ilustrations
- run dotnet ef database update to apply the init migration
