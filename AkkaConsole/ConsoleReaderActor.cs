namespace AkkaConsole
{
    using System;
    using Akka.Actor;

    internal class ConsoleReaderActor : ReceiveActor
    {
        private readonly IActorRef _writer;

        public ConsoleReaderActor(IActorRef writer)
        {
            _writer = writer;
            Receive<MonitorForInput>(msg => { GetAndValidateInput(); });
        }

        private void GetAndValidateInput()
        {
            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input) && string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
            {
                Context.System.Terminate();
                return;
            }

            _writer.Tell(new ConsoleWriterActor.WriteToOut(input));
        }

        public class MonitorForInput
        { }
    }
}