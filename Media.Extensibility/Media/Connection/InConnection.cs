using Media.Extensibility.Media.Connection.Ports;
using System.Collections.Generic;

namespace Media.Extensibility.Media.Connection
{
    public class InConnection : ConnectionBase, IInConnection
    {
        private IEnumerable<IInputPort> inputPins;

        public IEnumerable<IInputPort> InputPins
        {
            get => inputPins;
            set
            {
                if (inputPins != value)
                {
                    inputPins = value;
                    if (inputPins != null)
                    {
                        foreach (var inputPin in inputPins)
                        {
                            inputPin.Filter = this;
                        }
                    }
                }
            }
        }

        public InConnection(string name, IEnumerable<IInputPort> inputPins = null)
            : base(name)
        {
            InputPins = inputPins;
        }
    }
}
