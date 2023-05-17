using System;
using Castle.DynamicProxy;

namespace Core.Interceptors
{
    //BU SINIF NE ZAMAN ÇALIŞACAKLARI KONUSU İÇİN BİR ŞABLON TASARLAR
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        //IInvocation bizim metotumuz
        //içleri boş ne için doldurursak onun için çalışır.
        protected virtual void OnBefore(IInvocation invocation) { } //İşlem öncesinde çalışır
        protected virtual void OnAfter(IInvocation invocation) { } //İşlem sonrasında çalışır
        protected virtual void OnException(IInvocation invocation, System.Exception e) { }
        protected virtual void OnSuccess(IInvocation invocation) { }


        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();  //İşlemi devam ettirir.
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation, e);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);
        }
    }
}

