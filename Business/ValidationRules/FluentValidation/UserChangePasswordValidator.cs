using System;
using Entities;
using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
	public class UserChangePasswordValidator : AbstractValidator<UserChangePasswordDto>
    {
		public UserChangePasswordValidator()
		{
            RuleFor(p => p.NewPassword).NotEmpty().WithMessage("Parola Alanı Boş Geçilemez");
            RuleFor(p => p.NewPassword).MinimumLength(6).WithMessage("Parola Alanı 6 Karakterden Uzun Olmalı.");
            RuleFor(p => p.NewPassword).Matches("[A-Z]").WithMessage("Şifreniz en az 1 büyük karakter içermeli");
            RuleFor(p => p.NewPassword).Matches("[a-z]").WithMessage("Şifreniz en az 1 küçük karakter içermeli");
            RuleFor(p => p.NewPassword).Matches("[^a-zA-z0-9]").WithMessage("Şifreniz en az 1 tane özel karakter içermeli");

        }
    }
}

