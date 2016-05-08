using System;
using Akka.Actor;

namespace AkkaConsole
{
    internal class ConsoleWriterActor : ReceiveActor
    {
        public ConsoleWriterActor()
        {
            Receive<Messages.WriteToOut>((msg) => Console.WriteLine(msg.Content));
        }
    }
}