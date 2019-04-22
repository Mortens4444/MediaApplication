namespace Media.Extensibility.Media.Connection.Ports
{
    public class InputPort : PortBase, IInputPort
    {
        private IOutputPort connection;

        public new IOutputPort ConnectedPort
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

        public InputPort(string name, IOutputPort connection = null)
            : base(name)
        {
            ConnectedPort = connection;
        }
    }
}
