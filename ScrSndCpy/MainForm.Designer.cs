namespace ScrSndCpy
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ListBoxDevices = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ButtonPlay = new System.Windows.Forms.Button();
            this.TextBoxLog = new System.Windows.Forms.TextBox();
            this.TextBoxDevice = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TextBoxMaxSize = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TextBoxBitRate = new System.Windows.Forms.TextBox();
            this.CheckBoxBorderless = new System.Windows.Forms.CheckBox();
            this.CheckBoxAlwaysOnTop = new System.Windows.Forms.CheckBox();
            this.CheckBoxFullscreen = new System.Windows.Forms.CheckBox();
            this.CheckBoxNoControl = new System.Windows.Forms.CheckBox();
            this.CheckBoxStayAwake = new System.Windows.Forms.CheckBox();
            this.CheckBoxTurnScreenOff = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TextBoxMaxFps = new System.Windows.Forms.TextBox();
            this.CheckBoxPowerOffClose = new System.Windows.Forms.CheckBox();
            this.CheckBoxNoPowerOnStart = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ListBoxDevices
            // 
            this.ListBoxDevices.FormattingEnabled = true;
            this.ListBoxDevices.ItemHeight = 15;
            this.ListBoxDevices.Location = new System.Drawing.Point(12, 27);
            this.ListBoxDevices.Name = "ListBoxDevices";
            this.ListBoxDevices.Size = new System.Drawing.Size(195, 139);
            this.ListBoxDevices.TabIndex = 4;
            this.ListBoxDevices.SelectedIndexChanged += new System.EventHandler(this.ListBoxDevices_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Devices:";
            // 
            // ButtonPlay
            // 
            this.ButtonPlay.Location = new System.Drawing.Point(12, 428);
            this.ButtonPlay.Name = "ButtonPlay";
            this.ButtonPlay.Size = new System.Drawing.Size(195, 23);
            this.ButtonPlay.TabIndex = 2;
            this.ButtonPlay.Text = "Play";
            this.ButtonPlay.UseVisualStyleBackColor = true;
            this.ButtonPlay.Click += new System.EventHandler(this.ButtonPlay_Click);
            // 
            // TextBoxLog
            // 
            this.TextBoxLog.BackColor = System.Drawing.SystemColors.Window;
            this.TextBoxLog.Location = new System.Drawing.Point(213, 27);
            this.TextBoxLog.Multiline = true;
            this.TextBoxLog.Name = "TextBoxLog";
            this.TextBoxLog.ReadOnly = true;
            this.TextBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBoxLog.Size = new System.Drawing.Size(370, 424);
            this.TextBoxLog.TabIndex = 3;
            // 
            // TextBoxDevice
            // 
            this.TextBoxDevice.Location = new System.Drawing.Point(12, 187);
            this.TextBoxDevice.Name = "TextBoxDevice";
            this.TextBoxDevice.Size = new System.Drawing.Size(195, 23);
            this.TextBoxDevice.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Enter device or IP address:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(210, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Messages:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 344);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Max Size:";
            // 
            // TextBoxMaxSize
            // 
            this.TextBoxMaxSize.Location = new System.Drawing.Point(103, 341);
            this.TextBoxMaxSize.MaxLength = 5;
            this.TextBoxMaxSize.Name = "TextBoxMaxSize";
            this.TextBoxMaxSize.Size = new System.Drawing.Size(104, 23);
            this.TextBoxMaxSize.TabIndex = 8;
            this.TextBoxMaxSize.Text = "1920";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 373);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Bitrate (Mbps):";
            // 
            // TextBoxBitRate
            // 
            this.TextBoxBitRate.Location = new System.Drawing.Point(103, 370);
            this.TextBoxBitRate.Name = "TextBoxBitRate";
            this.TextBoxBitRate.Size = new System.Drawing.Size(104, 23);
            this.TextBoxBitRate.TabIndex = 10;
            this.TextBoxBitRate.Text = "32";
            // 
            // CheckBoxBorderless
            // 
            this.CheckBoxBorderless.AutoSize = true;
            this.CheckBoxBorderless.Location = new System.Drawing.Point(12, 216);
            this.CheckBoxBorderless.Name = "CheckBoxBorderless";
            this.CheckBoxBorderless.Size = new System.Drawing.Size(80, 19);
            this.CheckBoxBorderless.TabIndex = 11;
            this.CheckBoxBorderless.Text = "Borderless";
            this.CheckBoxBorderless.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAlwaysOnTop
            // 
            this.CheckBoxAlwaysOnTop.AutoSize = true;
            this.CheckBoxAlwaysOnTop.Location = new System.Drawing.Point(102, 216);
            this.CheckBoxAlwaysOnTop.Name = "CheckBoxAlwaysOnTop";
            this.CheckBoxAlwaysOnTop.Size = new System.Drawing.Size(101, 19);
            this.CheckBoxAlwaysOnTop.TabIndex = 12;
            this.CheckBoxAlwaysOnTop.Text = "Always on top";
            this.CheckBoxAlwaysOnTop.UseVisualStyleBackColor = true;
            // 
            // CheckBoxFullscreen
            // 
            this.CheckBoxFullscreen.AutoSize = true;
            this.CheckBoxFullscreen.Location = new System.Drawing.Point(12, 241);
            this.CheckBoxFullscreen.Name = "CheckBoxFullscreen";
            this.CheckBoxFullscreen.Size = new System.Drawing.Size(79, 19);
            this.CheckBoxFullscreen.TabIndex = 13;
            this.CheckBoxFullscreen.Text = "Fullscreen";
            this.CheckBoxFullscreen.UseVisualStyleBackColor = true;
            // 
            // CheckBoxNoControl
            // 
            this.CheckBoxNoControl.AutoSize = true;
            this.CheckBoxNoControl.Location = new System.Drawing.Point(102, 241);
            this.CheckBoxNoControl.Name = "CheckBoxNoControl";
            this.CheckBoxNoControl.Size = new System.Drawing.Size(83, 19);
            this.CheckBoxNoControl.TabIndex = 14;
            this.CheckBoxNoControl.Text = "No control";
            this.CheckBoxNoControl.UseVisualStyleBackColor = true;
            // 
            // CheckBoxStayAwake
            // 
            this.CheckBoxStayAwake.AutoSize = true;
            this.CheckBoxStayAwake.Location = new System.Drawing.Point(12, 266);
            this.CheckBoxStayAwake.Name = "CheckBoxStayAwake";
            this.CheckBoxStayAwake.Size = new System.Drawing.Size(84, 19);
            this.CheckBoxStayAwake.TabIndex = 15;
            this.CheckBoxStayAwake.Text = "Stay awake";
            this.CheckBoxStayAwake.UseVisualStyleBackColor = true;
            // 
            // CheckBoxTurnScreenOff
            // 
            this.CheckBoxTurnScreenOff.AutoSize = true;
            this.CheckBoxTurnScreenOff.Location = new System.Drawing.Point(102, 266);
            this.CheckBoxTurnScreenOff.Name = "CheckBoxTurnScreenOff";
            this.CheckBoxTurnScreenOff.Size = new System.Drawing.Size(105, 19);
            this.CheckBoxTurnScreenOff.TabIndex = 16;
            this.CheckBoxTurnScreenOff.Text = "Turn screen off";
            this.CheckBoxTurnScreenOff.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 402);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 15);
            this.label6.TabIndex = 17;
            this.label6.Text = "Max FPS:";
            // 
            // TextBoxMaxFps
            // 
            this.TextBoxMaxFps.Location = new System.Drawing.Point(103, 399);
            this.TextBoxMaxFps.MaxLength = 3;
            this.TextBoxMaxFps.Name = "TextBoxMaxFps";
            this.TextBoxMaxFps.Size = new System.Drawing.Size(104, 23);
            this.TextBoxMaxFps.TabIndex = 18;
            this.TextBoxMaxFps.Text = "60";
            // 
            // CheckBoxPowerOffClose
            // 
            this.CheckBoxPowerOffClose.AutoSize = true;
            this.CheckBoxPowerOffClose.Location = new System.Drawing.Point(12, 316);
            this.CheckBoxPowerOffClose.Name = "CheckBoxPowerOffClose";
            this.CheckBoxPowerOffClose.Size = new System.Drawing.Size(161, 19);
            this.CheckBoxPowerOffClose.TabIndex = 19;
            this.CheckBoxPowerOffClose.Text = "Power off device on close";
            this.CheckBoxPowerOffClose.UseVisualStyleBackColor = true;
            // 
            // CheckBoxNoPowerOnStart
            // 
            this.CheckBoxNoPowerOnStart.AutoSize = true;
            this.CheckBoxNoPowerOnStart.Location = new System.Drawing.Point(12, 291);
            this.CheckBoxNoPowerOnStart.Name = "CheckBoxNoPowerOnStart";
            this.CheckBoxNoPowerOnStart.Size = new System.Drawing.Size(188, 19);
            this.CheckBoxNoPowerOnStart.TabIndex = 20;
            this.CheckBoxNoPowerOnStart.Text = "Don\'t power on device on start";
            this.CheckBoxNoPowerOnStart.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 463);
            this.Controls.Add(this.CheckBoxNoPowerOnStart);
            this.Controls.Add(this.CheckBoxPowerOffClose);
            this.Controls.Add(this.TextBoxMaxFps);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.CheckBoxTurnScreenOff);
            this.Controls.Add(this.CheckBoxStayAwake);
            this.Controls.Add(this.CheckBoxNoControl);
            this.Controls.Add(this.CheckBoxFullscreen);
            this.Controls.Add(this.CheckBoxAlwaysOnTop);
            this.Controls.Add(this.CheckBoxBorderless);
            this.Controls.Add(this.TextBoxBitRate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TextBoxMaxSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TextBoxDevice);
            this.Controls.Add(this.TextBoxLog);
            this.Controls.Add(this.ButtonPlay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListBoxDevices);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "ScrSndCpy - by Neil";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox ListBoxDevices;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ButtonPlay;
        private System.Windows.Forms.TextBox TextBoxLog;
        private System.Windows.Forms.TextBox TextBoxDevice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TextBoxMaxSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TextBoxBitRate;
        private System.Windows.Forms.CheckBox CheckBoxBorderless;
        private System.Windows.Forms.CheckBox CheckBoxAlwaysOnTop;
        private System.Windows.Forms.CheckBox CheckBoxFullscreen;
        private System.Windows.Forms.CheckBox CheckBoxNoControl;
        private System.Windows.Forms.CheckBox CheckBoxStayAwake;
        private System.Windows.Forms.CheckBox CheckBoxTurnScreenOff;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TextBoxMaxFps;
        private System.Windows.Forms.CheckBox CheckBoxPowerOffClose;
        private System.Windows.Forms.CheckBox CheckBoxNoPowerOnStart;
    }
}

