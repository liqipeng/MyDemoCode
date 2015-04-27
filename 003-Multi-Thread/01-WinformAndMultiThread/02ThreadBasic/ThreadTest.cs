using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _02ThreadBasic
{
    class ThreadTest
    {
        #region Create thread

        public static void CreateThread()
        {
            Console.WriteLine("Thread count of current process :{0}", Process.GetCurrentProcess().Threads.Count);

            Thread t1 = new Thread(() =>
            {
                Thread.Sleep(100);
                Thread threadCurrent = Thread.CurrentThread;

                Console.WriteLine("Name: ", threadCurrent.Name);
                Console.WriteLine("ManagedThreadId: {0}", threadCurrent.ManagedThreadId);
                Console.WriteLine("State: {0}");
                Console.WriteLine("ThreadState: {0}", threadCurrent.ThreadState);
                Console.WriteLine("Priority: {0}", threadCurrent.Priority);
                Console.WriteLine("IsBackground: {0}", threadCurrent.IsBackground);
                Console.WriteLine("IsThreadPoolThread: {0}", threadCurrent.IsThreadPoolThread);
            });
            t1.Name = "My-Thread-1";
            t1.Priority = ThreadPriority.Highest;

            t1.Start();

            Console.WriteLine("Thread count of current process :{0}", Process.GetCurrentProcess().Threads.Count);
        }

        #endregion

        #region Create thread with parameter

        public static void CreateThreadWithParameter()
        {
            Thread t1 = new Thread((para) =>
            {
                DateTime? dateTime = para as Nullable<DateTime>;
                Console.WriteLine(dateTime.Value.ToString("yyyy-MM-dd hh:mm:ss"));
            });
            t1.Start(DateTime.Now);

            Thread t2 = new Thread(() =>
            {
                Add(1, 2);
            });
            t2.Start();
        }

        private static void Add(int a, int b)
        {
            Console.WriteLine(a + b);
        }

        #endregion

        #region Create background thread

        public static void CreateBackgroundThread(bool isBackground)
        {
            Thread t1 = new Thread(() =>
            {
                Console.ReadKey();
                Console.WriteLine("OK");
            })
            {
                IsBackground = isBackground
            };
            t1.Start();
        }

        #endregion

        #region Test thread priority

        public static void TestPriority()
        {
            bool isRun = true;
            int a = 0;
            int b = 0;

            new Thread(() =>
            {
                while (isRun)
                {
                    a++;
                }
            })
            {
                Priority = ThreadPriority.Highest
            }.Start();
            new Thread(() =>
            {
                while (isRun)
                {
                    b++;
                }
            }).Start();

            Thread.Sleep(100);
            isRun = false;
            Console.WriteLine("a={0:E3}, b={1:E3}, a-b={2}", a, b, a - b);

        }

        #endregion

        #region Abort thread

        public static void AbortThread()
        {
            var t1 = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        Console.WriteLine(Thread.CurrentThread.ThreadState);
                        Thread.Sleep(1000);
                    }
                }
                catch (ThreadAbortException threadAbortException)
                {
                    Console.WriteLine();
                    Console.WriteLine("catch");
                    Console.WriteLine(Thread.CurrentThread.ThreadState);
                    Console.WriteLine((string)threadAbortException.ExceptionState);
                    Console.WriteLine();
                }
            });
            t1.Start();

            Thread.Sleep(2000);
            t1.Abort("Abort message");
            Thread.Sleep(100);
            Console.WriteLine("Outer msg: " + t1.ThreadState);
        }

        #endregion

        #region Use Join() method to block invoking thread

        public static void UseJoin()
        {
            var t1 = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(100);
                    Console.WriteLine("-");
                }
            });

            var t2 = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(100);
                    Console.WriteLine("----");
                }
            });

            t1.Start();
            //调用用Join会阻塞调用线程500ms
            t1.Join(TimeSpan.FromMilliseconds(500));
            Console.WriteLine("join end");
            t2.Start();
        }

        #endregion

        #region Shared resource

        [ThreadStatic]
        private static int _sharedResource = 0;
        public static void SharedResource()
        {
            new Thread(() =>
            {
                for (int i = 0; i < 100000; i++)
                {
                    _sharedResource++;
                }
                Console.WriteLine("{0}: {1}", Thread.CurrentThread.Name, _sharedResource);
            }) { Name = "thread 1" }.Start();

            new Thread(() =>
            {
                for (int i = 0; i < 200000; i++)
                {
                    _sharedResource++;
                }
                Console.WriteLine("{0}: {1}", Thread.CurrentThread.Name, _sharedResource);
            }) { Name = "thread 2" }.Start();

        }

        #endregion

        #region Thread synchronization (passive)

        public static void ThreadSynchronization_1_Loop()
        {
            int result = -1;
            Stopwatch sw = Stopwatch.StartNew();
            Thread t1 = new Thread(() =>
                {
                    Thread.Sleep(1000);
                    result = 100;
                });

            t1.Start();
            Thread.Sleep(500);
            while (t1.IsAlive) ; //use a loop to block the calling thread until t1 ends
            Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
            Console.WriteLine(result);
        }

        public static void ThreadSynchronization_2_LoopWithSleep()
        {
            int result = -1;
            Stopwatch sw = Stopwatch.StartNew();
            Thread t1 = new Thread(() =>
            {
                Thread.Sleep(1000);
                result = 100;
            });

            t1.Start();
            Thread.Sleep(500);
            //use a loop to block the calling thread until t1 ends
            while (t1.IsAlive)
            {
                //let main thread sleep to save CPU time
                Thread.Sleep(600);
            }
            Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
            Console.WriteLine(result);
        }

        public static void ThreadSynchronization_3_UseJoin()
        {
            int result = -1;
            Stopwatch sw = Stopwatch.StartNew();
            Thread t1 = new Thread(() =>
            {
                Thread.Sleep(1000);
                result = 100;
            });

            t1.Start();
            t1.Join(); //t1 block main thread until it ends
            Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
            Console.WriteLine(result);
        }

        public static void ThreadSynchronization_4_UseLock()
        {
            object locker = new object();
            int result = -1;
            Stopwatch sw = Stopwatch.StartNew();
            Thread t1 = new Thread(() =>
            {
                lock (locker)
                {
                    Thread.Sleep(1000);
                    result = 100;
                }
            });

            t1.Start();
            Thread.Sleep(100);
            lock (locker)
            {
                Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
                Console.WriteLine(result);
            }
        }

        #endregion

        #region Thread synchronization (initiative)

        public static void ThreadSynchronization_5_AutoResetEvent()
        {
            /*
             AutoResetEvent从名字上可以看出它是一个自动Reset的事件通知方式。我举一个例子，您可能一下子就明白了，我们可以把它比作地铁的闸机，插入了票，闸机放行一个人。AutoResetEvent的构造方法中传入的false代表，这个闸机默认是关闭的，只有调用了一次Set之后才能通过，如果您把它改为true的话可以看到主线程根本没有等待，直接输出了结果。所谓的Reset就是闸机关闭的操作，也就是说对于AutoResetEvent，【一个人用一个票过去之后闸机就关闭了】，除非还有后面的人插入了票（Set）。
             */
            EventWaitHandle autoResetEvent = new AutoResetEvent(false); //闸机默认是关闭的
            int result = -1;
            Stopwatch sw = Stopwatch.StartNew();
            Thread t1 = new Thread(() =>
            {
                Console.WriteLine("......");
                Thread.Sleep(1000);
                result = 100;
                Console.WriteLine("准备好了，闸机开启");
                autoResetEvent.Set();
            });

            Console.WriteLine("闸机开启前需要做点准备工作");
            t1.Start();
            Console.WriteLine("等待通过闸机");
            autoResetEvent.WaitOne();
            Console.WriteLine("闸机开了，过了");
            Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
            Console.WriteLine(result);
        }

        public static void ThreadSynchronization_6_ManualResetEvent()
        {
            /*
             唯一的区别就是这个闸机在插入一张票之后还是可以通行，除非有人手动去设置了一下Reset来关闭闸机
             */
            EventWaitHandle manualResetEvent = new ManualResetEvent(false); //闸机默认是关闭的
            int result = -1;
            Stopwatch sw = Stopwatch.StartNew();
            Thread t1 = new Thread(() =>
            {
                Console.WriteLine("......");
                Thread.Sleep(1000);
                result = 100;
                Console.WriteLine("准备好了，闸机开启");
                manualResetEvent.Set();
            });

            Console.WriteLine("闸机开启前需要做点准备工作");
            t1.Start();
            Console.WriteLine("等待通过闸机");
            manualResetEvent.WaitOne();
            Console.WriteLine("闸机开了，过了");
            Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
            Console.WriteLine(result);
        }

        #endregion

        #region Use AutoResetEvent, ManualEvent, Semaphore
        public static void UseAutoResetEvent()
        {
            //var are = new AutoResetEvent(true); //初始化为终止状态

            var are = new AutoResetEvent(false); //初始化为非终止状态：初始化不可以走
            are.Set(); //设置为终止状态：可以走

            for (int i = 0; i < 10; i++)
            {
                new Thread(() =>
                {
                    are.WaitOne(); //阻止当前线程，直到收到信号：我停下，直到有人叫我走
                    Console.WriteLine("{0} - {1}", Thread.CurrentThread.Name, DateTime.Now.ToString("mm:ss"));
                    Thread.Sleep(1000);
                    are.Set(); //设置为终止状态：告诉别人，我走完了
                }) { Name = "TH" + i }.Start();
            }
        }

        public static void UseManualEvent()
        {
            Console.WriteLine("?");
        }

        public static void UseSemaphore()
        {
            Semaphore s = new Semaphore(2, 2);
            for (int i = 0; i < 10; i++)
            {
                new Thread(() =>
                {
                    s.WaitOne();
                    Thread.Sleep(1000);
                    Console.WriteLine(DateTime.Now.ToString("mm:ss"));
                    s.Release();
                }).Start();
            }
        } 
        #endregion

        public static void UseTimer() 
        {
            object locker = new object();
            ThreadPool.SetMinThreads(2, 2);
            ThreadPool.SetMaxThreads(4, 4);

            ShowTime();
            Timer timer = new Timer((state) => 
            {
                lock (locker) 
                {
                    ShowTime();
                    int a, b;
                    ThreadPool.GetAvailableThreads(out a, out b);
                    Console.WriteLine("可用辅助线程的数目：{0}，可用异步 I/O 线程的数目：{1}", a, b);
                }
            }, null, 700, 2000);
            Thread.Sleep(3000);
            timer.Change(1000, 100);
            Console.WriteLine("Change");
            Thread.Sleep(20000);
            timer.Dispose();
            Console.WriteLine("Timer disposed.");
        }

        public static void UserTimer2() 
        {
            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += (object sender, System.Timers.ElapsedEventArgs e) => {
                ShowTime();
            };
            timer.Start();
            Console.WriteLine("{0} - Start", DateTime.Now.ToString("mm:ss:fff"));

            /*
             1） 由于System.Timers.Timer封装了System.Threading.Timer，所以还是基于线程池
            2） 默认Timer是停止的，启动后需要等待一个Interval再执行回调方法
             */
        }

        private static void ShowTime()
        {
            Console.WriteLine(DateTime.Now.ToString("mm:ss:fff"));
        }
    }
}
