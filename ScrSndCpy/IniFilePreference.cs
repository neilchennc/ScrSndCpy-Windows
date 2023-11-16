using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ScrSndCpy
{
    internal class IniFilePreference : IPreference
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        const string PREFERENCE_FILE_NAME = "ScrSndCpy.ini";
        const string PREFERENCE_SECTION = "ScrSndCpy";
        const string PREFERENCE_KEY_MAX_SIZE = "MaxSize";
        const string PREFERENCE_KEY_BITRATE = "Bitrate";
        const string PREFERENCE_KEY_MAX_FPS = "MaxFps";
        const string PREFERENCE_KEY_BORDERLESS = "Borderless";
        const string PREFERENCE_KEY_ALWAYS_ON_TOP = "AlwaysOnTop";
        const string PREFERENCE_KEY_FULLSCREEN = "Fullscreen";
        const string PREFERENCE_KEY_NO_CONTROL = "NoControl";
        const string PREFERENCE_KEY_STAY_AWAKE = "StayAwake";
        const string PREFERENCE_KEY_TURN_SCREEN_OFF = "TurnScreenOff";
        const string PREFERENCE_KEY_NO_POWER_ON = "NoPowerOn";
        const string PREFERENCE_KEY_POWER_OFF_ON_CLOSE = "PowerOffOnClose";
        const string PREFERENCE_KEY_SHOW_TOUCHES = "ShowTouches";
        const string PREFERENCE_KEY_NO_KEY_REPEAT = "NoKeyRepeat";

        const int MAX_STRING_CAPACITY = 16;

        private string IniFilePath;

        public IniFilePreference()
        {
            IniFilePath = new FileInfo(PREFERENCE_FILE_NAME).FullName;
        }

        public PreferenceAttribute LoadPreference()
        {
            var attribute = new PreferenceAttribute();
            var retValue = new StringBuilder(MAX_STRING_CAPACITY);

            retValue.Clear();
            GetPrivateProfileString(PREFERENCE_SECTION, PREFERENCE_KEY_MAX_SIZE, "0", retValue, MAX_STRING_CAPACITY, IniFilePath);
            int.TryParse(retValue.ToString(), out attribute.MaxSize);

            retValue.Clear();
            GetPrivateProfileString(PREFERENCE_SECTION, PREFERENCE_KEY_BITRATE, "0", retValue, MAX_STRING_CAPACITY, IniFilePath);
            int.TryParse(retValue.ToString(), out attribute.Bitrate);

            retValue.Clear();
            GetPrivateProfileString(PREFERENCE_SECTION, PREFERENCE_KEY_MAX_FPS, "0", retValue, MAX_STRING_CAPACITY, IniFilePath);
            int.TryParse(retValue.ToString(), out attribute.MaxFps);

            retValue.Clear();
            GetPrivateProfileString(PREFERENCE_SECTION, PREFERENCE_KEY_BORDERLESS, "0", retValue, MAX_STRING_CAPACITY, IniFilePath);
            attribute.Borderless = (retValue.Length > 0 && retValue[0] != '0');

            retValue.Clear();
            GetPrivateProfileString(PREFERENCE_SECTION, PREFERENCE_KEY_ALWAYS_ON_TOP, "0", retValue, MAX_STRING_CAPACITY, IniFilePath);
            attribute.AlwaysOnTop = (retValue.Length > 0 && retValue[0] != '0');

            retValue.Clear();
            GetPrivateProfileString(PREFERENCE_SECTION, PREFERENCE_KEY_FULLSCREEN, "0", retValue, MAX_STRING_CAPACITY, IniFilePath);
            attribute.Fullscreen = (retValue.Length > 0 && retValue[0] != '0');

            retValue.Clear();
            GetPrivateProfileString(PREFERENCE_SECTION, PREFERENCE_KEY_NO_CONTROL, "0", retValue, MAX_STRING_CAPACITY, IniFilePath);
            attribute.NoControl = (retValue.Length > 0 && retValue[0] != '0');

            retValue.Clear();
            GetPrivateProfileString(PREFERENCE_SECTION, PREFERENCE_KEY_STAY_AWAKE, "0", retValue, MAX_STRING_CAPACITY, IniFilePath);
            attribute.StayAwake = (retValue.Length > 0 && retValue[0] != '0');

            retValue.Clear();
            GetPrivateProfileString(PREFERENCE_SECTION, PREFERENCE_KEY_TURN_SCREEN_OFF, "0", retValue, MAX_STRING_CAPACITY, IniFilePath);
            attribute.TurnScreenOff = (retValue.Length > 0 && retValue[0] != '0');

            retValue.Clear();
            GetPrivateProfileString(PREFERENCE_SECTION, PREFERENCE_KEY_NO_POWER_ON, "0", retValue, MAX_STRING_CAPACITY, IniFilePath);
            attribute.NoPowerOn = (retValue.Length > 0 && retValue[0] != '0');

            retValue.Clear();
            GetPrivateProfileString(PREFERENCE_SECTION, PREFERENCE_KEY_POWER_OFF_ON_CLOSE, "0", retValue, MAX_STRING_CAPACITY, IniFilePath);
            attribute.PowerOffOnClose = (retValue.Length > 0 && retValue[0] != '0');

            retValue.Clear();
            GetPrivateProfileString(PREFERENCE_SECTION, PREFERENCE_KEY_SHOW_TOUCHES, "0", retValue, MAX_STRING_CAPACITY, IniFilePath);
            attribute.ShowTouches = (retValue.Length > 0 && retValue[0] != '0');

            retValue.Clear();
            GetPrivateProfileString(PREFERENCE_SECTION, PREFERENCE_KEY_NO_KEY_REPEAT, "0", retValue, MAX_STRING_CAPACITY, IniFilePath);
            attribute.NoKeyRepeat = (retValue.Length > 0 && retValue[0] != '0');

            return attribute;
        }

        public void SavePreference(PreferenceAttribute attribute)
        {
            int temp;

            WritePrivateProfileString(PREFERENCE_SECTION, PREFERENCE_KEY_MAX_SIZE, $"{attribute.MaxSize}", IniFilePath);
            WritePrivateProfileString(PREFERENCE_SECTION, PREFERENCE_KEY_BITRATE, $"{attribute.Bitrate}", IniFilePath);
            WritePrivateProfileString(PREFERENCE_SECTION, PREFERENCE_KEY_MAX_FPS, $"{attribute.MaxFps}", IniFilePath);

            temp = attribute.Borderless ? 1 : 0;
            WritePrivateProfileString(PREFERENCE_SECTION, PREFERENCE_KEY_BORDERLESS, $"{temp}", IniFilePath);
            temp = attribute.AlwaysOnTop ? 1 : 0;
            WritePrivateProfileString(PREFERENCE_SECTION, PREFERENCE_KEY_ALWAYS_ON_TOP, $"{temp}", IniFilePath);
            temp = attribute.Fullscreen ? 1 : 0;
            WritePrivateProfileString(PREFERENCE_SECTION, PREFERENCE_KEY_FULLSCREEN, $"{temp}", IniFilePath);
            temp = attribute.NoControl ? 1 : 0;
            WritePrivateProfileString(PREFERENCE_SECTION, PREFERENCE_KEY_NO_CONTROL, $"{temp}", IniFilePath);
            temp = attribute.StayAwake ? 1 : 0;
            WritePrivateProfileString(PREFERENCE_SECTION, PREFERENCE_KEY_STAY_AWAKE, $"{temp}", IniFilePath);
            temp = attribute.TurnScreenOff ? 1 : 0;
            WritePrivateProfileString(PREFERENCE_SECTION, PREFERENCE_KEY_TURN_SCREEN_OFF, $"{temp}", IniFilePath);
            temp = attribute.NoPowerOn ? 1 : 0;
            WritePrivateProfileString(PREFERENCE_SECTION, PREFERENCE_KEY_NO_POWER_ON, $"{temp}", IniFilePath);
            temp = attribute.PowerOffOnClose ? 1 : 0;
            WritePrivateProfileString(PREFERENCE_SECTION, PREFERENCE_KEY_POWER_OFF_ON_CLOSE, $"{temp}", IniFilePath);
            temp = attribute.ShowTouches ? 1 : 0;
            WritePrivateProfileString(PREFERENCE_SECTION, PREFERENCE_KEY_SHOW_TOUCHES, $"{temp}", IniFilePath);
            temp = attribute.NoKeyRepeat ? 1 : 0;
            WritePrivateProfileString(PREFERENCE_SECTION, PREFERENCE_KEY_NO_KEY_REPEAT, $"{temp}", IniFilePath);
        }
    }
}
