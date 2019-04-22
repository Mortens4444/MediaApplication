namespace Media.Extensibility.Media.Connection
{
    public class ConnectionBase : IConnectionBase
    {
        public string Name { get; private set; }

        public ConnectionBase(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
