using Service;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Hosting
{
    class Program
    {
        static void Main(string[] args)
        {
            string serviceUrl = "http://127.0.0.1:23232/calculatorservice";

            using (ServiceHost serviceHost = new ServiceHost(typeof(CalculatorService))) 
            {
                serviceHost.AddServiceEndpoint(typeof(ICalculator), new WSHttpBinding(), "http://127.0.0.1:23232/calculatorservice");
                if (serviceHost.Description.Behaviors.Find<ServiceMetadataBehavior>() == null) 
                {
                    ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                    behavior.HttpGetEnabled = true;
                    behavior.HttpGetUrl = new Uri(string.Format("{0}{1}", serviceUrl, "metadata"));
                }
                serviceHost.Opened += delegate{
                    Console.WriteLine("CalculatorService is opened, press any key to end service.");
                };
                serviceHost.Open();

                Console.Read();
            }

        }
    }
}
