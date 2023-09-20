A webhook is a way for a application to provide callbacks for 3-part applications.

When an event occurs, the source application typically triggers an http POST call to a pre-configured external URL and wraps the data from the triggered event in the request's payload.

This approach allows external applications to respond to events via the standard WebAPI interface.
This pattern is used by most large companies and I will use Github (listen to new pull request or issue was created) as a template and integrate this functionality into the NetCore (c#) backend application.

https://www.christianfindlay.com/blog/asp-dotnet-core-minimal-webhooks
https://dev.to/damikun/integrate-webhook-under-net-c-backend-4f7