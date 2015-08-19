using ClientAndHostingInOne.Interface;
using ClientAndHostingInOne.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientAndHostingInOne
{
    class Program
    {
        static void Main(string[] args)
        {
            StartServer();

            Console.ReadKey();
        }

        static void StartServer() 
        {
            string serviceUrl = "http://127.0.0.1:23232/myservices/calculatorservice";

            using (ServiceHost serviceHost = new ServiceHost(typeof(CalculatorService)))
            {
                serviceHost.AddServiceEndpoint(typeof(ICalculator), new WSHttpBinding(), "http://127.0.0.1:23232/myservices/calculatorservice");

                if (serviceHost.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
                {
                    ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                    behavior.HttpGetEnabled = true;
                    behavior.HttpGetUrl = new Uri(string.Format("{0}/{1}", serviceUrl, "metadata"));
                    serviceHost.Description.Behaviors.Add(behavior);
                }
                serviceHost.Opened += delegate
                {
                    Console.WriteLine("CalculatorService is opened.");

                    StartClient();
                };
                serviceHost.Open();

                while (true) 
                {
                    Thread.Sleep(1000000);
                }
            }
        }

        static void StartClient()
        {
            string serviceUrl = "http://127.0.0.1:23232/myservices/calculatorservice";

            Console.WriteLine("Client is starting.");
            using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>(new WSHttpBinding(), serviceUrl))
            {
                ICalculator proxy = channelFactory.CreateChannel();
                Console.WriteLine("x + y = {2} when x = {0} and y = {1}", 1, 2, proxy.Add(1, 2));
                Console.WriteLine("x - y = {2} when x = {0} and y = {1}", 1, 2, proxy.Substract(1, 2));
                Console.WriteLine("x * y = {2} when x = {0} and y = {1}", 1, 2, proxy.Multiply(1, 2));
                Console.WriteLine("x / y = {2} when x = {0} and y = {1}", 1, 2, proxy.Divide(1, 2));
            }
        }
    }
}
