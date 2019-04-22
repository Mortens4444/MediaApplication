using DirectShowLib;
using Media.Extensibility.Filter.FilterCreator;
using System.Collections.Generic;

namespace Media.Extensibility.Filter
{
    public interface IFilterRegister
    {
        IBaseFilter this[string filterName] { get; }

        Dictionary<string, IBaseFilter> Filters { get; }

        IBaseFilter CreateFilter(FilterCreatorBase filterCreatorBase);

        //IBaseFilter CreateFilter(BaseFilterCreator baseFilterCreator);

        //IBaseFilter CreateFilter(TypedFilterCreator typedFilterCreator);

        //IBaseFilter CreateFilter(NamedFilterCreator namedFilterCreator);
    }
}
