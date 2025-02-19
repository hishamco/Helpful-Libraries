# ASP.NET Core Libraries Documentation


Please see the inline documentation of each extension methods learn more.


- `ForwardedHeadersApplicationBuilderExtensions`: Provides `UseForwardedHeadersForCloudflareAndAzure()` that forwards proxied headers onto the current request with settings suitable for an app behind Cloudflare and hosted in an Azure App Service.
- `EnvironmentHttpContextExtensions`: Provides shortcuts to determine information about the current hosting environment, like whether the app is running in Development mode.
- `NonEmptyTagHelper`: An attribute tag helper that conditionally hides its element if the provided collection is null or empty. This eliminates a bulky wrapping `@if(collection?.Count > 1) { ... }` expression that would needlessly increase the document's indentation too.
