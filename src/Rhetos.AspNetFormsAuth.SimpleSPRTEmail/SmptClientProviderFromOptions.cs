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

using System.Net;
using System.Net.Mail;

namespace Rhetos.AspNetFormsAuth.SimpleSPRTEmail
{
    public class SmptClientProviderFromOptions : ISmptClientProvider
    {
        private readonly SmptOptions _smptOptions;

        public SmptClientProviderFromOptions(SmptOptions smptOptions)
        {
            _smptOptions = smptOptions;
        }

        public MailMessage CreateMailMessage()
        {
            var mailMessage = new MailMessage();

            if (_smptOptions.From != null)
                mailMessage.From = new MailAddress(_smptOptions.From);

            return mailMessage;
        }

        public SmtpClient CreateSmtpClient()
        {
            var smtpClient = new SmtpClient();

            if (_smptOptions.Host != null)
                smtpClient.Host = _smptOptions.Host;

            if (_smptOptions.EnableSsl != null)
                smtpClient.EnableSsl = _smptOptions.EnableSsl.Value;

            if (_smptOptions.Port != null)
                smtpClient.Port = _smptOptions.Port.Value;

            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            if (_smptOptions.DefaultCredentials != null)
                smtpClient.UseDefaultCredentials = _smptOptions.DefaultCredentials.Value;

            if (_smptOptions.UserName != null)
                smtpClient.Credentials = new NetworkCredential(_smptOptions.UserName, _smptOptions.Password);

            if (_smptOptions.TargetName != null)
                smtpClient.TargetName = _smptOptions.TargetName;

            return smtpClient;
        }
    }
}
