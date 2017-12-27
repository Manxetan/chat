using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroMQ;
using System.Threading;

namespace chat_s
{
    static partial class Program
    {
        public static void Main(string[] args)
        {

            args = new string[] { "" };

            // Create
            using (var context = new ZContext())
            using (var responder = new ZSocket(context, ZSocketType.REP))
            {
                // Bind
                responder.Bind("tcp://*:5570");

                while (true)
                {
                    // Receive
                    using (ZFrame request = responder.ReceiveFrame())
                    {
                        string k;
                        int i = 0;
                        k = request.ReadString();
                        Console.WriteLine(k);

                        // Do some work
                        Thread.Sleep(1);
                        // Send 
                        args = new string[] { k };
                        string name = args[i];

                        responder.Send(new ZFrame(args[i]));
                        i++;
                    }
                }
            }
        }
    }
}
