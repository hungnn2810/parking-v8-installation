namespace Install.App;

partial class Main
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
        lblIpAddress = new System.Windows.Forms.Label();
        tbxIpAddress = new System.Windows.Forms.TextBox();
        lblRam = new System.Windows.Forms.Label();
        lbl_Cpus = new System.Windows.Forms.Label();
        nudCpus = new System.Windows.Forms.NumericUpDown();
        nudRam = new System.Windows.Forms.NumericUpDown();
        btnStart = new System.Windows.Forms.Button();
        tbxMaxDiskSize = new System.Windows.Forms.TextBox();
        lblMaxDiskSize = new System.Windows.Forms.Label();
        lbl_ipaddresss = new System.Windows.Forms.Label();
        numericUpDown1 = new System.Windows.Forms.NumericUpDown();
        cbNetworkInterfaces = new System.Windows.Forms.ComboBox();
        lblNetworkInterface = new System.Windows.Forms.Label();
        rtbLogs = new System.Windows.Forms.RichTextBox();
        lblDataDirectory = new System.Windows.Forms.Label();
        tbxDataDirectory = new System.Windows.Forms.TextBox();
        btnDataDirectory = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)nudCpus).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudRam).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
        SuspendLayout();
        // 
        // lblIpAddress
        // 
        lblIpAddress.AutoSize = true;
        lblIpAddress.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
        lblIpAddress.Location = new System.Drawing.Point(8, 8);
        lblIpAddress.Name = "lblIpAddress";
        lblIpAddress.Size = new System.Drawing.Size(94, 25);
        lblIpAddress.TabIndex = 0;
        lblIpAddress.Text = "Địa chỉ IP";
        // 
        // tbxIpAddress
        // 
        tbxIpAddress.Font = new System.Drawing.Font("Segoe UI", 14.25F);
        tbxIpAddress.Location = new System.Drawing.Point(8, 40);
        tbxIpAddress.Name = "tbxIpAddress";
        tbxIpAddress.Size = new System.Drawing.Size(191, 33);
        tbxIpAddress.TabIndex = 1;
        // 
        // lblRam
        // 
        lblRam.AutoSize = true;
        lblRam.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
        lblRam.Location = new System.Drawing.Point(817, 9);
        lblRam.Name = "lblRam";
        lblRam.Size = new System.Drawing.Size(179, 25);
        lblRam.TabIndex = 2;
        lblRam.Text = "Giới hạn RAM (GB)";
        // 
        // lbl_Cpus
        // 
        lbl_Cpus.AutoSize = true;
        lbl_Cpus.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
        lbl_Cpus.Location = new System.Drawing.Point(538, 8);
        lbl_Cpus.Name = "lbl_Cpus";
        lbl_Cpus.Size = new System.Drawing.Size(195, 25);
        lbl_Cpus.TabIndex = 4;
        lbl_Cpus.Text = "Giới hạn CPU (cores)";
        // 
        // nudCpus
        // 
        nudCpus.Font = new System.Drawing.Font("Segoe UI", 14.25F);
        nudCpus.Location = new System.Drawing.Point(538, 40);
        nudCpus.Name = "nudCpus";
        nudCpus.Size = new System.Drawing.Size(184, 33);
        nudCpus.TabIndex = 3;
        nudCpus.Value = new decimal(new int[] { 6, 0, 0, 0 });
        // 
        // nudRam
        // 
        nudRam.Font = new System.Drawing.Font("Segoe UI", 14.25F);
        nudRam.Location = new System.Drawing.Point(818, 41);
        nudRam.Name = "nudRam";
        nudRam.Size = new System.Drawing.Size(191, 33);
        nudRam.TabIndex = 4;
        nudRam.Value = new decimal(new int[] { 8, 0, 0, 0 });
        // 
        // btnStart
        // 
        btnStart.BackColor = System.Drawing.Color.DodgerBlue;
        btnStart.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
        btnStart.ForeColor = System.Drawing.SystemColors.Window;
        btnStart.Location = new System.Drawing.Point(421, 169);
        btnStart.Name = "btnStart";
        btnStart.Size = new System.Drawing.Size(191, 34);
        btnStart.TabIndex = 0;
        btnStart.Text = "Khởi động";
        btnStart.UseVisualStyleBackColor = false;
        btnStart.Click += btnStart_Click;
        // 
        // tbxMaxDiskSize
        // 
        tbxMaxDiskSize.Font = new System.Drawing.Font("Segoe UI", 14.25F);
        tbxMaxDiskSize.Location = new System.Drawing.Point(267, 40);
        tbxMaxDiskSize.Name = "tbxMaxDiskSize";
        tbxMaxDiskSize.Size = new System.Drawing.Size(191, 33);
        tbxMaxDiskSize.TabIndex = 2;
        // 
        // lblMaxDiskSize
        // 
        lblMaxDiskSize.AutoSize = true;
        lblMaxDiskSize.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
        lblMaxDiskSize.Location = new System.Drawing.Point(267, 8);
        lblMaxDiskSize.Name = "lblMaxDiskSize";
        lblMaxDiskSize.Size = new System.Drawing.Size(200, 25);
        lblMaxDiskSize.TabIndex = 9;
        lblMaxDiskSize.Text = "Giới hạn bộ nhớ (GB)";
        // 
        // lbl_ipaddresss
        // 
        lbl_ipaddresss.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
        lbl_ipaddresss.Location = new System.Drawing.Point(54, 189);
        lbl_ipaddresss.Name = "lbl_ipaddresss";
        lbl_ipaddresss.Size = new System.Drawing.Size(100, 23);
        lbl_ipaddresss.TabIndex = 11;
        // 
        // numericUpDown1
        // 
        numericUpDown1.Font = new System.Drawing.Font("Segoe UI", 14.25F);
        numericUpDown1.Location = new System.Drawing.Point(55, 215);
        numericUpDown1.Name = "numericUpDown1";
        numericUpDown1.Size = new System.Drawing.Size(120, 33);
        numericUpDown1.TabIndex = 12;
        numericUpDown1.Value = new decimal(new int[] { 8, 0, 0, 0 });
        // 
        // cbNetworkInterfaces
        // 
        cbNetworkInterfaces.Font = new System.Drawing.Font("Segoe UI", 14.25F);
        cbNetworkInterfaces.FormattingEnabled = true;
        cbNetworkInterfaces.Location = new System.Drawing.Point(8, 112);
        cbNetworkInterfaces.Name = "cbNetworkInterfaces";
        cbNetworkInterfaces.Size = new System.Drawing.Size(564, 33);
        cbNetworkInterfaces.TabIndex = 5;
        // 
        // lblNetworkInterface
        // 
        lblNetworkInterface.AutoSize = true;
        lblNetworkInterface.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
        lblNetworkInterface.Location = new System.Drawing.Point(8, 80);
        lblNetworkInterface.Name = "lblNetworkInterface";
        lblNetworkInterface.Size = new System.Drawing.Size(116, 25);
        lblNetworkInterface.TabIndex = 0;
        lblNetworkInterface.Text = "Cổng mạng";
        // 
        // rtbLogs
        // 
        rtbLogs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
        rtbLogs.Location = new System.Drawing.Point(8, 230);
        rtbLogs.Name = "rtbLogs";
        rtbLogs.Size = new System.Drawing.Size(1009, 336);
        rtbLogs.TabIndex = 15;
        rtbLogs.Text = "";
        rtbLogs.TextChanged += rtbLogs_TextChanged;
        // 
        // lblDataDirectory
        // 
        lblDataDirectory.AutoSize = true;
        lblDataDirectory.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
        lblDataDirectory.Location = new System.Drawing.Point(589, 79);
        lblDataDirectory.Name = "lblDataDirectory";
        lblDataDirectory.Size = new System.Drawing.Size(216, 25);
        lblDataDirectory.TabIndex = 17;
        lblDataDirectory.Text = "Đường dẫn lưu dữ liệu";
        // 
        // tbxDataDirectory
        // 
        tbxDataDirectory.BackColor = System.Drawing.SystemColors.Window;
        tbxDataDirectory.Font = new System.Drawing.Font("Segoe UI", 14.25F);
        tbxDataDirectory.ForeColor = System.Drawing.SystemColors.WindowText;
        tbxDataDirectory.Location = new System.Drawing.Point(589, 111);
        tbxDataDirectory.Name = "tbxDataDirectory";
        tbxDataDirectory.Size = new System.Drawing.Size(293, 33);
        tbxDataDirectory.TabIndex = 18;
        // 
        // btnDataDirectory
        // 
        btnDataDirectory.BackColor = System.Drawing.SystemColors.Window;
        btnDataDirectory.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
        btnDataDirectory.ForeColor = System.Drawing.SystemColors.WindowText;
        btnDataDirectory.Location = new System.Drawing.Point(888, 111);
        btnDataDirectory.Name = "btnDataDirectory";
        btnDataDirectory.Size = new System.Drawing.Size(124, 34);
        btnDataDirectory.TabIndex = 19;
        btnDataDirectory.Text = "Chọn";
        btnDataDirectory.UseVisualStyleBackColor = false;
        btnDataDirectory.Click += btnDataDirectory_Click;
        // 
        // Main
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.SystemColors.Control;
        ClientSize = new System.Drawing.Size(1029, 578);
        Controls.Add(btnDataDirectory);
        Controls.Add(tbxDataDirectory);
        Controls.Add(lblDataDirectory);
        Controls.Add(rtbLogs);
        Controls.Add(cbNetworkInterfaces);
        Controls.Add(tbxMaxDiskSize);
        Controls.Add(lblMaxDiskSize);
        Controls.Add(btnStart);
        Controls.Add(nudRam);
        Controls.Add(nudCpus);
        Controls.Add(lbl_Cpus);
        Controls.Add(lblRam);
        Controls.Add(tbxIpAddress);
        Controls.Add(lblNetworkInterface);
        Controls.Add(lblIpAddress);
        Icon = ((System.Drawing.Icon)resources.GetObject("$this.Icon"));
        KeyPreview = true;
        Location = new System.Drawing.Point(15, 15);
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "Parking v8 Server Setup";
        KeyDown += Main_KeyDown;
        ((System.ComponentModel.ISupportInitialize)nudCpus).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudRam).EndInit();
        ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.TextBox tbxDataDirectory;
    private System.Windows.Forms.Button btnDataDirectory;

    private System.Windows.Forms.Label lblDataDirectory;

    private System.Windows.Forms.Label lbl_ipaddresss;
    private System.Windows.Forms.NumericUpDown numericUpDown1;

    private System.Windows.Forms.TextBox tbxMaxDiskSize;
    private System.Windows.Forms.Label lblMaxDiskSize;

    private System.Windows.Forms.Button btnStart;

    private System.Windows.Forms.NumericUpDown nudCpus;
    private System.Windows.Forms.NumericUpDown nudRam;

    private System.Windows.Forms.TextBox tbxIpAddress;
    private System.Windows.Forms.Label lblRam;
    private System.Windows.Forms.Label lbl_Cpus;

    private System.Windows.Forms.Label lblIpAddress;

    #endregion

    private System.Windows.Forms.ComboBox cbNetworkInterfaces;
    private System.Windows.Forms.Label lblNetworkInterface;
    private System.Windows.Forms.RichTextBox rtbLogs;
}