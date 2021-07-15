# SimpleSPRTEmail

SimpleSPRTEmail is a plugin package for [Rhetos development platform](https://github.com/Rhetos/Rhetos).
It allows sending simple emails on "Send password reset token" authentication service method at [AspNetFormsAuth](https://github.com/Rhetos/AspNetFormsAuth).

## Features

### Entering user's email address

SimpleSPRTEmail package adds the **Email** property (ShortString) to the **Common.Principal** entity. Use CRUD operations on Common.Principal to edit the user's email address.

### Connecting to an SMTP email server

SimpleSPRTEmail uses `System.Net.Mail` classes from .NET Framework for sending an email to an SMTP server.

Use [&lt;mailSettings&gt; element](http://msdn.microsoft.com/en-us/library/w355a94k%28v=vs.100%29.aspx) in `App.config` to configure the email sending options. An example:

```XML
<configuration>
  ...

    <system.net>
      <mailSettings>
        <smtp from="test@foo.com">
          <network host="smtp.example.com" port="25" userName="username1" password="secret1" defaultCredentials="true" />
        </smtp>
      </mailSettings>
    </system.net>
</configuration>
```

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
