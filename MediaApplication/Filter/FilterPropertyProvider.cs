using Media.Extensibility.Filter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MediaApplication.Filter
{
    public class FilterPropertyProvider : IFilterPropertyProvider
    {
        private static readonly Dictionary<FilterType, (string Name, Guid Guid)> filterProperties
            = new Dictionary<FilterType, (string Name, Guid Guid)>()
        {
            {
                FilterType.AudioRenderer,
                (
                    Name: "Default WaveOut Device", // qedit.dll
                    Guid: new Guid("{E0F158E1-CB04-11D0-BD4E-00A0C911CE86}")
                )
            },
            {
                FilterType.ColorSpaceConverter,
                (
                    Name: "Color Space Converter",
                    Guid: new Guid("{1643E180-90F5-11CE-97D5-00AA0055595A}")
                )
            },
            {
                FilterType.SampleGrabber,
                (
                    Name: "SampleGrabber", // qedit.dll
                    Guid: new Guid("{C1F400A0-3F08-11D3-9F0B-006008039E37}")
                )
            },
            {
                FilterType.HpTruevisionHd,
                (
                    Name: "HP Truevision HD",
                    Guid: new Guid("{860BB310-5D01-11D0-BD3B-00A0C911CE86}")
                )
            },
            {
                FilterType.HpTruevisionHdRecord,
                (
                    Name: "HP Truevision HD 0003", // ksproxy.ax
                    Guid: new Guid("{17CCA71B-ECD7-11D0-B908-00A0C9223196}")
                )
            },
            {
                FilterType.Mpeg2Demultiplexer,
                (
                    Name: "MPEG-2 Demultiplexer",
                    Guid: new Guid("{AFB6C280-2C41-11D3-8A60-0000F81E0E4A}")
                )
            },
            {
                FilterType.Mpeg4sDecoderDmo,
                (
                    Name: "Mpeg4s Decoder DMO", // DMO
                    Guid: new Guid("{2A11BAE2-FE6E-4249-864B-9E9ED6E8DBC2}")
                )
            },
            {
                FilterType.Mpeg4sDecoderDmoCat,
                (
                    Name: "Mpeg4s Decoder DMO category", // DMO category
                    Guid: new Guid("{4A69B442-28BE-4991-969C-B500ADF5D8A8}")
                )
            },
            {
                FilterType.VideoRenderer,
                (
                    Name: "Video Renderer", // quartz.dll
                    Guid: new Guid("{6BC1CFFA-8FC1-4261-AC22-CFB4CC38DB50}")
                )
            },
            {
                FilterType.EnhancedVideoRenderer,
                (
                    Name: "Enhanced Video Renderer", // evr.dll
                    Guid: new Guid("{FA10746C-9B63-4B6C-BC49-FC300EA5F256}")
                )
            },
            {
                FilterType.Glow,
                (
                    Name: "Glow",
                    Guid: new Guid("{CC7BFB42-F175-11D1-A392-00E0291F3959}")
                )
            },
            {
                FilterType.VideoRenderer2,
                (
                    Name: "Video Renderer 2",
                    Guid: new Guid("{32595559-0000-0010-8000-00AA00389B71}")
                )
            },
            {
                FilterType.VideoRenderer3,
                (
                    Name: "Video Renderer 3",
                    Guid: new Guid("{70E102B0-5556-11CE-97C0-00AA0055595A}")
                )
            },
            {
                FilterType.AudioInput,
                (
                    Name: "Audio Input pin (rendered)",
                    Guid: new Guid("{73647561-0000-0010-8000-00AA00389B71}")
                )
            },
            {
                FilterType.DatasteadRtspRtmpHttpOnvifSourceEvaluation,
                (
                    Name: "Datastead RTSP/RTMP/HTTP/ONVIF Source (Evaluation)",
                    Guid: new Guid("{55D1139D-5E0D-4123-9AED-575D7B039569}")
                )
            },
            {
                FilterType.RtspSource,
                (
                    Name: "RTSP Source", // RTSP_Source.ax
                    Guid: new Guid("{CE2AF491-6527-4BAD-816F-94E53E760F44}")
                )
            },
            {
                FilterType.FFDShowVideoDecoder,
                (
                    Name: "ffdshow Video Decoder", // ffdshow                 
                    Guid: new Guid("{04FE9017-F873-410E-871E-AB91661A4EF7}")
                )
            },
            {
                FilterType.FFDShowAudioDecoder,
                (
                    Name: "ffdshow Audio Decoder", // ffdshow                 
                    Guid: new Guid("{0F40E1E5-4F79-4988-B1A9-CC98794E6B55}")
                )
            },
            {
                FilterType.DefaultDirectSoundDevice,
                (
                    Name: "Default DirectSound Device",
                    Guid: new Guid("{E0F158E1-CB04-11D0-BD4E-00A0C911CE86}")
                )
            },
            {
                FilterType.MicrophoneSource,
                (
                    Name: "Mikrofon (Realtek High Definition Audio)",
                    Guid: new Guid("{33D9A762-90C8-11D0-BD43-00A0C911CE86}")
                )
            }
        };

        public string GetName(FilterType filterType)
        {
            return filterProperties[filterType].Name;
        }

        public Guid GetGuid(FilterType filterType)
        {
            return filterProperties[filterType].Guid;
        }

        public Guid GetGuid(string filterName)
        {
            var filterKeyValuePair = filterProperties.First(filter => filter.Value.Name == filterName);
            return filterKeyValuePair.Value.Guid;
        }

        public FilterType GetFilterType(string filterName)
        {
            var filterKeyValuePair = filterProperties.First(filter => filter.Value.Name == filterName);
            return filterKeyValuePair.Key;
        }
    }
}
