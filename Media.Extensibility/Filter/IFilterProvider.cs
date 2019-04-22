using DirectShowLib;

namespace Media.Extensibility.Filter
{
    public interface IFilterProvider
    {
        IBaseFilter CreateFilter(FilterType filterType);

        IBaseFilter CreateFilterByName(string filterName);
    }
}
