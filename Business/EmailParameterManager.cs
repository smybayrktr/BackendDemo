using System;
using Business.BusinessAspects;
using Core.Aspects.Caching;
using Core.Aspects.Validation;
using Core.Utilities.Results;
using DataAccess;
using Entities;
using System.Net;
using System.Net.Mail;
using Business.Constants;
using Business.ValidationRules.FluentValidation;

namespace Business
{
    public class EmailParameterManager : IEmailParameterService
    {
        private readonly IEmailParameterDal _emailParameterDal;

        public EmailParameterManager(IEmailParameterDal emailParameterDal)
        {
            _emailParameterDal = emailParameterDal;
        }

        [SecuredAspect()]
        [ValidationAspect(typeof(EmailParameterValidator))]
        [CacheRemoveAspect("IEmailParameterService.Get")]
        public async Task<IResult> Add(EmailParameter emailParameter)
        {
            await _emailParameterDal.Add(emailParameter);
            return new SuccessResult(Messages.Added);

        }

        [SecuredAspect()]
        [CacheRemoveAspect("IEmailParameterService.Get")]
        public async Task<IResult> Delete(EmailParameter emailParameter)
        {
            await _emailParameterDal.Delete(emailParameter);
            return new SuccessResult(Messages.Deleted);
        }

        public async Task<IDataResult<EmailParameter>> GetById(int id)
        {
            return new SuccessDataResult<EmailParameter>(await _emailParameterDal.Get(p => p.Id == id));
        }

        public async Task<EmailParameter> GetFirst()
        {
            return await _emailParameterDal.GetFirst();
        }

        [CacheAspect()]
        public async Task<IDataResult<List<EmailParameter>>> GetList()
        {
            return new SuccessDataResult<List<EmailParameter>>(await _emailParameterDal.GetAll());
        }

        public async Task<IResult> SendEmail(EmailParameter emailParameter, string body, string subject, string emails)
        {
            using (MailMessage mail = new MailMessage())
            {
                string[] setEmails = emails.Split(",");
                mail.From = new MailAddress(emailParameter.Email);
                foreach (var email in setEmails)
                {
                    mail.To.Add(email);
                }
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = emailParameter.Html;
                //mail.Attachments.Add();
                using (SmtpClient smtp = new SmtpClient(emailParameter.SMTP))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(emailParameter.Email, emailParameter.Password);
                    smtp.EnableSsl = emailParameter.SSL;
                    smtp.Port = emailParameter.Port;
                    await smtp.SendMailAsync(mail);
                }
            }
            return new SuccessResult(Messages.EmailSendSuccesiful);

        }

        [SecuredAspect()]
        [ValidationAspect(typeof(EmailParameterValidator))]
        [CacheRemoveAspect("IEmailParameterService.Get")]
        public async Task<IResult> Update(EmailParameter emailParameter)
        {
            await _emailParameterDal.Update(emailParameter);
            return new SuccessResult(Messages.UpdatedEmailParameter);
        }
    }
}

