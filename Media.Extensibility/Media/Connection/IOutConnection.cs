using Media.Extensibility.Media.Connection.Ports;
using System.Collections.Generic;

namespace Media.Extensibility.Media.Connection
{
    public interface IOutConnection : IConnectionBase
    {
        IEnumerable<IOutputPort> OutputPins { get; }
    }
}