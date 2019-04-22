using Media.Extensibility.Filter.FilterCreator;
using System.Collections.Generic;

namespace Media.Extensibility.Media
{
    public interface IConnectedFilterProvider
    {
        IEnumerable<FilterCreatorBase> GetHpPreviewFilters();

        IEnumerable<FilterCreatorBase> GetHpPreviewFilters2();

        IEnumerable<FilterCreatorBase> GetHpRecorderFilters(string pathToRecordedFile);

        IEnumerable<FilterCreatorBase> GetHpRecorderFilters2(string pathToRecordedFile);

        IEnumerable<FilterCreatorBase> GetDeprecatedAviPlayerGraph(string filePath);

        IEnumerable<FilterCreatorBase> GetAviPlayerFilters(string filePath);

        IEnumerable<FilterCreatorBase> GetMicrophoneFilters();

        IEnumerable<FilterCreatorBase> GetRtspFilters(string url);

        IEnumerable<FilterCreatorBase> GetSunellRtspFilters(string url);

        IEnumerable<FilterCreatorBase> GetDatasteadRtspRtmpHttpOnvifSourceEvaluationFiltersI(string url);

        IEnumerable<FilterCreatorBase> GetDatasteadRtspRtmpHttpOnvifSourceEvaluationFiltersII(string url);

        IEnumerable<FilterCreatorBase> GetMpegPlayerFilters(string filename);
    }
}
