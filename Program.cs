
namespace DelegatesAndEvents
{
    class Program
    {
        //Delegates and events are similar functionalities. 
        //Delegatede are function pointer that can be used as callback between sender and receiver.
        //With delegates each side can communicate its like two way binding. For example Receiver can assing null to sender delegate

        //Events are encapsulation of delegates. For example they work as callback as well but its only one way communication. 
        //Source publishes messages and receiver ither can register to it with += or unregister unsubscribe with -=. But receiver can not assign any value to sender event.
        //Event mechanism works as publisher and subscriber just like signalr or azure service bus.
        //To make delegate event -> just add event keyword to delegate property definition and its done.

        static void Main(string[] args)
        {

            Console.WriteLine("This is main thread beginning");

            //Create class instance for accessing methods and its callback delegate method.
            Counter counterObj = new Counter();
            
            //Single delagate Broadcasting to multiple methods (Multicasting)
            counterObj.sender += Send;
            counterObj.sender += Send1;
            counterObj.sender += Send2;

            //Event mechanism -> Its same as delegates except on this one receiver can not change sender class instance. It can only subscripte or unsubscribe and as you see below it can not assing null value.
            counterObj.senderEvent += SendEvent;
            counterObj.senderEvent += SendEvent1;

            // This line would throw error -> counterObj.senderEvent = null;

            //Start paralel programming for thread.
            Thread newThread = new Thread(new ThreadStart(counterObj.StartCounting));
            newThread.Start();


            //Now on every 3 seconds Counter class delegate will be triggered. Since its mapped to Send method Send method will be triggered as well and write i value to console.
        }

        public static void Send(int i)
        {
            Console.WriteLine("Send " +  i.ToString());
        }

        public static void Send1(int i)
        {
            Console.WriteLine("Send1 " + i.ToString());
        }

        public static void Send2(int i)
        {
            Console.WriteLine("Send2 " + i.ToString());
        }

        public static void SendEvent(int i)
        {
            Console.WriteLine("SendEvent " + i.ToString());
        }

        public static void SendEvent1(int i)
        {
            Console.WriteLine("SendEvent1 " + i.ToString());
        }

        public class Counter
        {
            //This class has delegate usage for callback purposes.
            //I will invoke this delegate based on the scenerio I want to know.
            //And on the main thread I will map function to my delegate to implement actual logic I want to create.

            public delegate void Sender(int i);

            public Sender sender = null;

            public delegate void SenderEvent(int i);
            
            public event SenderEvent senderEvent = null;

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

