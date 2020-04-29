# WebTraffic
A project to help aggregate web server logs and consolidate information.

The plan is to start with support for IIS. Abstract the details into a provider. Then add support for more web servers.

Upcoming goals:
- Add Serilog.
- Add API calls to a service that maps IP addresses to GEO location.
- Add a front-end to so access logs are viewable. This goal is more difficult because it also requires setting up the sign-in gateway to protect the UI & APIs from unauthorized access. Setting up the gatewa is a project all on its own.

To try this out you need .NET 5. Rename the app settings template files and add your connection string. Note that you need to add a SQL account on your server with read/write permission. Then run the migrations to create the database. 
