using System;
using Castle.DynamicProxy;

namespace Core.Interceptors
{
    /// <summary>
    /// Bu sınıf base attribute yapısı oluşturmak için tasarlandı
    /// </summary>

    //Attribute olduğunu söyledik. Classlar-metotlar attribute olarak kullansın.
    //Birden fazla olsun ve inherit edilebilsin.

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        public int Priority { get; set; }

        public virtual void Intercept(IInvocation invocation) //Araya gir dedik. Castle.Core içerisinden gelir.
        {

        }
    }
}

