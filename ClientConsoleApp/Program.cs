using ClientConsoleApp.AsyncService;
using ClientConsoleApp.CallbackService;
using ClientConsoleApp.ComplexCalcClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientConsoleApp
{
    class Program
    {
        static void Main(string [] args)
        {
            Uri baseAddress = new Uri("http://localhost:10000/MyCCalculator/endpoint1");
            BasicHttpBinding myBinding = new BasicHttpBinding();
            EndpointAddress eAddress = new EndpointAddress(baseAddress);

            ChannelFactory<ICCalculator> myCF = new ChannelFactory<ICCalculator>(myBinding, eAddress);

            ICCalculator client1 = myCF.CreateChannel();

            ComplexNum cnum1 = new ComplexNum();
            cnum1.real = 1.2;
            cnum1.imag = 3.4;
            ComplexNum cnum2 = new ComplexNum();
            cnum2.real = 5.6;
            cnum2.imag = 7.8;
            Console.WriteLine("\nCLIENT1 - START");
            Console.WriteLine("...calling addCNum(...)");
            ComplexNum result1 = client1.addCNum(cnum1, cnum2);
            Console.WriteLine(" addCNum(...) = ({0},{1})",
            result1.real, result1.imag);
            Console.WriteLine("--> Press ENTER to continue");
            Console.ReadLine();
            myCF.Close();
            Console.WriteLine("CLIENT1 - STOP");

            
            Uri baseAddress2 = new Uri("http://localhost:10000/AsyncService/endpoint2");
            BasicHttpBinding myBinding2 = new BasicHttpBinding();
            EndpointAddress eAddress2 = new EndpointAddress(baseAddress2);

            ChannelFactory<IAsyncService> myCF2 = new ChannelFactory<IAsyncService>(myBinding2, eAddress2);

            IAsyncService client2 = myCF2.CreateChannel();

            Console.WriteLine("CLIENT2 – START (Async service)");
            Console.WriteLine("...calling Fun 1");
            client2.Fun1("Client2");
            Thread.Sleep(10);
            Console.WriteLine("...continue after Fun 1 call");
            Console.WriteLine("...calling Fun 2");
            client2.Fun2("Client2");
            Thread.Sleep(10);
            Console.WriteLine("...continue after Fun 2 call");
            Console.WriteLine("--> Press ENTER to continue");
            Console.ReadLine();
            myCF2.Close();
            Console.WriteLine("CLIENT2 - STOP");


            Uri baseAddress3 = new Uri("http://localhost:10000/CallbackService/endpoint3");
            WSDualHttpBinding myBinding3 = new WSDualHttpBinding(WSDualHttpSecurityMode.None);
            EndpointAddress eAddress3 = new EndpointAddress(baseAddress3);

            SuperCalcCallback myCbHandler = new SuperCalcCallback();
            InstanceContext instanceContext = new InstanceContext(myCbHandler);
            DuplexChannelFactory<ISuperCalc> myCF3 = new DuplexChannelFactory<ISuperCalc>(typeof(SuperCalcCallback), myBinding3, eAddress3);
            Console.WriteLine("\nCLIENT3 – START (Callbacks):");
            ISuperCalc client3 = myCF3.CreateChannel(instanceContext);
            double value1 = 10;
            Console.WriteLine("...call of Factorial({0})...", value1);
            client3.Factorial(value1);
            int value2 = 5;
            Console.WriteLine("...call of DoSomething...");
            client3.DoSomething(value2);
            value1 = 20;
            Console.WriteLine("...call of Factorial({0})...", value1);
            client3.Factorial(value1);
            Console.WriteLine("--> Client must wait for the results");
            Console.WriteLine("--> Press ENTER after receiving ALL results");
            Console.ReadLine();
            myCF3.Close();
            Console.WriteLine("CLIENT3 - STOP");
        }
    }
}
