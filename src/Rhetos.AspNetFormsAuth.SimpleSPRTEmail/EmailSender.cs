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

using Rhetos.Dom.DefaultConcepts;
using Rhetos.Logging;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net.Mail;

namespace Rhetos.AspNetFormsAuth.SimpleSPRTEmail
{
    [Export(typeof(ISendPasswordResetToken))]
    public class EmailSender : ISendPasswordResetToken
    {
        private readonly GenericRepository<IPrincipalWithEmail> _principalRepository;
        private readonly GenericRepository<IEmailFormat> _emailFormatRepository;
        private readonly ILogger _logger;
        private readonly SmtpClient _smtp;
        private readonly ISmptClientProvider _smptClientProvider;

        public EmailSender(
            GenericRepository<IPrincipalWithEmail> principalRepository,
            GenericRepository<IEmailFormat> emailFormatRepository,
            ILogProvider logProvider,
            ISmptClientProvider smptClientProvider)
        {
            _principalRepository = principalRepository;
            _emailFormatRepository = emailFormatRepository;
            _logger = logProvider.GetLogger("SimpleSPRTEmail." + GetType().Name);
            _smptClientProvider = smptClientProvider;
            _smtp = _smptClientProvider.GetSmtpClient();
        }

        private IEmailFormat ReadEmailFormat()
        {
            var mailFormats = _emailFormatRepository.Load();

            if (mailFormats.Count() == 0)
                throw new FrameworkException("There is no email format set in " + _emailFormatRepository.EntityName + ".");

            if (mailFormats.Count() > 1)
                throw new FrameworkException("There is more than one email format set in " + _emailFormatRepository.EntityName + ".");

            return mailFormats.Single();
        }

        public void SendPasswordResetToken(string userName, Dictionary<string, string> additionalClientInfo, string passwordResetToken)
        {
            var emailFormat = ReadEmailFormat();

            var email = _smptClientProvider.GetMailMessage();
            email.Subject = emailFormat.Subject;
            email.Body = emailFormat.Body.Replace("{Token}", passwordResetToken).Replace("{UserName}", userName);
            email.IsBodyHtml = emailFormat.IsBodyHtml == true;

            string error = null;
            if (string.IsNullOrEmpty(_smtp.Host))
                error = "\"host\" is not set";
            else if (email.From == null)
                error = "\"from\" is not set";
            if (error != null)
                throw new FrameworkException("Email settings are not configured (" + error + "). Use \"system.Net mailSettings smpt\" configuration element.");

            string userEmail = _principalRepository.Query(p => p.Name == userName).Select(p => p.Email).SingleOrDefault();
            if (string.IsNullOrEmpty(userEmail))
            {
                // The error is logged, but the user information is not returned to the client, for security reasons.
                _logger.Error("There is no email address set in " + _principalRepository.EntityName + " for user '" + userName + "'.");
                return;
            }
            email.To.Add(userEmail);

            _logger.Trace("Sending email from " + email.From + " over " + _smtp.Host + ":" + _smtp.Port + " to " + userEmail);
            _smtp.Send(email);
        }
    }
}
