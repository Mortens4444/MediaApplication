using DirectShowLib;
using System;

namespace Media.Extensibility.Filter.FilterCreator
{
    public class TypedFilterCreator : FilterCreatorBase
    {
        public FilterType Type { get; private set; }

        public TypedFilterCreator(FilterType type, Action<IBaseFilter> postAction = null)
            : base(postAction)
        {
            Type = type;
        }
    }
}
