using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ScrSndCpy
{
    internal class DeviceMonitor
    {
        public delegate void DeviceEventHandler(List<string> devices);
        public delegate void ConnectionEventHandler();

        public event DeviceEventHandler OnDeviceChanged;
        public event ConnectionEventHandler OnConnectionLost;

        private TcpClient tcpClient;
        private NetworkStream networkStream;

        public void Start()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    tcpClient = new TcpClient("127.0.0.1", 5037);
                    networkStream = tcpClient.GetStream();
                    byte[] data = System.Text.Encoding.ASCII.GetBytes("0012host:track-devices");
                    networkStream.Write(data, 0, data.Length);
                    data = new byte[1024];
                    string responseData = string.Empty;

                    while (tcpClient.Connected)
                    {
                        int bytes = networkStream.Read(data, 0, data.Length);
                        ParseData(data, bytes);
                    }

                    networkStream.Close();
                    tcpClient.Close();
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Socket exception: {e}");
                }
                finally
                {
                    // Cleanup
                    networkStream?.Close();
                    networkStream = null;
                    tcpClient?.Close();
                    tcpClient = null;
                }

                OnConnectionLost?.Invoke();
            });
        }

        private void ParseData(byte[] data, int bytes)
        {
            var responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Debug.WriteLine("Parsing data:");
            Debug.WriteLine(responseData);

            if ("OKAY" == responseData)
            {
                return;
            }
            if ("0000" == responseData || "OKAY0000" == responseData)
            {
                OnDeviceChanged?.Invoke(new List<string>(0));
                return;
            }

            int index = 0;
            while (index < bytes)
            {
                var devices = new List<string>();
                bool isHexValue = int.TryParse(responseData.Substring(index, 4), System.Globalization.NumberStyles.HexNumber, null, out int payloadLength);
                var payloadData = responseData.Substring(index + 4, payloadLength - 1); // exclude last character '\n'
                Debug.WriteLine($"payloadLength: {payloadLength}, payloadData: {payloadData}");
                var lines = payloadData.Split('\n');
                foreach (var line in lines)
                {
                    var parts = line.Split('\t');
                    if (parts.Length == 2)
                    {
                        devices.Add(parts[0]);
                    }
                    else
                    {
                        Debug.WriteLine($"Unexpected payload data: {line}");
                    }
                }
                index = index + 4 + payloadLength;
                OnDeviceChanged?.Invoke(devices);
            }
        }

        public void Stop()
        {
            // Stream
            try
            {
                networkStream?.Close();
                tcpClient?.Close();
            }
            catch
            {
                // ignore any exceptions
            }
        }
    }
}
