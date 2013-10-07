Factory
=======================

In Newsletter Studio version 1.4.0 we introduced the new data layer and with that a new factor interface. This interface is found in NewsletterStudio.Core.Interfaces.INewsletterStudioGlobalFactory and this interface is used trough out in Newsletter Studio do resolve dependencies.

If you for example would like to implement your own Url-encoder, parameter-compression, json-serializer or change the default data layer - this is where you can do this.

The files in this folder are:

DefaultNewsletterStudioGlobalFactory.cs
---------------------------------------
The default factory that shippes with Newlsetter Studio

INewsletterStudioGlobalFactory.cs
---------------------------------------
The interface that is used in Newsletter Studio

SuperJsonEncoder.cs
-------------------
An example of a custom json encoder that could be used.

BootstrapNewsletterStudio.cs
----------------------------
A simple demo on how to change the default factory.






