using System;
using Castle.DynamicProxy;
using Core.Interceptors;
using Core.Utilities.IoC;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Performance
{
    public class PerformanceAspect : MethodInterception
    {
        private int _interval; //Kaç saniye sonra kontrole başlayacak
        private Stopwatch _stopwatch;

        public PerformanceAspect()
        {
            _interval = 30;
        }

        public PerformanceAspect(int interval)
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }

        //Metotun önünde kromometreyi başlatıyoruz.
        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }

        //Geçen süreyi hesaplar.
        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                Debug.WriteLine($"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}");
            }
            _stopwatch.Reset();
        }
    }
}

