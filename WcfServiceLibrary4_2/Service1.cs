using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace WcfServiceLibrary4_2
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class MyCCalculator : ICCalculator
    {
        public ComplexNum addCNum(ComplexNum n1, ComplexNum n2)
        {
            Console.WriteLine("...called addCNum(...)");
            return new ComplexNum(n1.real + n2.real,
            n1.imag + n2.imag);
        }
    }

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class AsyncService : IAsyncService
    {
        public void Fun1(String s1)
        {
            Console.WriteLine("...called Fun 1 - start");
            Thread.Sleep(4 * 1000); // sleep for 4 sec. (4000 ms)
            Console.WriteLine("...Fun 1 - stop");
            return;
        }
        public void Fun2(String s2)
        {
            Console.WriteLine("...called Fun 2 - start ");
            Thread.Sleep(2 * 1000); // sleep for 2 sec. (2000 ms)
            Console.WriteLine("...Fun 2 - stop");
            return;
        }
    }
}
