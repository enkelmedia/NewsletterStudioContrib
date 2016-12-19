The controller is the entry point for the Umbraco Scheduled Tasks.
﻿
Its aim is to be called on a regular basis, connect as an Imap client to retrieve mails, treat their content to mark any EmailTrackingItem or Subscriber as bounced.

In the file Config/umbracoSettings.config, register your task such as: 

```
<scheduledTasks>
    <task log="true" alias="newsletterstudioBounces" interval="840" url="http://localhost:52633/umbraco/api/ImapHandleBounces/handle" />
</scheduledTasks>
```
