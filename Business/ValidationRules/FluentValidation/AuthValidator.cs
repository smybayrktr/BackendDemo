using System;
using Entities;
using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
	public class AuthValidator : AbstractValidator<RegisterAuthDto>
	{
		public AuthValidator()
		{
			RuleFor(p => p.Name).NotEmpty().WithMessage("İsim Alanı Boş Geçilemez");
            RuleFor(p => p.EMail).NotEmpty().WithMessage("Mail Alanı Boş Geçilemez");
            RuleFor(p => p.EMail).EmailAddress().WithMessage("Mail Formatı Doğru Değil");
            RuleFor(p => p.Image).NotEmpty().WithMessage("Resim Alanı Boş Geçilemez");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Parola Alanı Boş Geçilemez");
            RuleFor(p => p.Password).MinimumLength(6).WithMessage("Parola Alanı 6 Karakterden Uzun Olmalı.");
            RuleFor(p => p.Password).Matches("[A-Z]").WithMessage("Şifreniz en az 1 büyük karakter içermeli");
            RuleFor(p => p.Password).Matches("[a-z]").WithMessage("Şifreniz en az 1 küçük karakter içermeli");
            RuleFor(p => p.Password).Matches("[^a-zA-z0-9]").WithMessage("Şifreniz en az 1 tane özel karakter içermeli");



        }
    }
}

