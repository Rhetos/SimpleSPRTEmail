# SimpleSPRTEmail

SimpleSPRTEmail is a plugin package for [Rhetos development platform](https://github.com/Rhetos/Rhetos).
It allows sending simple emails on "Send password reset token" authentication service method at [AspNetFormsAuth](https://github.com/Rhetos/AspNetFormsAuth).

1. [Installation and configuration](#installation-and-configuration)
   1. [Connecting to an SMTP email server](#connecting-to-an-smtp-email-server)
   2. [Entering user's email address](#entering-users-email-address)
   3. [Email content and format](#email-content-and-format)
2. [Troubleshooting](#troubleshooting)
3. [How to contribute](#how-to-contribute)

## Installation and configuration

Installing this package to a Rhetos application:

1. Add "Rhetos.SimpleSPRTEmail" NuGet package, available at the [NuGet.org](https://www.nuget.org/) on-line gallery.

### Connecting to an SMTP email server

SimpleSPRTEmail uses `System.Net.Mail` classes from .NET Framework for sending an email to an SMTP server.

Specify SMTP configuration in the application's configuration file (for example, in appsettings.json).
See documentation on SmtpClient class for description of each property.
For properties that are not specified in configuration, default SmtpClient settings will be used.

Example:

```json
{
    "SimpleSPRTEMail": {
        "Smpt": {
            "From": "username@gmail.com",
            "Host": "smtp.gmail.com",
            "Port": 587,
            "EnableSsl": true,
            "UserName": "username",
            "Password": "password",
            "DefaultCredentials": false,
            "TargetName": null
        }
    }
}
```

For advanced configuration, register a custom implementation of Rhetos.AspNetFormsAuth.SimpleSPRTEmail.ISmptClientProvider.

### Entering user's email address

SimpleSPRTEmail package adds the **Email** property (ShortString) to the **Common.Principal** entity. Use CRUD operations on Common.Principal to edit the user's email address.

### Email content and format

Email content and format is defined in **SimpleSPRTEmail.EmailFormat** entity. The entity should contain exactly one record.

The email format can be a plain-text or HTML (Bool IsBodyHtml).

The email body may contain the following special tokens:

* `{UserName}` -> will be replaced with the username.
* `{Token}` -> will be replaced with the generated password reset token.

## Troubleshooting

In case of a server error, additional information on the error may be found in the Rhetos app's log file (`RhetosServer.log` for Rhetos v1-v4).
If needed, more verbose logging of the authentication service may be switched on by enabling `Trace` level loggers
`AspNetFormsAuth.AuthenticationService` and `SimpleSPRTEmail.EmailSender`.

For example, in Rhetos v1-v4 add
`<logger name="AspNetFormsAuth.AuthenticationService" minLevel="Trace" writeTo="TraceLog" />`
and `<logger name="SimpleSPRTEmail.EmailSender" minLevel="Trace" writeTo="TraceLog" />`
in Rhetos application's `web.config` or `nlog.config`,
then the trace log will be written to `RhetosServerTrace.log`.

Similar logging configuration will work in Rhetos 5 if using NLog.

## How to contribute

Contributions are very welcome. The easiest way is to fork this repo, and then
make a pull request from your fork. The first time you make a pull request, you
may be asked to sign a Contributor Agreement.
For more info see [How to Contribute](https://github.com/Rhetos/Rhetos/wiki/How-to-Contribute) on Rhetos wiki.

**Building** the source code:

* Note: This package is already available at the [NuGet.org](https://www.nuget.org/) online gallery.
  You don't need to build it from source in order to use it in your application.
* To build the package from source, run `Clean.bat` and `Build.bat`.
* The build output is a NuGet package in the "Install" subfolder.
