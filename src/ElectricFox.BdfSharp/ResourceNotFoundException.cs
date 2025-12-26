namespace ElectricFox.BdfSharp
{
    [Serializable]
    internal class ResourceNotFoundException : Exception
    {
        public string ResourceName { get; }

        public ResourceNotFoundException(string resourceName)
        {
            ResourceName = resourceName;
        }

        public ResourceNotFoundException(string resourceName, string? message)
            : base(message)
        {
            ResourceName = resourceName;
        }

        public ResourceNotFoundException(
            string resourceName,
            string? message,
            Exception? innerException
        )
            : base(message, innerException)
        {
            ResourceName = resourceName;
        }
    }
}
