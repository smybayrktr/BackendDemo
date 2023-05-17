using System;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Interceptors;
using FluentValidation;

namespace Core.Aspects.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType) //Attribue ye tip atıyoruz
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil!");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation) //Metot çalışmadan önce bu çalışır
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); //Validator için bellekte yer ayrıldı
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; //Validatorün base inin ilk paremetresinin tipini alır
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); //metotun parametrelerini gezer entityType ile aynı mı kontrol eder. 
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity); //Onu Doğrular.
            }
        }
    }
}

