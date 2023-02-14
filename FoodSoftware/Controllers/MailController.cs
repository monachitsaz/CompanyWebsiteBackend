using FoodSoftware.Common;
using FoodSoftware.Helpers;
using FoodSoftware.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FoodSoftware.Controllers
{
  
    public class Email
    {
        public string To { get; set; }
        public string Cc { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
    }

    public class MailController : BaseHomeController
    {
        private readonly IMailService mailService;
        public MailController(IMailService mailService)
        {
            this.mailService = mailService;
        }

        //[HttpPost]
        //public async Task<IActionResult> SendMail([FromBody] Email email)
        //{
        //    var client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
        //    client.UseDefaultCredentials = false;
        //    client.EnableSsl = true;
        //    client.Credentials = new System.Net.NetworkCredential("chitsazmn@gmail.com", "mnmn1363");

        //    var mailMessage = new System.Net.Mail.MailMessage();
        //    mailMessage.From = new System.Net.Mail.MailAddress("chitsazmn@gmail.com");

        //    mailMessage.To.Add(email.To);

        //    if (!string.IsNullOrEmpty(email.Cc))
        //    {
        //        mailMessage.CC.Add(email.Cc);
        //    }

        //    mailMessage.Body = email.Text;

        //    mailMessage.Subject = email.Subject;

        //    mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
        //    mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

        //    await client.SendMailAsync(mailMessage);

        //    return Ok();
        //}

        [HttpPost("Send")]
        public async Task<IActionResult> SendMail([FromBody] MailRequest request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return Ok(message);
            }

        }
    }
}
