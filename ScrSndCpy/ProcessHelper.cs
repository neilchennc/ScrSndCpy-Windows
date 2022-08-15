using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrSndCpy
{
    internal class ProcessHelper
    {
        public static Process Create(string filename, string arguments)
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = filename;
            p.StartInfo.Arguments = arguments;
            return p;
        }
    }
}
