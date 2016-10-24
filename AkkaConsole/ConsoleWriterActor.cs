namespace AkkaConsole
{
    using System;
    using Akka.Actor;

    internal class ConsoleWriterActor : ReceiveActor
    {
        public ConsoleWriterActor()
        {
            Receive<WriteToOut>(msg =>
            {
                Console.WriteLine(msg.Content);
                Sender.Tell(new ConsoleReaderActor.MonitorForInput());
            });
        }

        public class WriteToOut
        {
            public string Content { get; }

            public WriteToOut(string message)
            {
                Content = message;
            }
        }
    }
}