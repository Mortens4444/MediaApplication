using Media.Extensibility.Media.Connection.Ports;
using System.Collections.Generic;

namespace Media.Extensibility.Media.Connection
{
    public class OutConnection : ConnectionBase, IOutConnection
    {
        private IEnumerable<IOutputPort> outputPins;

        public IEnumerable<IOutputPort> OutputPins
        {
            get => outputPins;
            set
            {
                if (outputPins != value)
                {
                    outputPins = value;
                    if (outputPins != null)
                    {
                        foreach (var outputPin in outputPins)
                        {
                            outputPin.Filter = this;
                        }
                    }
                }
            }
        }
        public OutConnection(string name, IEnumerable<IOutputPort> outputPins = null)
            : base(name)
        {
            OutputPins = outputPins;
        }
    }
}
