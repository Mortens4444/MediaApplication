namespace Media.Extensibility.Media
{
    public interface ITypedGraphBuilder
    {
        void Stop();

        void BuildAviPlayer(string filename);

        void BuildDeprecatedAviPlayer(string filePath);

        void BuildHpCameraPreview();

        void BuildHpCameraPreview2();

        void BuildMicrophone();

        void BuildHpCameraRecorder(string filename);

        void BuildHpCameraRecorderWithConnectionProvider(string filename);

        void BuildRtsp(string url);

        void BuildSunellRtsp(string url);

        void BuildMpegPlayer(string filename);
    }
}
