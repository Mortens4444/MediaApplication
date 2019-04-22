using Media.Extensibility.Media;
using Media.Extensibility.Media.Connection;
using Media.Extensibility.Media.Connection.Ports;
using System.Collections.Generic;

namespace MediaApplication.Media
{
    public class ConnectionProvider : IConnectionProvider
    {
        public IOutConnection GetAviPlayerConnection()
        {
            var fileSourceOutput = new OutputPort("Output");
            var fileSource = new OutConnection("File Source (Async.)")
            {
                OutputPins = new List<OutputPort> { fileSourceOutput }
            };

            var aviSplitterInput = new InputPort("input pin");
            var aviSplitterVideoStream = new OutputPort("Stream 00");
            var aviSplitterAudioStream = new OutputPort("Stream 01");
            var aviSplitter = new InOutConnection("AVI Splitter")
            {
                InputPins = new List<InputPort> { aviSplitterInput },
                OutputPins = new List<OutputPort> { aviSplitterVideoStream, aviSplitterAudioStream }
            };

            fileSourceOutput.ConnectedPort = aviSplitterInput;

            var mpegDecoderDmoInput = new InputPort("in0");
            var mpegDecoderDmoOutput = new OutputPort("out0");
            var mpegDecoderDmo = new InOutConnection("Mpeg4s Decoder DMO")
            {
                InputPins = new List<InputPort> { mpegDecoderDmoInput },
                OutputPins = new List<OutputPort> { mpegDecoderDmoOutput }
            };

            aviSplitterVideoStream.ConnectedPort = mpegDecoderDmoInput;

            var videoRendererInput = new InputPort("VMR Input0");
            var videoRenderer = new InOutConnection("Video Renderer")
            {
                InputPins = new List<InputPort> { videoRendererInput }
            };

            mpegDecoderDmoOutput.ConnectedPort = videoRendererInput;

            var audioRendererInput = new InputPort("Audio Input pin (rendered)");
            var audioRenderer = new InOutConnection("Default WaveOut Device")
            {
                InputPins = new List<InputPort> { audioRendererInput }
            };

            aviSplitterAudioStream.ConnectedPort = audioRendererInput;

            return fileSource;
        }

        /// <summary>
        /// This function provides a connection map, to automatize the following instructions:
        /// generalGraphBuilder.ManualConnect("HP Truevision HD", "Rögzítés", "Smart Tee", "Input");
        /// generalGraphBuilder.ManualConnect("Smart Tee", "Capture", "AVI Mux", "Input 01");
        /// generalGraphBuilder.ManualConnect("AVI Mux", "AVI Out", "File Writer", "in");
        /// </summary>
        /// <returns></returns>
        public IOutConnection GetHpRecorderConnection()
        {
            var hpCameraRecord = new OutputPort("Rögzítés");
            var hpCamera = new OutConnection("HP Truevision HD")
            {
                OutputPins = new List<OutputPort> { hpCameraRecord }
            };

            var smartTeeInput = new InputPort("Input");
            var smartTeeCapture = new OutputPort("Capture");
            var smartTee = new InOutConnection("Smart Tee")
            {
                InputPins = new List<InputPort> { smartTeeInput },
                OutputPins = new List<OutputPort> { smartTeeCapture }
            };

            hpCameraRecord.ConnectedPort = smartTeeInput;

            var aviMuxInput = new InputPort("Input 01");
            var aviMuxOutput = new OutputPort("AVI Out");
            var aviMux = new InOutConnection("AVI Mux")
            {
                InputPins = new List<InputPort> { aviMuxInput },
                OutputPins = new List<OutputPort> { aviMuxOutput }
            };

            smartTeeCapture.ConnectedPort = aviMuxInput;

            var fileWriterInput = new InputPort("in");
            var fileWriter = new InConnection("File Writer")
            {
                InputPins = new List<InputPort> { fileWriterInput }
            };

            aviMuxOutput.ConnectedPort = fileWriterInput;

            return hpCamera;
        }

        public IOutConnection GetMpegPlayerConnection()
        {
            var fileSourceOutput = new OutputPort("Output");
            var fileSource = new OutConnection("File Source (Async.)")
            {
                OutputPins = new List<OutputPort> { fileSourceOutput }
            };

            var mpeg2DemultiplexerInput = new InputPort("MPEG-2 Stream");
            var mpeg2DemultiplexerVideoOut = new OutputPort("Video");
            var mpeg2DemultiplexerAc3Out = new OutputPort("AC-3");
            var mpeg2Demultiplexer = new InOutConnection("MPEG-2 Demultiplexer")
            {
                InputPins = new List<InputPort> { mpeg2DemultiplexerInput },
                OutputPins = new List<OutputPort> { mpeg2DemultiplexerVideoOut, mpeg2DemultiplexerAc3Out }
            };

            fileSourceOutput.ConnectedPort = mpeg2DemultiplexerInput;

            var ffdshowVideoDecoderInput = new InputPort("In");
            var ffdshowVideoDecoderOutput = new OutputPort("Out");
            var ffdshowVideoDecoder = new InOutConnection("ffdshow Video Decoder")
            {
                InputPins = new List<InputPort> { ffdshowVideoDecoderInput },
                OutputPins = new List<OutputPort> { ffdshowVideoDecoderOutput }
            };

            mpeg2DemultiplexerVideoOut.ConnectedPort = ffdshowVideoDecoderInput;

            var videoRendererInput = new InputPort("VMR Input0");
            var videoRenderer = new InOutConnection("Video Renderer")
            {
                InputPins = new List<InputPort> { videoRendererInput }
            };

            ffdshowVideoDecoderOutput.ConnectedPort = videoRendererInput;

            var ffdshowAudioDecoderInput = new InputPort("In");
            var ffdshowAudioDecoderOutput = new OutputPort("Out");
            var ffdshowAudioDecoder = new InOutConnection("ffdshow Audio Decoder")
            {
                InputPins = new List<InputPort> { ffdshowAudioDecoderInput }
            };

            mpeg2DemultiplexerAc3Out.ConnectedPort = ffdshowAudioDecoderInput;

            var audioRendererInput = new InputPort("Audio Input pin (rendered)");
            var audioRenderer = new InConnection("Default DirectSound Device")
            {
                InputPins = new List<InputPort> { audioRendererInput }
            };

            ffdshowAudioDecoderOutput.ConnectedPort = audioRendererInput;

            return fileSource;
        }
    }
}
