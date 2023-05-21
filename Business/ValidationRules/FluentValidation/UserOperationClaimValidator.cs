using System;
using Entities;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
	public class UserOperationClaimValidator : AbstractValidator<UserOperationClaim>
    {
		public UserOperationClaimValidator()
		{
            RuleFor(p => p.UserId).Must(IsIdValid).WithMessage("Yetki Ataması için Kullanıcı Seçimi Yapmanız Lazım");

            RuleFor(p => p.OperationClaimId).Must(IsIdValid).WithMessage("Yetki Ataması için Yetki Seçimi Yapmanız Lazım");
        }

        private bool IsIdValid(int Id)
        {
            if (Id>0 && Id != null)
            {
                return true;
            }
            return false;
        }
	}
    
}

