# Rhetos.SimpleSPRTEmail release notes

## 6.0.0 (2025-09-08)

* Update to .NET 8 and Rhetos 6.

## 5.0.0 (2022-03-25)

### Breaking changes

1. Migrated from .NET Framework to .NET 5 and Rhetos 5.
2. SmptClient configuration in Web.config is no longer supported.
   * Move the existing settings from `<mailSettings>` element in Web.config to appsettings.json or rhetos-app.local.settings.json.
     See new JSON configuration format for "SimpleSPRTEMail" in [Readme.md](Readme.md).
