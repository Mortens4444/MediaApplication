using Media.Extensibility.Media.Connection.Ports;
using System.Collections.Generic;

namespace Media.Extensibility.Media.Connection
{
    public interface IInConnection : IConnectionBase
    {
        IEnumerable<IInputPort> InputPins { get; }
    }
}