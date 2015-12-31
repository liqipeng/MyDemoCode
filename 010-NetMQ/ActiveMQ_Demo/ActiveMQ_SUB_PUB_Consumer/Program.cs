using Apache.NMS;
using Apache.NMS.ActiveMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActiveMQ_SUB_HUB_Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create the Connection factory  
            IConnectionFactory factory = new ConnectionFactory("tcp://localhost:61616/");

            //Create the connection  
            using (IConnection connection = factory.CreateConnection())
            {
                connection.ClientId = "testing listener" + DateTime.Now.Millisecond;
                connection.Start();

                //Create the Session  
                using (ISession session = connection.CreateSession())
                {
                    //Create the Consumer  
                    IMessageConsumer consumer = session.CreateDurableConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic("testing"), "testing listener1", null, false);

                    consumer.Listener += new MessageListener(consumer_Listener);

                    Console.ReadLine();
                }
                connection.Stop();
            }
        }

        private static void consumer_Listener(IMessage message)
        {
            ITextMessage msg = message as ITextMessage;
            Console.WriteLine("Receive: " + msg.Text);  
        }
    }
}
