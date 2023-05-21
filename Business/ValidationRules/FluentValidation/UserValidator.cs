using System;
using Entities;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
	public class UserValidator : AbstractValidator<User>
    {
		public UserValidator()
		{
            RuleFor(p => p.Name).NotEmpty().WithMessage("İsim Alanı Boş Geçilemez");
            RuleFor(p => p.EMail).NotEmpty().WithMessage("Mail Alanı Boş Geçilemez");
            RuleFor(p => p.EMail).EmailAddress().WithMessage("Mail Formatı Doğru Değil");
            RuleFor(p => p.ImageUrl).NotEmpty().WithMessage("Resim Alanı Boş Geçilemez");

        }
    }
}

