using DirectShowLib;
using System;

namespace Media.Extensibility.Filter.FilterCreator
{
    public class FilterCreatorBase
    {
        public Action<IBaseFilter> PostAction { get; private set; }

        public FilterCreatorBase(Action<IBaseFilter> postAction)
        {
            PostAction = postAction;
        }
    }
}