# WebTraffic
A project to help aggregate web server logs and consolidate information.

A project to help aggregate web server logs and consolidate information.

The plan is to start with support for IIS. Abstract the details into a provider. Then add support for more web servers.

Upcoming goals:

Add API calls to a service that maps IP addresses to GEO location.
Add a front-end to so access logs are viewable. This goal is more difficult because it also requires setting up the sign-in gateway to protect the UI & APIs from unauthorized access. Setting up the gatewa is a project all on its own.

