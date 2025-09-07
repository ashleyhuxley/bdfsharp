namespace ElectricFox.BdfSharp
{
    public class BdfLoadException : Exception
    {
        public BdfLoadException()
        {
        }
        public BdfLoadException(string message)
            : base(message)
        {
        }
        public BdfLoadException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
