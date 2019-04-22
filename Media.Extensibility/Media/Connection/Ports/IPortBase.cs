namespace Media.Extensibility.Media.Connection.Ports
{
    public interface IPortBase
    {
        string Name { get; }

        ConnectionBase Filter { get; set; }

        IPortBase ConnectedPort { get; set; }
    }
}
