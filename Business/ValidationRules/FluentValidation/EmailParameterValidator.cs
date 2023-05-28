using System;
using Entities;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class EmailParameterValidator : AbstractValidator<EmailParameter>
    {
        public EmailParameterValidator()
        {
            RuleFor(p => p.SMTP).NotEmpty().WithMessage("SMTP adresi boş olamaz");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Mail adresi boş olamaz");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Şifre adresi boş olamaz");
            RuleFor(p => p.Port).NotEmpty().WithMessage("Port adresi boş olamaz");
        }
    }
}

