using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02ThreadBasic
{
    class Program
    {
        static void Main(string[] args)
        {
            //ThreadTest.CreateThread();
            //ThreadTest.CreateThreadWithParameter();
            //ThreadTest.CreateBackgroundThread(true);
            //ThreadTest.CreateBackgroundThread(false);
            //ThreadTest.TestPriority();
            //ThreadTest.AbortThread();
            //ThreadTest.UseJoin();
            //ThreadTest.SharedResource();

            //different way to make thread synchronize
            //ThreadTest.ThreadSynchronization_1_Loop();
            //ThreadTest.ThreadSynchronization_2_LoopWithSleep();
            //ThreadTest.ThreadSynchronization_3_UseJoin();
            //ThreadTest.ThreadSynchronization_4_UseLock();
            //ThreadTest.ThreadSynchronization_5_AutoResetEvent();
            //ThreadTest.ThreadSynchronization_6_ManualResetEvent();

            //ThreadTest.UseAutoResetEvent();
            //ThreadTest.UseManualEvent();
            //ThreadTest.UseSemaphore();
            //todo: Monitor
            //todo: ReaderWriterLockSlim

            //ThreadTest.UseTimer();
            ThreadTest.UserTimer2();

            Console.ReadKey();
        }
    }
}
