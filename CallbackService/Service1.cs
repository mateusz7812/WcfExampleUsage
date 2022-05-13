using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace CallbackService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MySuperCalc : ISuperCalc
    {
        double result;
        ISuperCalcCallback callback = null;
        public MySuperCalc()
        {
            callback = OperationContext.Current.GetCallbackChannel<ISuperCalcCallback>();
        }
        public void DoSomething(int sec)
        {
            Console.WriteLine("...called DoSomething({0})", sec);
            if (sec > 2 & sec < 10)
                Thread.Sleep(sec * 1000);
            else
                Thread.Sleep(3000);
            callback.DoSomethingResult("Calculatons lasted " +
            sec + " second(s)");
        }
        public void Factorial(double n)
        {
            Console.WriteLine("...called Factorial({0})", n);
            Thread.Sleep(1000);
            result = 1;
            for (int i = 1; i <= n; i++)
                result *= i;
            callback.FactorialResult(result);
        }
    }
}
