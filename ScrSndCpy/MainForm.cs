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
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScrSndCpy
{
    public partial class MainForm : Form
    {
        private const string SCRSNDCPY_GITHUB_URL = "https://github.com/neilchennc/ScrSndCpy-Windows";
        private const string ADB_FILE = "adb.exe";
        private const string SCRCPY_FILE = "scrcpy.exe";

        private TaskScheduler uiContext;

        private readonly BindingList<string> connectedDevices = new BindingList<string>();

        private readonly IDeviceMonitor deviceMonitor = new DeviceMonitor();

        private readonly IPreference preference = new IniFilePreference();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            uiContext = TaskScheduler.FromCurrentSynchronizationContext();
            ListBoxDevices.DataSource = connectedDevices;
            SetDefaultValues();
            LoadPreference();
            ShowVersionAndRunAdb();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopDeviceMonitor();
            SavePreference();
        }

        private void SetDefaultValues()
        {
            var deviceMode = new Display.DeviceMode();
            int maxSize = 0;
            int maxFps = 0;
            foreach (var screen in Screen.AllScreens)
            {
                if (Display.EnumDisplaySettings(screen.DeviceName, Display.ENUM_CURRENT_SETTINGS, ref deviceMode) != 0)
                {
                    Debug.WriteLine($"screen: {screen.DeviceName}, width: {deviceMode.dmPelsWidth}, height: {deviceMode.dmPelsHeight}, frequency: {deviceMode.dmDisplayFrequency}");
                    maxSize = Math.Max(Math.Max(deviceMode.dmPelsWidth, deviceMode.dmPelsHeight), maxSize);
                    maxFps = Math.Max(deviceMode.dmDisplayFrequency, maxFps);
                }
            }
            Debug.WriteLine($"Max Size: {maxSize}, Max FPS: {maxFps}");
            TextBoxMaxSize.Text = $"{maxSize}";
            TextBoxMaxFps.Text = $"{maxFps}";
        }

        private void LoadPreference()
        {
            var preference = this.preference.LoadPreference();

            if (preference.MaxSize > 0)
            {
                TextBoxMaxSize.Text = $"{preference.MaxSize}";
            }
            if (preference.Bitrate > 0)
            {
                TextBoxBitRate.Text = $"{preference.Bitrate}";
            }
            if (preference.MaxFps > 0)
            {
                TextBoxMaxFps.Text = $"{preference.MaxFps}";
            }

            CheckBoxBorderless.Checked = preference.Borderless;
            CheckBoxAlwaysOnTop.Checked = preference.AlwaysOnTop;
            CheckBoxFullscreen.Checked = preference.Fullscreen;
            CheckBoxNoControl.Checked = preference.NoControl;
            CheckBoxStayAwake.Checked = preference.StayAwake;
            CheckBoxTurnScreenOff.Checked = preference.TurnScreenOff;
            CheckBoxNoPowerOnStart.Checked = preference.NoPowerOn;
            CheckBoxPowerOffClose.Checked = preference.PowerOffOnClose;
            CheckBoxShowTouches.Checked = preference.ShowTouches;
            CheckBoxNoKeyRepeat.Checked = preference.NoKeyRepeat;
        }

        private async void ShowVersionAndRunAdb()
        {
            await Task.Factory.StartNew(() =>
            {
                var messageBuilder = new StringBuilder(512);
                var isSuccess = true;

                // ScrSndCpy version
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                messageBuilder.AppendLine($"ScrSndCpy v{version.Major}.{version.Minor} <{SCRSNDCPY_GITHUB_URL}>");
                messageBuilder.AppendLine();

                try
                {
                    // scrcpy version
                    var pScrcpy = ProcessHelper.Create(SCRCPY_FILE, "-v", redirectStandardOutput: true);
                    pScrcpy.Start();
                    messageBuilder.AppendLine(pScrcpy.StandardOutput.ReadToEnd());
                    pScrcpy.WaitForExit();
                }
                catch
                {
                    messageBuilder.AppendLine("Failed to check scrcpy version.");
                    messageBuilder.AppendLine();
                    isSuccess = false;
                }

                try
                {
                    // adb version
                    var pAdb = ProcessHelper.Create(ADB_FILE, "--version", redirectStandardOutput: true);
                    pAdb.Start();
                    messageBuilder.AppendLine(pAdb.StandardOutput.ReadToEnd());
                    pAdb.WaitForExit();

                    // Start tracking devices
                    StartDeviceMonitor();
                }
                catch
                {
                    messageBuilder.AppendLine("Failed to check adb version.");
                    messageBuilder.AppendLine();
                    isSuccess = false;
                }

                // Show versions and enable/disable button
                DispatchUiAction(() =>
                {
                    ButtonPlay.Enabled = isSuccess;
                    TextBoxLog.AppendText(messageBuilder.ToString());
                });
            });
        }

        private void StartDeviceMonitor()
        {
            deviceMonitor.OnDeviceChanged += DeviceMonitor_OnDeviceChanged;
            deviceMonitor.OnConnectionLost += DeviceMonitor_OnConnectionLost;
            deviceMonitor.Start();
        }

        private void StopDeviceMonitor()
        {
            deviceMonitor.OnConnectionLost -= DeviceMonitor_OnConnectionLost;
            deviceMonitor.OnDeviceChanged -= DeviceMonitor_OnDeviceChanged;
            deviceMonitor.Stop();
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

        private void SavePreference()
        {
            var attribute = new PreferenceAttribute();
            int.TryParse(TextBoxMaxSize.Text, out attribute.MaxSize);
            int.TryParse(TextBoxBitRate.Text, out attribute.Bitrate);
            int.TryParse(TextBoxMaxFps.Text, out attribute.MaxFps);
            attribute.Borderless = CheckBoxBorderless.Checked;
            attribute.AlwaysOnTop = CheckBoxAlwaysOnTop.Checked;
            attribute.Fullscreen = CheckBoxFullscreen.Checked;
            attribute.NoControl = CheckBoxNoControl.Checked;
            attribute.StayAwake = CheckBoxStayAwake.Checked;
            attribute.TurnScreenOff = CheckBoxTurnScreenOff.Checked;
            attribute.NoPowerOn = CheckBoxNoPowerOnStart.Checked;
            attribute.PowerOffOnClose = CheckBoxPowerOffClose.Checked;
            attribute.ShowTouches = CheckBoxShowTouches.Checked;
            attribute.NoKeyRepeat = CheckBoxNoKeyRepeat.Checked;
            preference.SavePreference(attribute);
        }

        private async void ButtonPlay_Click(object sender, EventArgs e)
        {
            if (TextBoxDevice.Text.Trim().Length > 0)
            {
                // Connect and check state
                var errorMessage = await CheckConnectionStateAsync();

                // Show error messages if errors occured
                if (!string.IsNullOrEmpty(errorMessage))
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
                var exitCode = await StartScrcpyAsync();
                TextBoxLog.AppendText($"scrcpy stopped. ({exitCode})");
                TextBoxLog.AppendText(Environment.NewLine);
            }
        }

        private Task<string> CheckConnectionStateAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                var serial = TextBoxDevice.Text;

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
                            port = ushort.Parse(parts[1]);
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
        }

        private string GenerateScrcpyArgumentString()
        {
            var argumentString = new StringBuilder();

            argumentString.Append($" -s {TextBoxDevice.Text}");

            if (TextBoxMaxSize.Text.Trim().Length > 0)
                argumentString.Append($" --max-size={TextBoxMaxSize.Text}");
            if (TextBoxBitRate.Text.Trim().Length > 0)
                argumentString.Append($" --video-bit-rate={TextBoxBitRate.Text}M");
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
            if (CheckBoxShowTouches.Checked)
                argumentString.Append(" --show-touches");
            if (CheckBoxNoKeyRepeat.Checked)
                argumentString.Append(" --no-key-repeat");

            return argumentString.ToString();
        }

        private Task<int> StartScrcpyAsync()
        {
            var argumentString = GenerateScrcpyArgumentString();
            TextBoxLog.AppendText($"Run command: {SCRCPY_FILE} {argumentString}");
            TextBoxLog.AppendText(Environment.NewLine);

            return Task.Factory.StartNew(() =>
            {
                var scrcpy = ProcessHelper.Create(SCRCPY_FILE, argumentString, redirectStandardOutput: true);
                scrcpy.Start();

                while (!scrcpy.HasExited)
                {
                    var message = scrcpy.StandardOutput.ReadLine();
                    if (message != null)
                    {
                        DispatchUiAction(() =>
                        {
                            TextBoxLog.AppendText(message);
                            TextBoxLog.AppendText(Environment.NewLine);
                        });
                    }
                }

                return scrcpy.ExitCode;
            });
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
