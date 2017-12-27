using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroMQ;

namespace chat_c
{
    static partial class Program
    {
        public static void Main(string[] args)
        {

            if (args == null || args.Length < 1)
            {
                args = new string[] { "tcp://10.168.78.254:8080" };
            }

            string endpoint = args[0];

            string UserName;
            Console.Write("Enter your name: ");
            UserName = Console.ReadLine();

            // Create
            using (var context = new ZContext())
            using (var requester = new ZSocket(context, ZSocketType.REQ))
            {
                // Connect
                requester.Connect(endpoint);


                while (true)
                {
                    string requestText;
                    Console.Write("Enter your message: ");
                    requestText = UserName + ": " + Console.ReadLine();
                    // Send
                    requester.Send(new ZFrame(requestText));

                    // Receive
                    using (ZFrame reply = requester.ReceiveFrame())
                    {

                        Console.WriteLine(reply.ReadString());
                        //Thread.Sleep(10000);
                    }
                }

            }
        }
    }
}
