using System;
using Akka.Actor;

namespace AkkaConsole
{
    internal class ConsoleReaderActor : ReceiveActor
    {
        private readonly IActorRef _consoleWriter;

        public ConsoleReaderActor(IActorRef consoleWriter)
        {
            _consoleWriter = consoleWriter;
            Receive<Messages.StartMonitoringIn>((msg) => { Self.Tell(new Messages.ContinueMonitoringIn()); });
            Receive<Messages.ContinueMonitoringIn>((msg) => { GetAndValidateInput(); });
        }

        private void GetAndValidateInput()
        {
            var message = Console.ReadLine();
            if (!string.IsNullOrEmpty(message) && string.Equals(message, "exit", StringComparison.OrdinalIgnoreCase))
            {
                Context.System.Terminate();
                return;
            }

            _consoleWriter.Tell(new Messages.WriteToOut(message));

            Self.Tell(new Messages.ContinueMonitoringIn());
        }
    }
}