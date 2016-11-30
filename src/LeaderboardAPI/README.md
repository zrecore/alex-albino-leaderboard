# Leaderboard Application

This Leaderboard Application is built upon .Net Core 1.1 (C#)
Written by Alex Albino <webmaster@alexventure.com>


## Database setup (SQLite3)

Navigate to `src/LeaderboardAPI` and run the following:

```
dotnet ef database update
```

To create a migration after updating a model class, run the following (replace MIGRATION_NAME with an actual name):

```
dotnet ef migrations add MIGRATION_NAME
```

## Run 

Navigate to `src/LeaderboardAPI` and run the following:

```
dotnet run
``` 

You can now send HTTP requests (GET/PUT/POST/DELETE) to [http://localhost:5000/records](http://localhost:5000/records)