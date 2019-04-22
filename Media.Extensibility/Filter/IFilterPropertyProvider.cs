using System;

namespace Media.Extensibility.Filter
{
    public interface IFilterPropertyProvider
    {
        string GetName(FilterType filterType);

        Guid GetGuid(FilterType filterType);

        Guid GetGuid(string filterName);

        FilterType GetFilterType(string filterName);
    }
}
