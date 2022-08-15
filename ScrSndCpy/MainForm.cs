using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScrSndCpy
{
    public partial class MainForm : Form
    {
        private const string SCRCPY_FILE = "scrcpy-direct.bat";
        private const string SNDCPY_FILE = "sndcpy-direct.bat";

        private TaskScheduler uiContext;
        private readonly BindingList<string> connectedDevices = new BindingList<string>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            uiContext = TaskScheduler.FromCurrentSynchronizationContext();
            ListBoxDevices.DataSource = connectedDevices;

            UpdateDeviceList();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void UpdateDeviceList()
        {
            Task.Factory.StartNew(() =>
            {
                while (!IsDisposed)
                {
                    var newDevices = GetDeviceList();
                    if (!newDevices.SequenceEqual(connectedDevices))
                    {
                        DispatchUiAction(() =>
                        {
                            connectedDevices.Clear();
                            newDevices.ForEach(d => connectedDevices.Add(d));
                            ListBoxDevices.SelectedIndex = -1;
                        });
                    }
                    Thread.Sleep(1000);
                }
            });
        }

        /// <summary>
        /// Get connected devices from ADB.
        /// </summary>
        private List<string> GetDeviceList()
        {
            var p = ProcessHelper.Create("adb.exe", "devices");
            p.Start();

            // Read the output stream first and then wait.
            string output = p.StandardOutput.ReadToEnd();

            // Wait for the child process to exit
            p.WaitForExit();

            Debug.WriteLine(output);
            var list = new List<string>();
            var lines = output.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length > 1 && lines[0] == "List of devices attached")
            {
                lines.Skip(1).ToList().ForEach(line =>
                {
                    var devices = line.Split(new char[] { '\t' }, StringSplitOptions.None);
                    list.Add(devices[0]);
                });
            }

            return list;
        }

        /// <summary>
        /// Dispatch a task to be executed on UI thread.
        /// </summary>
        private void DispatchUiAction(Action action)
        {
            Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None, uiContext);
        }

        private void CastButton_Click(object sender, EventArgs e)
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

                // Start scrcpy
                TextBoxLog.AppendText($"Start {SCRCPY_FILE} {argumentString}");
                TextBoxLog.AppendText(Environment.NewLine);
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        // Check if device is exists in the connected list
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
                                TextBoxLog.AppendText($"Connect to {serial}...");
                                TextBoxLog.AppendText(Environment.NewLine);
                            });
                            var adbConnect = ProcessHelper.Create("adb.exe", $"connect {serial}");
                            adbConnect.Start();
                            adbConnect.WaitForExit();
                        }
                    }
                    catch
                    {
                        Debug.WriteLine("NOT IP Address format, ignored");
                    }

                    var scrcpy = ProcessHelper.Create(SCRCPY_FILE, argumentString.ToString());
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
                    var stopSndcpy = ProcessHelper.Create("adb.exe", $"-s {serial} shell am force-stop com.rom1v.sndcpy");
                    stopSndcpy.Start();
                    stopSndcpy.WaitForExit();

                    TextBoxLog.AppendText("sndcpy stopped.");
                    TextBoxLog.AppendText(Environment.NewLine);
                });

                // Start sndcpy
                TextBoxLog.AppendText($"Start command: {SNDCPY_FILE} {serial}");
                TextBoxLog.AppendText(Environment.NewLine);
                Task.Factory.StartNew(() =>
                {
                    var p = ProcessHelper.Create(SNDCPY_FILE, serial);
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
    }
}
