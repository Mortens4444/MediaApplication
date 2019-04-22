namespace Media.Extensibility.Media.Connection.Ports
{
    public class PortBase : IPortBase
    {
        public string Name { get; private set; }

        public ConnectionBase Filter { get; set; }

        public IPortBase ConnectedPort { get; set; }

        public PortBase(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"{Filter.Name}: {Name}";
        }
    }
}
