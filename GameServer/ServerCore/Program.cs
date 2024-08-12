namespace ServerCore
{
    class SpinLock()
    {
         int _locked= 0;
        public void Acquire()
        { 
            while(true)
            {
                int original = Interlocked.CompareExchange(ref _locked, 1, 0);
                if (original == 0)
                {
                    break;
                }
                //Thread.Sleep(1);
                //Thread.Sleep(0);
                Thread.Yield();
            }
            
        }

        public void Release()
        {
            _locked = 0;
        }
    }

    class Program
    {
        static int number = 0;
        static SpinLock _lock = new SpinLock();

        static void Thread1()
        {
            for(int i = 0;i<100000;i++)
            {
                _lock.Acquire();
                number++;
                _lock.Release();

            }
        }

        static void Thread2()
        {
            for (int i = 0; i < 100000; i++)
            {
                _lock.Acquire();
                number--;
                _lock.Release();
            }
        }

        static void Main(string[] args)
        {
            Task t1 = new Task(Thread1);
            Task t2 = new Task(Thread2);

            t1.Start();
            t2.Start();

            Task.WaitAll(t1,t2);

            Console.WriteLine(number);
        }
    }
}
