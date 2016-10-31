Custom Bounce Management
=======================

If you want to handle bounces outside of Newsletter Studio this simple example shows how to add your own bounce. When this is done the system will both set the specific subscriber as a bounced and show this receiver as bounced in the analytics-section for the e-mail.

## Example
This is how this helper could be used

``` csharp 
var helper = new NewsletterBounceHelper();
helper.AddNewHardBounce("MyEmail@domain.com","Message for the UI and the end user");
```

or if you have a list this is more efficient, if the list is bigger than 200, call the method in "chunks" to avoid memory issues.

``` csharp 
var list = new List<string>();
list.Add("MyEmail@domain.com");
list.Add("MyOtherEmail@domain.com");

var helper = new NewsletterBounceHelper();
helper.AddNewHardBounce(list,"Message for the UI and the end user");
```



