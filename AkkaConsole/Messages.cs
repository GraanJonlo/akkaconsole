namespace AkkaConsole
{
    internal class Messages
    {
        public class StartMonitoringIn { }
        public class ContinueMonitoringIn { }

        public class WriteToOut
        {
            public readonly string Content;

            public WriteToOut(string message)
            {
                Content = message;
            }
        }
    }
}
