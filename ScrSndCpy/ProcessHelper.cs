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
        /// <summary>
        /// Create a invisible process.
        /// </summary>
        public static Process Create(string filename, string arguments, bool redirectStandardOutput = false, bool redirectStandardError = false)
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = redirectStandardOutput;
            p.StartInfo.RedirectStandardError = redirectStandardError;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = filename;
            p.StartInfo.Arguments = arguments;
            return p;
        }
    }
}
