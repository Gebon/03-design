namespace battleships
{
    public class AiFactory
    {
        public Ai Create(string exe, ProcessMonitor monitor)
        {
            return new Ai(exe, monitor);
        }
    }
}