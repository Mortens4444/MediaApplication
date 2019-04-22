using Media.Extensibility.Filter.FilterCreator;
using Media.Extensibility.Media.Connection;
using System.Collections.Generic;

namespace Media.Extensibility.Media
{
    public interface IGeneralGraphBuilder
    {
        void BuildGraphWithAutoConnect(IOutConnection outConnection);

        void BuildGraphWithAutoConnect(IEnumerable<FilterCreatorBase> filters);

        void RegisterFilters(IEnumerable<FilterCreatorBase> filterCreators);

        void ManualConnect(string outputFilter, string ouputPinName, string inputFilter, string inputPinName);
    }
}
