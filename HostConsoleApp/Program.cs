using CallbackService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using WcfServiceLibrary4_2;

namespace HostConsoleApp
{
    class Program
    {
        static void Main(string [] args)
        {
            Uri baseAddress1 = new Uri("http://localhost:10000/MyCCalculator");
            ServiceHost myHost1 = new ServiceHost(typeof(MyCCalculator), baseAddress1);
            BasicHttpBinding myBinding1 = new BasicHttpBinding();
            ServiceEndpoint endpoint1 = myHost1.AddServiceEndpoint(typeof(ICCalculator), myBinding1, "endpoint1");

            Uri baseAddress2 = new Uri("http://localhost:10000/AsyncService");
            ServiceHost myHost2 = new ServiceHost(typeof(AsyncService), baseAddress2);
            BasicHttpBinding myBinding2 = new BasicHttpBinding();
            ServiceEndpoint endpoint2 = myHost2.AddServiceEndpoint(typeof(IAsyncService), myBinding2, "endpoint2");

            Uri baseAddress3 = new Uri("http://localhost:10000/CallbackService");
            ServiceHost myHost3 = new ServiceHost(typeof(MySuperCalc), baseAddress3);
            WSDualHttpBinding myBinding3 = new WSDualHttpBinding(WSDualHttpSecurityMode.None);
            ServiceEndpoint endpoint3 = myHost3.AddServiceEndpoint(typeof(ISuperCalc), myBinding3, "endpoint3");

            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            myHost1.Description.Behaviors.Add(smb);
            myHost2.Description.Behaviors.Add(smb);
            myHost3.Description.Behaviors.Add(smb);
            
            try
            {
                myHost1.Open();
                Console.WriteLine("--> ComplexCalculator is running.");
                myHost2.Open();
                Console.WriteLine("--> Async service is running");
                myHost3.Open();
                Console.WriteLine("--> Callback SuperCalc is running.");
                Console.WriteLine("--> Press <ENTER> to stop.\n");
                Console.ReadLine(); //wait for stop
                myHost1.Close();
                Console.WriteLine("--> ComplexCalculator finished");
                myHost2.Close();
                Console.WriteLine("--> Async service finished");
                myHost3.Close();
                Console.WriteLine("--> Callback SuperCalc finished");
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("Exception occurred: {0}", ce.Message);
                myHost1.Abort();
                myHost2.Abort();
                myHost3.Abort();
            }

            Console.ReadLine(); //wait for stop
        }
    }
}
