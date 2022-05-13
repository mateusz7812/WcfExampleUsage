using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using WcfServiceLibrary4_2;

namespace ClientConsoleApp
{
    class CalculatorChannelFactory<T> : ChannelFactory<T>
    {
        protected override ServiceEndpoint CreateDescription()
        {
            base.CreateDescription();

            Uri baseAddress1 = new Uri("http://localhost:10000/MyCCalculator");
            ServiceHost myHost1 = new ServiceHost(typeof(MyCCalculator), baseAddress1);
            BasicHttpBinding myBinding1 = new BasicHttpBinding();
            ServiceEndpoint endpoint1 = myHost1.AddServiceEndpoint(typeof(ICCalculator), myBinding1, "endpoint1");

            return endpoint1;
        }
    }

}
