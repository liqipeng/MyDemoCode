using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //WithoutHeader();
            WithHeader();

            Console.Read();
        }

        static void WithoutHeader() 
        {
            string serviceUrl = "http://127.0.0.1:23232/myservices/calculatorservice";

            using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>(new WSHttpBinding(), serviceUrl))
            {
                ICalculator proxy = channelFactory.CreateChannel();
                Console.WriteLine("x + y = {2} when x = {0} and y = {1}", 1, 2, proxy.Add(1, 2));
                Console.WriteLine("x - y = {2} when x = {0} and y = {1}", 1, 2, proxy.Substract(1, 2));
                Console.WriteLine("x * y = {2} when x = {0} and y = {1}", 1, 2, proxy.Multiply(1, 2));
                Console.WriteLine("x / y = {2} when x = {0} and y = {1}", 1, 2, proxy.Divide(1, 2));
            }
        }

        static void WithHeader() 
        {
            AddressHeader header = AddressHeader.CreateAddressHeader("UserType", "http://www.aaa.com", "Licensed User");
            EndpointAddress address = new EndpointAddress(new Uri("http://127.0.0.1:23232/myservices/calculatorservice"), header);
            //EndpointAddress address = new EndpointAddress(new Uri("http://127.0.0.1:23232/myservices/calculatorservice"));
            Binding httpBinding = new WS2007HttpBinding();
            ContractDescription contract = ContractDescription.GetContract(typeof(ICalculator));
            ServiceEndpoint endPoint = new ServiceEndpoint(contract, httpBinding, address);

            using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>(endPoint))
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
