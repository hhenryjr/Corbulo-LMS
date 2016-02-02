using Sabio.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Web.Services.Interfaces
{
    public interface IMessagingService
    {
        Task Mail(MailRequest model);
        Task SendConfirmationEmail(SendConfirmationEmailRequest model);
        Task SendForgotPasswordEmail(SendConfirmationEmailRequest model);
        Task SendAddOfficeHourEmail(string Email, OfficeHourAddRequest model);
    }
}
