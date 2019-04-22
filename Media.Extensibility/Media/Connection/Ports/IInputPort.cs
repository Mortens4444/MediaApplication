namespace Media.Extensibility.Media.Connection.Ports
{
    public interface IInputPort : IPortBase
    {
        new IOutputPort ConnectedPort { get; set; }
    }
}