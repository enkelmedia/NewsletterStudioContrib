# Newsletter Studio - Extensions demo site
This Umbraco-site contains examples of extension points in Newsletter Studio. You can read more about the possibilities in our [documentation](https://www.newsletterstudio.org/documentation/package/16.0.0/getting-started/installation/).

Clone/download the repository and start the website using

```bash
dotnet run
```

The website is configured with unattended install and this default backoffice-user will be created:

Email: admin@demo.com
Pass: 1234567890

**NOTE!** Since a new Umbraco-database is created when you run the website the first time, you will need to go to the `Email`-section, then `Administration`, edit the default workspace and activate the extensions to see them in the UI. This is required to see extensions like `Themes`, `Recipient List Providers`, `Merge Field Providers` and more.