# Newsletter Studio - Extentions demo site
This Umbraco-site contains examples of extensions and extension points for Newsletter Studio.

Clone/download the repository and start the website using

```
dotnet run
```

The website is configured with unattended install and this default backoffice-user will be created:

Email: admin@demo.com
Pass: 1234567890

**NOTE!** Since the Umbraco-database is created when you run the website the first time most of the extensions need to be activated/allowed in Administration -> Workspace. This goes for things like `Themes`, `Recipient List Providers`, `Merge Field Providers`.