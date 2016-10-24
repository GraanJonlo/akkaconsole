namespace AkkaConsole
{
    using System.Threading.Tasks;
    using Akka.Actor;

    public static class Program
    {
        public static void Main()
        {
            AsyncMain().Wait();
        }

        private static async Task AsyncMain()
        {
            using (var myActorSystem = ActorSystem.Create("MyActorSystem"))
            {
                Props writerProps = Props.Create<ConsoleWriterActor>();
                IActorRef writerActor = myActorSystem.ActorOf(writerProps);

                Props readerProps = Props.Create(() => new ConsoleReaderActor(writerActor));
                IActorRef readerActor = myActorSystem.ActorOf(readerProps);

                readerActor.Tell(new ConsoleReaderActor.MonitorForInput());

                await myActorSystem.WhenTerminated;
            }
        }
    }
}