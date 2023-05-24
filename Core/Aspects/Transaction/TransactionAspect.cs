using System;
using System.Transactions;
using Castle.DynamicProxy;
using Core.Interceptors;

namespace Core.Aspects.Transaction
{
	public class TransactionAspect:MethodInterception
	{
        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();
                    transaction.Complete();

                }
                catch (Exception ex) //Hata alırsan işlemleri dispose et dedik.
                {
                    transaction.Dispose();
                    throw;
                }
            }
        }
    }
}

