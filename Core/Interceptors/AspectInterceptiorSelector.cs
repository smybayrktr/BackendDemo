using System;
using Castle.DynamicProxy;
using System.Reflection;

namespace Core.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        //Kaç attribute yazıldıysa hepsini tek tek çalıştırması için tasarlandı.
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList(); //Kaç attribute varsa hepsini yakalar.

            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes); //Metotları classlarla birleştirir.

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}

