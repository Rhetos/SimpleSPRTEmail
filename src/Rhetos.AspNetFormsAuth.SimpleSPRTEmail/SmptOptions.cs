/*
    Copyright (C) 2014 Omega software d.o.o.

    This file is part of Rhetos.

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/


using System.Net.Mail;

namespace Rhetos.AspNetFormsAuth.SimpleSPRTEmail
{
    /// <summary>
    /// See documentation on <see cref="SmtpClient"/> class for description of each property.
    /// For properties that are not specified in configuration, default SmtpClient settings will be used.
    /// </summary>
    [Options("SimpleSPRTEMail:Smpt")]
    public class SmptOptions
    {
        public string From { get; set; }

        public string Host { get; set; }

        public int? Port { get; set; }

        public bool? EnableSsl { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool? DefaultCredentials { get; set; }

        public string TargetName { get; set; }

        // The "clientDomain" property of SmtpClient is not configurable on .NET Core,
        // see https://github.com/dotnet/runtime/issues/27765.
    }
}
