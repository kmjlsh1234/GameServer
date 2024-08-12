﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore
{
    
    class Mutex_Test()
    {
        static int num = 0;
        static Mutex _lock = new Mutex();

        static void Thread1()
        {
            for(int i = 0; i<100000;i++)
            {
                _lock.WaitOne();
                num++;
                _lock.ReleaseMutex();
            }
        }

        static void Thread2()
        {
            for (int i = 0; i < 100000; i++)
            {
                _lock.WaitOne();
                num--;
                _lock.ReleaseMutex();
            }
        }

        static void Main(string[] args)
        {
            Task t1 = new Task(Thread1);
            Task t2 = new Task(Thread2);
            t1.Start();
            t2.Start();
            Task.WaitAll(t1,t2);
            Console.WriteLine(num);
        }

    }
}
