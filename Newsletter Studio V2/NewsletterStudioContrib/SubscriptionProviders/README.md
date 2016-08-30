Subscription providers
=======================

A subscription provider can be used to fetch subscribers from custom data sources ie. custom database tables, files on disk, web services or what ever that your code can talk to. This repository contains some examples of providers that you can use "as is" or use as a starting-point and customize it in you own way.

## Example projects

### TeaCommerce
---------
A basic provider that fetches all valid customers from the custom tables created by TeaCommerce.

### uCommerce
---------
A basic provider that fetches all valid customers from the custom tables created by uCommerce

### uWebshop
--------
No need for a custom provider as all customers are stored as Umbraco members.

### DownloadSubscriptionProvider
----------------------------
This class demonstrates a simple approach do implement a custom subscription provider that downloads the list of emails from an external web-page. Probably not a real world scenario so think of this class more as a demonstration of what can be done.
 
 

## Configuration
After your providers is created you need to configure Newsletter Studio to use it. Go to /config/newsletterStudio.config and look for the "subscriptionProviers"-element.

To configure the DownloadSubscriptionProvider from the examples above, add a element like this:

``` xml 
<provider name="CustomProvider" type="NewsletterStudioContrib.SubscriptionProviders.DownloadSubscriptioProvider, NewsletterStudioContrib" /> 
```

The logic is:

type="Any.NameSpace.That.You.Use.ClassName, AssemblyName"

In the example with the DownloadSubscriptionProvider

**The assembly name is**: NewsletterStudioContrib

**The namespace**: NewsletterStudioContrib.SubscriptionProviders

**Class name**: DownloadSubscriptioProvider
