# Rhetos.SimpleSPRTEmail release notes

## 5.0.0 (TO BE RELEASED)

### Breaking changes

1. Migrated from .NET Framework to .NET 5 and Rhetos 5.
2. Removed support for configuring the SmptClient through Web.config. Use a JSON file for configuration as in the example below
    ```json
    {
        "SimpleSPRTEMail": {
            "Smpt": {
                "Host": "smtp.gmail.com",
                "Port": 587,
                "EnableSsl": true,
                "UserName": "username",
                "Password": "pasword",
                "From": "username@gmail.com"
            }
        }
    }
    ```
