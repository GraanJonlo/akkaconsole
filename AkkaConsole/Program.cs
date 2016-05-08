using System.Threading.Tasks;
using Akka.Actor;

namespace AkkaConsole
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            AsyncMain().Wait();
        }

        private static async Task AsyncMain()
        {
            using (var myActorSystem = ActorSystem.Create("MyActorSystem"))
            {
                Props consoleWriterProps = Props.Create<ConsoleWriterActor>();
                IActorRef consoleWriterActor = myActorSystem.ActorOf(consoleWriterProps, "consoleWriterActor");

                Props consoleReaderProps = Props.Create(() => new ConsoleReaderActor(consoleWriterActor));
                IActorRef consoleReaderActor = myActorSystem.ActorOf(consoleReaderProps);

                consoleReaderActor.Tell(new Messages.StartMonitoringIn());

                await myActorSystem.WhenTerminated;
            }
        }
    }
}