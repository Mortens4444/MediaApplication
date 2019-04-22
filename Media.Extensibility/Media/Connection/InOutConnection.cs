using Media.Extensibility.Media.Connection.Ports;
using System.Collections.Generic;

namespace Media.Extensibility.Media.Connection
{
    public class InOutConnection : ConnectionBase, IInConnection, IOutConnection
    {
        private IEnumerable<IInputPort> inputPins;
        private IEnumerable<IOutputPort> outputPins;

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

        public InOutConnection(string name, IEnumerable<IInputPort> inputPins = null, IEnumerable<IOutputPort> outputPins = null)
            : base(name)
        {
            InputPins = inputPins;
            OutputPins = outputPins;
        }
    }
}
