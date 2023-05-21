using System;
using Entities;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
	public class OperationClaimValidator: AbstractValidator<OperationClaim>
    {
        public OperationClaimValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Yetki Alanı Boş Geçilemez");
        }
    }
}

