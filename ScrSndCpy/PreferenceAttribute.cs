using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrSndCpy
{
    public struct PreferenceAttribute
    {
        public int MaxSize;
        public int Bitrate;
        public int MaxFps;

        public bool Borderless;
        public bool AlwaysOnTop;
        public bool Fullscreen;
        public bool NoControl;
        public bool StayAwake;
        public bool TurnScreenOff;
        public bool NoPowerOn;
        public bool PowerOffOnClose;
        public bool ShowTouches;
        public bool NoKeyRepeat;
    }
}
