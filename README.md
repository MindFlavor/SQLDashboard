## Intro

This is a template for a simple SQL Server dashboard. It's written in C# and AngularJS. Main features are:
- [Azure AD](http://azure.microsoft.com/it-it/services/active-directory/) compatible.
- RESTful interface (using [WebAPI 2](http://www.asp.net/web-api)).
- Responsive (thanks to [AngularJS](https://angularjs.org/) and [angular-charts](https://github.com/chinmaymk/angular-charts)).
- Cloud configuration using [Azure table storage](https://msdn.microsoft.com/en-us/magazine/ff796231.aspx).

## Features
Right now it allows you to add/remove connection strings:
![](http://i.imgur.com/EEBe2r3.png)

And then show connection status
![](http://i.imgur.com/8w2OW4u.png)

The drilldown info shows the buffer pool distribution:
![](http://i.imgur.com/HfG6QXZ.png)

The plan cache map:
![](http://i.imgur.com/zGZa9jT.png)

And the db status (from `sys.databases`):
![](http://i.imgur.com/5z65qmL.png)

Other features are planned and will be added at later time.

## How to deploy

1. Copy the `web.config.example` file as `web.config` and configure it specifying your Azure storage connection sting:
```xml
<configuration>
  <appSettings>
    <add key="Azure_Storage_Account_Connection_String" value="DefaultEndpointsProtocol=https;AccountName=<account_name>;AccountKey=<account_key>" />
  </appSettings>
</configuration>
```
 
2. (optional) Enable Azure AD authentication enabling the `[Authorize]` attributes in C# code and configuring the relevant sections in `web.config`:
```xml
<configuration>
  <appSettings>   
	<add key="ida:ClientId" value="<client_id>" />
    <add key="ida:AADInstance" value="https://login.windows.net/" />
    <add key="ida:Domain" value="<domain>" />
    <add key="ida:TenantId" value="<tenant_id>" />
    <add key="ida:PostLogoutRedirectUri" value="<redirect_uri>" />
  </appSettings>
</configuration>
``` 

## How to use
As soon as the application starts it will create the required Azure table. It will be empty so you need to go to the `Server management` section and add one (or more) connection strings:
![](http://i.imgur.com/s8thuw7.png)

Now switch back to SQL Server dashboard (home page) and you should see your instance. If the instance is reachable it will turn green and you will be able to drill down into it.

## References
This project relies upon:

* Angular directives for creating common charts using d3 ([https://github.com/chinmaymk/angular-charts](https://github.com/chinmaymk/angular-charts)).
* D3.js ([http://d3js.org/](http://d3js.org/)).
