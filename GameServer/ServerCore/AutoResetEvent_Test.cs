using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore
{
    class Lock()
    {
        AutoResetEvent _available = new AutoResetEvent(true);

        public void Acquire()
        {
            _available.WaitOne(); //입장 시도
        }

        public void Release()
        {
            _available.Set();
        }
    }

    class AutoResetEvent_Test()
    {
        static int num = 0;
        static Lock _lock = new Lock();

        static void Thread1()
        {
            for(int i = 0; i<100000;i++)
            {
                _lock.Acquire();
                num++;
                _lock.Release();
            }
        }

        static void Thread2()
        {
            for (int i = 0; i < 100000; i++)
            {
                _lock.Acquire();
                num--;
                _lock.Release();
            }
        }

        //static void Main(string[] args)
        //{
        //    Task t1 = new Task(Thread1);
        //    Task t2 = new Task(Thread2);
        //    t1.Start();
        //    t2.Start();
        //    Task.WaitAll(t1,t2);
        //    Console.WriteLine(num);
        //}

    }
}
