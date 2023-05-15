using System;
using FluentValidation;

namespace Core.CrossCuttingConcerns.Validation
{
	public class ValidationTool
	{
        //UserValidator: AbstractValidator u implement etmişti o da bir IValidator olduğu için
		//UserValidator bir IValidatordür. Onu parametre olarak aldık.
        public static void Validate(IValidator validator, object entity)
		{
			var context = new ValidationContext<object>(entity);
			var result = validator.Validate(context);
			if (!result.IsValid)
			{
				throw new ValidationException(result.Errors);
			}
		}
	}
}

