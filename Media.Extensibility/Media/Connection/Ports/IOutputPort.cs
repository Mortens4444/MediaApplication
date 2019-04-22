namespace Media.Extensibility.Media.Connection.Ports
{
    public interface IOutputPort : IPortBase
    {
        new IInputPort ConnectedPort { get; set; }
    }
}