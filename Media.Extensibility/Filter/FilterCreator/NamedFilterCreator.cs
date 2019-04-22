using DirectShowLib;
using System;

namespace Media.Extensibility.Filter.FilterCreator
{
    public class NamedFilterCreator : FilterCreatorBase
    {
        public string Name { get; private set; }

        public NamedFilterCreator(string name, Action<IBaseFilter> postAction = null)
            : base(postAction)
        {
            Name = name;
        }
    }
}
