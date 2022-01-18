
namespace DelegatesAndEvents
{
    class Program
    {


        static void Main(string[] args)
        {

            Console.WriteLine("This is main thread beginning");

            //Create class instance for accessing methods and its callback delegate method.
            Counter counterObj = new Counter();
            counterObj.sender = Send;

            //Start paralel programming for thread.
            Thread newThread = new Thread(new ThreadStart(counterObj.StartCounting));
            newThread.Start();


            //Now on every 3 seconds Counter class delegate will be triggered. Since its mapped to Send method Send method will be triggered as well and write i value to console.
        }

        public static void Send(int i)
        {
            Console.WriteLine(i);
        }

        public class Counter
        {
            //This class has delegate usage for callback purposes.
            //I will invoke this delegate based on the scenerio I want to know.
            //And on the main thread I will map function to my delegate to implement actual logic I want to create.

            public delegate void Sender(int i);

            public Sender sender = null;

            public void StartCounting ()
            {
                for (int i = 0; i < Int32.MaxValue; i++)
                {
                    Thread.Sleep(3000);
                    sender.Invoke(i);
                }
            }

        }
        
    }
}

