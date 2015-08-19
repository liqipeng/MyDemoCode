using Service;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Hosting
{
    class Program
    {
        static void Main(string[] args)
        {
            //UseFullAddress();
            //UseBaseAddress();
            UseAddressHeader();

        }

        /// <summary>
        /// 使用完整地址
        /// </summary>
        static void UseFullAddress() 
        {
            string serviceUrl = "http://127.0.0.1:23232/myservices/calculatorservice";

            using (ServiceHost serviceHost = new ServiceHost(typeof(CalculatorService)))
            {
                serviceHost.AddServiceEndpoint(typeof(ICalculator), new WSHttpBinding(), "http://127.0.0.1:23232/myservices/calculatorservice");

                if (serviceHost.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
                {
                    ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                    behavior.HttpGetEnabled = true;
                    behavior.HttpGetUrl = new Uri(string.Format("{0}{1}", serviceUrl, "metadata"));
                    serviceHost.Description.Behaviors.Add(behavior);
                }
                serviceHost.Opened += delegate
                {
                    Console.WriteLine("CalculatorService is opened, press any key to end service.");
                };
                serviceHost.Open();

                Console.Read();
            }
        }

        /// <summary>
        /// 使用基地址
        /// </summary>
        static void UseBaseAddress() 
        {
            Uri[] baseAddresses = new Uri[2];
            baseAddresses[0] = new Uri("http://127.0.0.1:23232/myservices");
            baseAddresses[1] = new Uri("net.tcp://127.0.0.1:23233/myservices");

            using (ServiceHost serviceHost = new ServiceHost(typeof(CalculatorService), baseAddresses))
            {
                serviceHost.AddServiceEndpoint(typeof(ICalculator), new WSHttpBinding(), "calculatorservice");
                serviceHost.AddServiceEndpoint(typeof(ICalculator), new NetTcpBinding(), "calculatorservice");

                if (serviceHost.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
                {
                    ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                    behavior.HttpGetEnabled = true;
                    behavior.HttpGetUrl = new Uri(string.Format("{0}{1}", baseAddresses[0].AbsoluteUri, "/calculatorservice/metadata"));
                    serviceHost.Description.Behaviors.Add(behavior);
                }

                serviceHost.Opened += delegate
                {
                    Console.WriteLine("CalculatorService is opened, press any key to end service.");
                };
                serviceHost.Open();

                Console.Read();
            }
        }

        static void UseAddressHeader() 
        {
            using (ServiceHost serviceHost = new ServiceHost(typeof(CalculatorService)))
            {
                AddressHeader header = AddressHeader.CreateAddressHeader("UserType", "http://www.aaa.com", "Licensed User");
                EndpointAddress address = new EndpointAddress(new Uri("http://127.0.0.1:23232/myservices/calculatorservice"), header);
                //EndpointAddress address = new EndpointAddress(new Uri("http://127.0.0.1:23232/myservices/calculatorservice"));

                Binding httpBinding = new WS2007HttpBinding();
                ContractDescription contract = ContractDescription.GetContract(typeof(ICalculator));
                ServiceEndpoint endPoint = new ServiceEndpoint(contract, httpBinding, address);

                serviceHost.AddServiceEndpoint(endPoint);

                if (serviceHost.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
                {
                    ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                    behavior.HttpGetEnabled = true;
                    behavior.HttpGetUrl = new Uri("http://127.0.0.1:23232/myservices/calculatorservice/metadata");
                    serviceHost.Description.Behaviors.Add(behavior);
                }

                serviceHost.Opened += delegate
                {
                    Console.WriteLine("CalculatorService is opened, press any key to end service.");
                };
                serviceHost.Open();

                Console.Read();
            }
        }
    }
}
