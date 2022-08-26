using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScrSndCpy
{
    public partial class MainForm : Form
    {
        private const string ADB_FILE = "adb.exe";
        private const string SCRCPY_FILE = "scrcpy.exe";
        private const string SNDCPY_FILE = "sndcpy-direct.bat";

        private TaskScheduler uiContext;
        private DeviceMonitor deviceMonitor;
        private readonly BindingList<string> connectedDevices = new BindingList<string>();

        public MainForm()
        {
            InitializeComponent();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            uiContext = TaskScheduler.FromCurrentSynchronizationContext();
            ListBoxDevices.DataSource = connectedDevices;

            await Task.Factory.StartNew(() => { CheckScrcpyVersion(); });
        }

        private void CheckScrcpyVersion()
        {
            try
            {
                // Check scrcpy version
                var pCheckScrcpy = ProcessHelper.Create(SCRCPY_FILE, "-v", redirectStandardOutput: true);
                pCheckScrcpy.Start();
                string info = pCheckScrcpy.StandardOutput.ReadToEnd();
                pCheckScrcpy.WaitForExit();
                DispatchUiAction(() => TextBoxLog.AppendText(info));

                // Start tracking devices
                deviceMonitor = new DeviceMonitor();
                deviceMonitor.OnDeviceChanged += DeviceMonitor_OnDeviceChanged;
                deviceMonitor.OnConnectionLost += DeviceMonitor_OnConnectionLost;
                deviceMonitor.Start();
            }
            catch
            {
                DispatchUiAction(() =>
                {
                    TextBoxLog.AppendText("Failed to check scrcpy version.");
                    ButtonPlay.Enabled = false;
                });
            }
        }

        private void DeviceMonitor_OnDeviceChanged(List<string> devices)
        {
            DispatchUiAction(() =>
            {
                connectedDevices.Clear();
                devices.ForEach(d => connectedDevices.Add(d));
                ListBoxDevices.SelectedIndex = -1;
            });
        }

        private void DeviceMonitor_OnConnectionLost()
        {
            // Cleanup device list
            DispatchUiAction(() =>
            {
                connectedDevices.Clear();
                TextBoxLog.AppendText("Connection lost, restart daemon now...");
                TextBoxLog.AppendText(Environment.NewLine);
            });

            // Restart server
            var pStartServer = ProcessHelper.Create(ADB_FILE, "start-server", redirectStandardError: true);
            pStartServer.Start();
            while (!pStartServer.HasExited)
            {
                var output = pStartServer.StandardError.ReadLine();
                DispatchUiAction(() => TextBoxLog.AppendText($"{output}{Environment.NewLine}"));
            }
            pStartServer.WaitForExit();

            // Restart
            deviceMonitor.Start();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (deviceMonitor != null)
            {
                deviceMonitor.OnConnectionLost -= DeviceMonitor_OnConnectionLost;
                deviceMonitor.OnDeviceChanged -= DeviceMonitor_OnDeviceChanged;
                deviceMonitor.Stop();
            }
        }

        private async void ButtonPlay_Click(object sender, EventArgs e)
        {
            if (TextBoxDevice.Text.Trim().Length > 0)
            {
                var serial = TextBoxDevice.Text;

                var argumentString = new StringBuilder();
                argumentString.Append($" -s {serial}");

                if (TextBoxMaxSize.Text.Trim().Length > 0)
                    argumentString.Append($" --max-size={TextBoxMaxSize.Text}");
                if (TextBoxBitRate.Text.Trim().Length > 0)
                    argumentString.Append($" --bit-rate={TextBoxBitRate.Text}M");
                if (TextBoxMaxFps.Text.Trim().Length > 0)
                    argumentString.Append($" --max-fps={TextBoxMaxFps.Text}");
                if (CheckBoxBorderless.Checked)
                    argumentString.Append(" --window-borderless");
                if (CheckBoxAlwaysOnTop.Checked)
                    argumentString.Append(" --always-on-top");
                if (CheckBoxFullscreen.Checked)
                    argumentString.Append(" --fullscreen");
                if (CheckBoxNoControl.Checked)
                    argumentString.Append(" --no-control");
                if (CheckBoxStayAwake.Checked)
                    argumentString.Append(" --stay-awake");
                if (CheckBoxTurnScreenOff.Checked)
                    argumentString.Append(" --turn-screen-off");
                if (CheckBoxNoPowerOnStart.Checked)
                    argumentString.Append(" --no-power-on");
                if (CheckBoxPowerOffClose.Checked)
                    argumentString.Append(" --power-off-on-close");

                // Connect and check state
                var errorMessage = await Task.Factory.StartNew(() =>
                {
                    // Connect to the device if IP address is present
                    try
                    {
                        // Check if device is already existing in the connected list
                        if (!connectedDevices.Contains(serial))
                        {
                            // Check IP address format
                            var parts = serial.Split(':');
                            var ip = IPAddress.Parse(parts[0]);
                            var port = 5555;
                            if (parts.Length > 1)
                            {
                                port = UInt16.Parse(parts[1]);
                            }

                            // It's IP address format, connect to it
                            DispatchUiAction(() =>
                            {
                                TextBoxLog.AppendText($"Connecting to {serial}...");
                                TextBoxLog.AppendText(Environment.NewLine);
                            });
                            var adbConnect = ProcessHelper.Create(ADB_FILE, $"connect {serial}");
                            adbConnect.Start();
                            adbConnect.WaitForExit();
                        }
                    }
                    catch
                    {
                        Debug.WriteLine("NOT IP Address format, ignored");
                    }

                    // Check ADB state
                    var pAdbGetState = ProcessHelper.Create(ADB_FILE, $"-s {serial} get-state", redirectStandardError: true);
                    pAdbGetState.Start();
                    var errorOutput = pAdbGetState.StandardError.ReadToEnd();
                    pAdbGetState.WaitForExit();

                    return (pAdbGetState.ExitCode == 0 ? null : errorOutput);
                });

                // Show error messages if errors occured
                if (errorMessage != null)
                {
                    TextBoxLog.AppendText(errorMessage);

                    // Device unauthorized
                    if (errorMessage.StartsWith("error: device unauthorized."))
                    {
                        TextBoxLog.AppendText("After confirming with the dialog, please try again.");
                        TextBoxLog.AppendText(Environment.NewLine);
                    }
                    return;
                }

                // Start scrcpy
                TextBoxLog.AppendText($"Run command: {SCRCPY_FILE} {argumentString}");
                TextBoxLog.AppendText(Environment.NewLine);
                var scrcpyTask = Task.Factory.StartNew(() =>
                {
                    var scrcpy = ProcessHelper.Create(SCRCPY_FILE, argumentString.ToString(), redirectStandardOutput: true);
                    scrcpy.Start();

                    while (!scrcpy.HasExited)
                    {
                        var message = scrcpy.StandardOutput.ReadLine();
                        DispatchUiAction(() =>
                        {
                            TextBoxLog.AppendText(message);
                            TextBoxLog.AppendText(Environment.NewLine);
                        });
                    }

                    TextBoxLog.AppendText("scrcpy stopped.");
                    TextBoxLog.AppendText(Environment.NewLine);

                    // Terminate sndcpy
                    var stopSndcpy = ProcessHelper.Create(ADB_FILE, $"-s {serial} shell am force-stop com.rom1v.sndcpy");
                    stopSndcpy.Start();
                    stopSndcpy.WaitForExit();
                });

                // Start sndcpy
                TextBoxLog.AppendText($"Run command: {SNDCPY_FILE} {serial}");
                TextBoxLog.AppendText(Environment.NewLine);
                var sndcpyTask = Task.Factory.StartNew(() =>
                {
                    var p = ProcessHelper.Create(SNDCPY_FILE, serial, redirectStandardOutput: true);
                    p.Start();

                    while (!p.HasExited)
                    {
                        var message = p.StandardOutput.ReadLine();
                        DispatchUiAction(() =>
                        {
                            TextBoxLog.AppendText(message);
                            TextBoxLog.AppendText(Environment.NewLine);
                        });
                    }

                    TextBoxLog.AppendText("sndcpy stopped.");
                    TextBoxLog.AppendText(Environment.NewLine);
                });
            }
        }

        private void ListBoxDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListBoxDevices.SelectedItem != null)
            {
                TextBoxDevice.Text = ListBoxDevices.SelectedItem.ToString();
            }
        }

        /// <summary>
        /// Dispatch a task to be executed on UI thread.
        /// </summary>
        private void DispatchUiAction(Action action)
        {
            Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None, uiContext);
        }
    }
}
