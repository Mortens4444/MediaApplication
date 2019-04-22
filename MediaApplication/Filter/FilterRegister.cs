using DirectShowLib;
using Media.Extensibility.Filter;
using Media.Extensibility.Filter.FilterCreator;
using System.Collections.Generic;

namespace MediaApplication.Filter
{
    public class FilterRegister : IFilterRegister
    {
        public Dictionary<string, IBaseFilter> Filters => filters;

        private readonly Dictionary<string, IBaseFilter> filters = new Dictionary<string, IBaseFilter>();

        private readonly FilterGraph filterGraph;
        private readonly IFilterPropertyProvider filterPropertyProvider;
        private readonly IFilterProvider filterProvider;

        public FilterRegister(FilterGraph filterGraph,
            IFilterPropertyProvider filterPropertyProvider,
            IFilterProvider filterProvider)
        {
            this.filterGraph = filterGraph;
            this.filterPropertyProvider = filterPropertyProvider;
            this.filterProvider = filterProvider;
        }

        public IBaseFilter this[string filterName]
        {
            get
            {
                return Filters[filterName];
            }
        }

        public IBaseFilter CreateFilter(FilterCreatorBase filterCreatorBase)
        {
            if (filterCreatorBase is BaseFilterCreator)
            {
                return CreateFilter(filterCreatorBase as BaseFilterCreator);
            }
            if (filterCreatorBase is TypedFilterCreator)
            {
                return CreateFilter(filterCreatorBase as TypedFilterCreator);
            }
            return CreateFilter(filterCreatorBase as NamedFilterCreator);
        }

        private IBaseFilter CreateFilter(BaseFilterCreator baseFilterCreator)
        {
            AddFilter(baseFilterCreator.BaseFilter, baseFilterCreator.Name);
            return baseFilterCreator.BaseFilter;
        }

        private IBaseFilter CreateFilter(TypedFilterCreator typedFilterCreator)
        {
            var filter = filterProvider.CreateFilter(typedFilterCreator.Type);
            var filterName = filterPropertyProvider.GetName(typedFilterCreator.Type);
            AddFilter(filter, filterName);
            return filter;
        }

        private IBaseFilter CreateFilter(NamedFilterCreator namedFilterCreator)
        {
            var filter = filterProvider.CreateFilterByName(namedFilterCreator.Name);
            AddFilter(filter, namedFilterCreator.Name);
            return filter;
        }

        private void AddFilter(IBaseFilter filter, string filterName)
        {
            ((IGraphBuilder)filterGraph).AddFilter(filter, filterName);
            Filters.Add(filterName, filter);
        }
    }
}
