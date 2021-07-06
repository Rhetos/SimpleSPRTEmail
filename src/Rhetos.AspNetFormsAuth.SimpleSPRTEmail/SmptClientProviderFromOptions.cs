﻿/*
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

        public MailMessage GetMailMessage()
        {
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_smptOptions.From);
            return mailMessage;
        }

        public SmtpClient GetSmtpClient()
        {
            var smtpClient = new SmtpClient();
            smtpClient.Host = _smptOptions.Host;
            smtpClient.EnableSsl = _smptOptions.EnableSsl;
            smtpClient.Port = _smptOptions.Port;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = new NetworkCredential(_smptOptions.UserName, _smptOptions.Password);
            return smtpClient;
        }
    }
}