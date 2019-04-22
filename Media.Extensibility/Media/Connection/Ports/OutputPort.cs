namespace Media.Extensibility.Media.Connection.Ports
{
    public class OutputPort : PortBase, IOutputPort
    {
        private IInputPort connection;

        public new IInputPort ConnectedPort
        {
            get
            {
                return connection;
            }
            set
            {
                if (connection != value)
                {
                    connection = value;
                    if (connection != null)
                    {
                        connection.ConnectedPort = this;
                    }
                }
            }
        }

        public OutputPort(string name, IInputPort connection = null)
            : base(name)
        {
            ConnectedPort = connection;
        }
    }
}
