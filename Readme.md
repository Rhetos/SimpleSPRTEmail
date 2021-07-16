# SimpleSPRTEmail

SimpleSPRTEmail is a plugin package for [Rhetos development platform](https://github.com/Rhetos/Rhetos).
It allows sending simple emails on "Send password reset token" authentication service method at [AspNetFormsAuth](https://github.com/Rhetos/AspNetFormsAuth).

## Features

### Entering user's email address

SimpleSPRTEmail package adds the **Email** property (ShortString) to the **Common.Principal** entity. Use CRUD operations on Common.Principal to edit the user's email address.

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
            "Password": "pasword",
            "DefaultCredentials": false,
            "TargetName": null
        }
    }
}
```

For advanced configuration, register a custom implementation of Rhetos.AspNetFormsAuth.SimpleSPRTEmail.ISmptClientProvider.

### Email content and format

Email content and format is defined in **SimpleSPRTEmail.EmailFormat** entity. The entity should contain exactly one record.

The email format can be a plain-text or HTML (Bool IsBodyHtml).

The email body may contain the following special tokens:

* `{UserName}` -> will be replaced with the username.
* `{Token}` -> will be replaced with the generated password reset token.

## Troubleshooting

In case of a server error, additional information on the error may be found in the Rhetos server log (`RhetosServer.log` file, by default).
If needed, more verbose logging of the authentication service may be switched on by adding
`<logger name="AspNetFormsAuth.AuthenticationService" minLevel="Trace" writeTo="TraceLog" />`
and `<logger name="SimpleSPRTEmail.EmailSender" minLevel="Trace" writeTo="TraceLog" />`
in Rhetos server's `web.config`. The trace log will be written to `RhetosServerTrace.log`.
