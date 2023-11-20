using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrSndCpy
{
    delegate void DeviceEventHandler(List<string> devices);
    delegate void ConnectionEventHandler();

    internal interface IDeviceMonitor
    {
        /// <summary>
        /// Event handler when device list changed.
        /// </summary>
        event DeviceEventHandler OnDeviceChanged;

        /// <summary>
        /// Event handler when disconnecting from the ADB server.
        /// </summary>
        event ConnectionEventHandler OnConnectionLost;

        /// <summary>
        /// Start monitoring device list changed.
        /// </summary>
        void Start();

        /// <summary>
        /// Stop monitoring device list changed.
        /// </summary>
        void Stop();
    }
}
