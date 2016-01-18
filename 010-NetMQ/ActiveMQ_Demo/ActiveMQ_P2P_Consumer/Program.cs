using Apache.NMS;
using Apache.NMS.ActiveMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActiveMQ_P2P_Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            //创建连接工厂
            IConnectionFactory factory = new ConnectionFactory("tcp://localhost:61616");
            //通过工厂构建连接
            using (IConnection connection = factory.CreateConnection()) 
            {
                //这个是连接的客户端名称标识
                connection.ClientId = "firstQueueListener";
                //启动连接，监听的话要主动启动连接
                connection.Start();
                //通过连接创建一个会话
                using (ISession session = connection.CreateSession()) 
                {
                    //通过会话创建一个消费者，这里就是Queue这种会话类型的监听参数设置
                    IMessageConsumer consumer = session.CreateConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue("firstQueue"), "filter='demo'");
                    //注册监听事件
                    consumer.Listener += consumer_Listener;
                }
            }
            
            Console.ReadKey();
        }

        static void consumer_Listener(IMessage message)
        {
            ITextMessage msg = (ITextMessage)message;
            Console.WriteLine(msg.Text);
        }
    }
}
