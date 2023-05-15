using System;
using Entities;
using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
	public class UserValidator: AbstractValidator<RegisterAuthDto>
	{
		public UserValidator()
		{
			RuleFor(p => p.Name).NotEmpty().WithMessage("İsim Alanı Boş Geçilemez");
            RuleFor(p => p.EMail).NotEmpty().WithMessage("Mail Alanı Boş Geçilemez");
            RuleFor(p => p.ImageUrl).NotEmpty().WithMessage("Resim Alanı Boş Geçilemez");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Parola Alanı Boş Geçilemez");
            RuleFor(p => p.Password).MinimumLength(6).WithMessage("Parola Alanı 6 Karakterden Uzun Olmalı.");
        }
    }
}

