using Apache.NMS;
using Apache.NMS.ActiveMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActiveMQ_Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化工厂，这里默认的URL是不需要修改的
            IConnectionFactory factory = new ConnectionFactory("tcp://localhost:61616");

            //通过工厂建立连接
            using (IConnection connection = factory.CreateConnection())
            {
                //通过连接创建Session会话
                using (ISession session = connection.CreateSession())
                {
                    //通过会话创建生产者，方法里面new出来的是MQ中的Queue
                    IMessageProducer prod = session.CreateProducer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue("firstQueue"));
                    for (int i = 0; i < 10000; i++)
                    {
                        //创建一个发送的消息对象
                        ITextMessage message = prod.CreateTextMessage();
                        message.Text = "msg-" + i;
                        //设置消息对象的属性，这个很重要哦，是Queue的过滤条件，也是P2P消息的唯一指定属性
                        message.Properties.SetString("filter", "demo");
                        //生产者把消息发送出去，几个枚举参数MsgDeliveryMode是否长链，MsgPriority消息优先级别，发送最小单位，当然还有其他重载
                        prod.Send(message, MsgDeliveryMode.NonPersistent, MsgPriority.Normal, TimeSpan.MinValue);
                        Console.WriteLine("发送成功!! - {0}", i);
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
