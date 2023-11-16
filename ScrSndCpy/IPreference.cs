using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrSndCpy
{
    internal interface IPreference
    {
        PreferenceAttribute LoadPreference();
        void SavePreference(PreferenceAttribute attribute);
    }
}
