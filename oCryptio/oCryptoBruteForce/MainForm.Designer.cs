namespace oCryptoBruteForce
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
            this.components = new System.ComponentModel.Container();
            this.checksumComboBox = new System.Windows.Forms.ComboBox();
            this.tplCheckBox = new System.Windows.Forms.CheckBox();
            this.byteSkippingCheckBox = new System.Windows.Forms.CheckBox();
            this.skipBytesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lazyGenerateComboBox = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lazySearchComboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.stopAtPositionTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.startChecksumPositionTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.startSearchTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.bruteforceTabPage = new System.Windows.Forms.TabPage();
            this.workMonitorListView = new System.Windows.Forms.ListView();
            this.workMonitorStatusHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.workMonitorWorkIdHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.workMonitorSecurityHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.workMonitorChecksumHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.workMonitorChecksumOffsetHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.workMonitorLengthHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.workMonitorStartTimeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.workMonitorEndTimeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel6 = new System.Windows.Forms.Panel();
            this.label21 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.addWorkButton = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.serverModeTabPage = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.startListeningButton = new System.Windows.Forms.Button();
            this.stopListeningButton = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.listeningTextBox = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.clientMonitorListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.infoTextBox = new System.Windows.Forms.TextBox();
            this.clientModeTabPage = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.addServerButton = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.serverIPTextBox = new System.Windows.Forms.TextBox();
            this.serverPortTextBox = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.serverMonitorListView = new System.Windows.Forms.ListView();
            this.statusColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.workerIdColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.serverIpColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.serverPortColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clientInformationTextBox = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.exhaustiveSearchCheckBox = new System.Windows.Forms.CheckBox();
            this.convertFromBase64StringCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dragAndDropPanel = new System.Windows.Forms.Panel();
            this.clientModeContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPossibleChecksumFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.workContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.viewDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.removeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.skipBytesNumericUpDown)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.bruteforceTabPage.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel8.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.serverModeTabPage.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.clientModeTabPage.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.clientModeContextMenuStrip.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.workContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // checksumComboBox
            // 
            this.checksumComboBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checksumComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.checksumComboBox.FormattingEnabled = true;
            this.checksumComboBox.Items.AddRange(new object[] {
            "Adler8 - {1Bytes}",
            "Adler16 - {2Bytes}",
            "Adler32 - {4Bytes}",
            "Checksum8 - {1Bytes}",
            "Checksum16 - {2Bytes}",
            "Checksum24 - {3Bytes}",
            "Checksum32 - {4Bytes}",
            "Checksum40 - {5Bytes}",
            "Checksum48 - {6Bytes}",
            "Checksum56 - {7Bytes}",
            "Checksum64 - {8Bytes}",
            "CRC16 - {2Bytes}",
            "CRC16 CCITT - {2Bytes}",
            "CRC32 - {4Bytes}",
            "HMAC SHA 1 (128)  - {16Bytes}",
            "HMAC SHA 256 - {32Bytes}",
            "HMAC SHA 384 - {48Bytes}",
            "HMAC SHA 512 - {64Bytes}",
            "MD5 - {16Bytes}",
            "MD5 CNG - {16Bytes}"});
            this.checksumComboBox.Location = new System.Drawing.Point(143, 22);
            this.checksumComboBox.Name = "checksumComboBox";
            this.checksumComboBox.Size = new System.Drawing.Size(488, 23);
            this.checksumComboBox.TabIndex = 2;
            // 
            // tplCheckBox
            // 
            this.tplCheckBox.Location = new System.Drawing.Point(142, 22);
            this.tplCheckBox.Name = "tplCheckBox";
            this.tplCheckBox.Size = new System.Drawing.Size(236, 37);
            this.tplCheckBox.TabIndex = 34;
            this.tplCheckBox.Text = "Task Parallelism (Task Parallel Library)";
            this.tplCheckBox.UseVisualStyleBackColor = true;
            // 
            // byteSkippingCheckBox
            // 
            this.byteSkippingCheckBox.AutoSize = true;
            this.byteSkippingCheckBox.Location = new System.Drawing.Point(18, 32);
            this.byteSkippingCheckBox.Name = "byteSkippingCheckBox";
            this.byteSkippingCheckBox.Size = new System.Drawing.Size(136, 19);
            this.byteSkippingCheckBox.TabIndex = 33;
            this.byteSkippingCheckBox.Text = "Enable Byte Skipping";
            this.byteSkippingCheckBox.UseVisualStyleBackColor = true;
            this.byteSkippingCheckBox.CheckedChanged += new System.EventHandler(this.byteSkippingCheckBox_CheckedChanged);
            // 
            // skipBytesNumericUpDown
            // 
            this.skipBytesNumericUpDown.Enabled = false;
            this.skipBytesNumericUpDown.Location = new System.Drawing.Point(84, 58);
            this.skipBytesNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.skipBytesNumericUpDown.Name = "skipBytesNumericUpDown";
            this.skipBytesNumericUpDown.Size = new System.Drawing.Size(70, 23);
            this.skipBytesNumericUpDown.TabIndex = 32;
            this.skipBytesNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(15, 61);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(63, 15);
            this.label14.TabIndex = 31;
            this.label14.Text = "Skip bytes:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(246, 68);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 15);
            this.label10.TabIndex = 23;
            this.label10.Text = "Lazy Generate:";
            // 
            // lazyGenerateComboBox
            // 
            this.lazyGenerateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lazyGenerateComboBox.FormattingEnabled = true;
            this.lazyGenerateComboBox.Items.AddRange(new object[] {
            "OFF",
            "ON"});
            this.lazyGenerateComboBox.Location = new System.Drawing.Point(335, 65);
            this.lazyGenerateComboBox.Name = "lazyGenerateComboBox";
            this.lazyGenerateComboBox.Size = new System.Drawing.Size(149, 23);
            this.lazyGenerateComboBox.TabIndex = 22;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 15);
            this.label9.TabIndex = 21;
            this.label9.Text = "Lazy Search:";
            // 
            // lazySearchComboBox
            // 
            this.lazySearchComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lazySearchComboBox.FormattingEnabled = true;
            this.lazySearchComboBox.Items.AddRange(new object[] {
            "OFF",
            "ON"});
            this.lazySearchComboBox.Location = new System.Drawing.Point(89, 65);
            this.lazySearchComboBox.Name = "lazySearchComboBox";
            this.lazySearchComboBox.Size = new System.Drawing.Size(149, 23);
            this.lazySearchComboBox.TabIndex = 20;
            this.lazySearchComboBox.SelectedIndexChanged += new System.EventHandler(this.lazySearchComboBox_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(210, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 15);
            this.label8.TabIndex = 19;
            this.label8.Text = "Stop At Position:";
            // 
            // stopAtPositionTextBox
            // 
            this.stopAtPositionTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.stopAtPositionTextBox.Location = new System.Drawing.Point(311, 54);
            this.stopAtPositionTextBox.Name = "stopAtPositionTextBox";
            this.stopAtPositionTextBox.Size = new System.Drawing.Size(80, 23);
            this.stopAtPositionTextBox.TabIndex = 18;
            this.stopAtPositionTextBox.Text = "0";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(404, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 15);
            this.label7.TabIndex = 16;
            this.label7.Text = "Start Generation From:";
            // 
            // startChecksumPositionTextBox
            // 
            this.startChecksumPositionTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.startChecksumPositionTextBox.Location = new System.Drawing.Point(536, 54);
            this.startChecksumPositionTextBox.Name = "startChecksumPositionTextBox";
            this.startChecksumPositionTextBox.Size = new System.Drawing.Size(95, 23);
            this.startChecksumPositionTextBox.TabIndex = 15;
            this.startChecksumPositionTextBox.Text = "0";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Start From Position:";
            // 
            // startSearchTextBox
            // 
            this.startSearchTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.startSearchTextBox.Location = new System.Drawing.Point(143, 54);
            this.startSearchTextBox.Name = "startSearchTextBox";
            this.startSearchTextBox.Size = new System.Drawing.Size(61, 23);
            this.startSearchTextBox.TabIndex = 6;
            this.startSearchTextBox.Text = "0";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select Checksum Type:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.bruteforceTabPage);
            this.tabControl1.Controls.Add(this.serverModeTabPage);
            this.tabControl1.Controls.Add(this.clientModeTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 187);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(853, 360);
            this.tabControl1.TabIndex = 9;
            // 
            // bruteforceTabPage
            // 
            this.bruteforceTabPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bruteforceTabPage.Controls.Add(this.workMonitorListView);
            this.bruteforceTabPage.Controls.Add(this.panel6);
            this.bruteforceTabPage.Controls.Add(this.panel8);
            this.bruteforceTabPage.Location = new System.Drawing.Point(4, 24);
            this.bruteforceTabPage.Name = "bruteforceTabPage";
            this.bruteforceTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.bruteforceTabPage.Size = new System.Drawing.Size(845, 332);
            this.bruteforceTabPage.TabIndex = 0;
            this.bruteforceTabPage.Text = "Bruteforce For Checksums";
            this.bruteforceTabPage.UseVisualStyleBackColor = true;
            // 
            // workMonitorListView
            // 
            this.workMonitorListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.workMonitorListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.workMonitorStatusHeader,
            this.workMonitorWorkIdHeader,
            this.workMonitorSecurityHeader,
            this.workMonitorChecksumHeader,
            this.workMonitorChecksumOffsetHeader,
            this.workMonitorLengthHeader,
            this.workMonitorStartTimeHeader,
            this.workMonitorEndTimeHeader});
            this.workMonitorListView.FullRowSelect = true;
            this.workMonitorListView.GridLines = true;
            this.workMonitorListView.Location = new System.Drawing.Point(6, 201);
            this.workMonitorListView.Name = "workMonitorListView";
            this.workMonitorListView.Size = new System.Drawing.Size(830, 138);
            this.workMonitorListView.TabIndex = 51;
            this.workMonitorListView.UseCompatibleStateImageBehavior = false;
            this.workMonitorListView.View = System.Windows.Forms.View.Details;
            this.workMonitorListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.workMonitorListView_MouseDown);
            // 
            // workMonitorStatusHeader
            // 
            this.workMonitorStatusHeader.Text = "Status";
            this.workMonitorStatusHeader.Width = 95;
            // 
            // workMonitorWorkIdHeader
            // 
            this.workMonitorWorkIdHeader.Text = "Worker ID";
            this.workMonitorWorkIdHeader.Width = 99;
            // 
            // workMonitorSecurityHeader
            // 
            this.workMonitorSecurityHeader.Text = "Security";
            // 
            // workMonitorChecksumHeader
            // 
            this.workMonitorChecksumHeader.Text = "Checksum";
            this.workMonitorChecksumHeader.Width = 135;
            // 
            // workMonitorChecksumOffsetHeader
            // 
            this.workMonitorChecksumOffsetHeader.Text = "Checksum Offset";
            this.workMonitorChecksumOffsetHeader.Width = 114;
            // 
            // workMonitorLengthHeader
            // 
            this.workMonitorLengthHeader.Text = "Generation Length";
            this.workMonitorLengthHeader.Width = 121;
            // 
            // workMonitorStartTimeHeader
            // 
            this.workMonitorStartTimeHeader.Text = "Start Time";
            this.workMonitorStartTimeHeader.Width = 105;
            // 
            // workMonitorEndTimeHeader
            // 
            this.workMonitorEndTimeHeader.Text = "End Time";
            this.workMonitorEndTimeHeader.Width = 99;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.label21);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(3, 148);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(837, 38);
            this.panel6.TabIndex = 52;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(355, 8);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(117, 21);
            this.label21.TabIndex = 0;
            this.label21.Text = "Work Monitor";
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.groupBox3);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(3, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(837, 145);
            this.panel8.TabIndex = 54;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.addWorkButton);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.checksumComboBox);
            this.groupBox3.Controls.Add(this.stopAtPositionTextBox);
            this.groupBox3.Controls.Add(this.startChecksumPositionTextBox);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.startSearchTextBox);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(835, 143);
            this.groupBox3.TabIndex = 37;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Search Information";
            // 
            // addWorkButton
            // 
            this.addWorkButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.addWorkButton.Location = new System.Drawing.Point(25, 88);
            this.addWorkButton.Name = "addWorkButton";
            this.addWorkButton.Size = new System.Drawing.Size(606, 40);
            this.addWorkButton.TabIndex = 44;
            this.addWorkButton.Text = "Add Work";
            this.addWorkButton.UseVisualStyleBackColor = true;
            this.addWorkButton.Click += new System.EventHandler(this.addWorkButton_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox5.Controls.Add(this.byteSkippingCheckBox);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.skipBytesNumericUpDown);
            this.groupBox5.Location = new System.Drawing.Point(647, 22);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(169, 106);
            this.groupBox5.TabIndex = 41;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Search Byte Skipping";
            // 
            // serverModeTabPage
            // 
            this.serverModeTabPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.serverModeTabPage.Controls.Add(this.panel5);
            this.serverModeTabPage.Controls.Add(this.panel4);
            this.serverModeTabPage.Controls.Add(this.clientMonitorListView);
            this.serverModeTabPage.Controls.Add(this.infoTextBox);
            this.serverModeTabPage.Location = new System.Drawing.Point(4, 24);
            this.serverModeTabPage.Name = "serverModeTabPage";
            this.serverModeTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.serverModeTabPage.Size = new System.Drawing.Size(845, 332);
            this.serverModeTabPage.TabIndex = 2;
            this.serverModeTabPage.Text = "Server Mode";
            this.serverModeTabPage.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.startListeningButton);
            this.panel5.Controls.Add(this.stopListeningButton);
            this.panel5.Controls.Add(this.label16);
            this.panel5.Controls.Add(this.listeningTextBox);
            this.panel5.Location = new System.Drawing.Point(9, 6);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(514, 38);
            this.panel5.TabIndex = 51;
            // 
            // startListeningButton
            // 
            this.startListeningButton.Location = new System.Drawing.Point(207, 8);
            this.startListeningButton.Name = "startListeningButton";
            this.startListeningButton.Size = new System.Drawing.Size(138, 23);
            this.startListeningButton.TabIndex = 44;
            this.startListeningButton.Text = "Start";
            this.startListeningButton.UseVisualStyleBackColor = true;
            this.startListeningButton.Click += new System.EventHandler(this.startListeningButton_Click);
            // 
            // stopListeningButton
            // 
            this.stopListeningButton.Location = new System.Drawing.Point(351, 8);
            this.stopListeningButton.Name = "stopListeningButton";
            this.stopListeningButton.Size = new System.Drawing.Size(138, 23);
            this.stopListeningButton.TabIndex = 43;
            this.stopListeningButton.Text = "Stop";
            this.stopListeningButton.UseVisualStyleBackColor = true;
            this.stopListeningButton.Click += new System.EventHandler(this.stopListeningButton_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 11);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(83, 15);
            this.label16.TabIndex = 40;
            this.label16.Text = "Listening Port:";
            // 
            // listeningTextBox
            // 
            this.listeningTextBox.Location = new System.Drawing.Point(101, 8);
            this.listeningTextBox.Name = "listeningTextBox";
            this.listeningTextBox.Size = new System.Drawing.Size(100, 23);
            this.listeningTextBox.TabIndex = 41;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label20);
            this.panel4.Location = new System.Drawing.Point(529, 6);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(308, 38);
            this.panel4.TabIndex = 50;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(91, 8);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(121, 21);
            this.label20.TabIndex = 0;
            this.label20.Text = "Client Monitor";
            // 
            // clientMonitorListView
            // 
            this.clientMonitorListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.clientMonitorListView.FullRowSelect = true;
            this.clientMonitorListView.GridLines = true;
            this.clientMonitorListView.Location = new System.Drawing.Point(529, 50);
            this.clientMonitorListView.Name = "clientMonitorListView";
            this.clientMonitorListView.Size = new System.Drawing.Size(308, 242);
            this.clientMonitorListView.TabIndex = 49;
            this.clientMonitorListView.UseCompatibleStateImageBehavior = false;
            this.clientMonitorListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Status";
            this.columnHeader1.Width = 95;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Worker ID";
            this.columnHeader2.Width = 99;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "IP Address";
            this.columnHeader3.Width = 103;
            // 
            // infoTextBox
            // 
            this.infoTextBox.BackColor = System.Drawing.SystemColors.InfoText;
            this.infoTextBox.ForeColor = System.Drawing.SystemColors.Info;
            this.infoTextBox.Location = new System.Drawing.Point(9, 50);
            this.infoTextBox.Multiline = true;
            this.infoTextBox.Name = "infoTextBox";
            this.infoTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.infoTextBox.Size = new System.Drawing.Size(514, 240);
            this.infoTextBox.TabIndex = 39;
            // 
            // clientModeTabPage
            // 
            this.clientModeTabPage.Controls.Add(this.panel3);
            this.clientModeTabPage.Controls.Add(this.panel2);
            this.clientModeTabPage.Controls.Add(this.serverMonitorListView);
            this.clientModeTabPage.Controls.Add(this.clientInformationTextBox);
            this.clientModeTabPage.Location = new System.Drawing.Point(4, 24);
            this.clientModeTabPage.Name = "clientModeTabPage";
            this.clientModeTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.clientModeTabPage.Size = new System.Drawing.Size(845, 332);
            this.clientModeTabPage.TabIndex = 3;
            this.clientModeTabPage.Text = "Client Mode";
            this.clientModeTabPage.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.addServerButton);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Controls.Add(this.serverIPTextBox);
            this.panel3.Controls.Add(this.serverPortTextBox);
            this.panel3.Controls.Add(this.label18);
            this.panel3.Location = new System.Drawing.Point(8, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(515, 38);
            this.panel3.TabIndex = 49;
            // 
            // addServerButton
            // 
            this.addServerButton.Location = new System.Drawing.Point(336, 8);
            this.addServerButton.Name = "addServerButton";
            this.addServerButton.Size = new System.Drawing.Size(149, 23);
            this.addServerButton.TabIndex = 43;
            this.addServerButton.Text = "Add";
            this.addServerButton.UseVisualStyleBackColor = true;
            this.addServerButton.Click += new System.EventHandler(this.addServerButton_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(12, 11);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 15);
            this.label17.TabIndex = 40;
            this.label17.Text = "IP Address:";
            // 
            // serverIPTextBox
            // 
            this.serverIPTextBox.Location = new System.Drawing.Point(83, 8);
            this.serverIPTextBox.Name = "serverIPTextBox";
            this.serverIPTextBox.Size = new System.Drawing.Size(100, 23);
            this.serverIPTextBox.TabIndex = 41;
            // 
            // serverPortTextBox
            // 
            this.serverPortTextBox.Location = new System.Drawing.Point(228, 8);
            this.serverPortTextBox.Name = "serverPortTextBox";
            this.serverPortTextBox.Size = new System.Drawing.Size(100, 23);
            this.serverPortTextBox.TabIndex = 46;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(190, 11);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(32, 15);
            this.label18.TabIndex = 45;
            this.label18.Text = "Port:";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label19);
            this.panel2.Location = new System.Drawing.Point(529, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(308, 38);
            this.panel2.TabIndex = 48;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(91, 8);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(125, 21);
            this.label19.TabIndex = 0;
            this.label19.Text = "Server Monitor";
            // 
            // serverMonitorListView
            // 
            this.serverMonitorListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.statusColumnHeader,
            this.workerIdColumnHeader,
            this.serverIpColumnHeader,
            this.serverPortColumnHeader});
            this.serverMonitorListView.FullRowSelect = true;
            this.serverMonitorListView.GridLines = true;
            this.serverMonitorListView.Location = new System.Drawing.Point(529, 50);
            this.serverMonitorListView.Name = "serverMonitorListView";
            this.serverMonitorListView.Size = new System.Drawing.Size(308, 242);
            this.serverMonitorListView.TabIndex = 47;
            this.serverMonitorListView.UseCompatibleStateImageBehavior = false;
            this.serverMonitorListView.View = System.Windows.Forms.View.Details;
            this.serverMonitorListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.serverMonitorListView_MouseDown);
            // 
            // statusColumnHeader
            // 
            this.statusColumnHeader.Text = "Status";
            // 
            // workerIdColumnHeader
            // 
            this.workerIdColumnHeader.Text = "Worker ID";
            this.workerIdColumnHeader.Width = 84;
            // 
            // serverIpColumnHeader
            // 
            this.serverIpColumnHeader.Text = "IP Address";
            this.serverIpColumnHeader.Width = 83;
            // 
            // serverPortColumnHeader
            // 
            this.serverPortColumnHeader.Text = "Port";
            // 
            // clientInformationTextBox
            // 
            this.clientInformationTextBox.BackColor = System.Drawing.SystemColors.InfoText;
            this.clientInformationTextBox.ForeColor = System.Drawing.SystemColors.Info;
            this.clientInformationTextBox.Location = new System.Drawing.Point(8, 50);
            this.clientInformationTextBox.Multiline = true;
            this.clientInformationTextBox.Name = "clientInformationTextBox";
            this.clientInformationTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.clientInformationTextBox.Size = new System.Drawing.Size(515, 242);
            this.clientInformationTextBox.TabIndex = 44;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.exhaustiveSearchCheckBox);
            this.groupBox4.Controls.Add(this.convertFromBase64StringCheckBox);
            this.groupBox4.Controls.Add(this.tplCheckBox);
            this.groupBox4.Controls.Add(this.lazySearchComboBox);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.lazyGenerateComboBox);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(851, 114);
            this.groupBox4.TabIndex = 38;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Miscellaneous";
            // 
            // exhaustiveSearchCheckBox
            // 
            this.exhaustiveSearchCheckBox.Location = new System.Drawing.Point(6, 22);
            this.exhaustiveSearchCheckBox.Name = "exhaustiveSearchCheckBox";
            this.exhaustiveSearchCheckBox.Size = new System.Drawing.Size(130, 37);
            this.exhaustiveSearchCheckBox.TabIndex = 43;
            this.exhaustiveSearchCheckBox.Text = "Exhaustive Search";
            this.exhaustiveSearchCheckBox.UseVisualStyleBackColor = true;
            // 
            // convertFromBase64StringCheckBox
            // 
            this.convertFromBase64StringCheckBox.Location = new System.Drawing.Point(384, 22);
            this.convertFromBase64StringCheckBox.Name = "convertFromBase64StringCheckBox";
            this.convertFromBase64StringCheckBox.Size = new System.Drawing.Size(226, 37);
            this.convertFromBase64StringCheckBox.TabIndex = 42;
            this.convertFromBase64StringCheckBox.Text = "Convert From Base 64 string";
            this.convertFromBase64StringCheckBox.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.dragAndDropPanel);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(853, 163);
            this.panel1.TabIndex = 10;
            // 
            // dragAndDropPanel
            // 
            this.dragAndDropPanel.AllowDrop = true;
            this.dragAndDropPanel.BackColor = System.Drawing.Color.DimGray;
            this.dragAndDropPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.dragAndDropPanel.Location = new System.Drawing.Point(0, 114);
            this.dragAndDropPanel.Name = "dragAndDropPanel";
            this.dragAndDropPanel.Size = new System.Drawing.Size(851, 47);
            this.dragAndDropPanel.TabIndex = 11;
            this.dragAndDropPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.dragAndDropPanel_DragDrop);
            this.dragAndDropPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.dragAndDropPanel_DragEnter);
            // 
            // clientModeContextMenuStrip
            // 
            this.clientModeContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.disconnectToolStripMenuItem,
            this.toolStripSeparator1,
            this.removeToolStripMenuItem});
            this.clientModeContextMenuStrip.Name = "contextMenuStrip1";
            this.clientModeContextMenuStrip.Size = new System.Drawing.Size(134, 76);
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.disconnectToolStripMenuItem.Text = "Disconnect";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(130, 6);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.openPossibleChecksumFileToolStripMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.openToolStripMenuItem.Text = "File";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // openPossibleChecksumFileToolStripMenuItem
            // 
            this.openPossibleChecksumFileToolStripMenuItem.Name = "openPossibleChecksumFileToolStripMenuItem";
            this.openPossibleChecksumFileToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.openPossibleChecksumFileToolStripMenuItem.Text = "Open Possible Checksum File";
            this.openPossibleChecksumFileToolStripMenuItem.Click += new System.EventHandler(this.openPossibleChecksumFileToolStripMenuItem_Click);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.mainMenuStrip.Size = new System.Drawing.Size(853, 24);
            this.mainMenuStrip.TabIndex = 1;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // workContextMenuStrip
            // 
            this.workContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.toolStripSeparator3,
            this.viewDetailsToolStripMenuItem,
            this.toolStripSeparator2,
            this.removeToolStripMenuItem1});
            this.workContextMenuStrip.Name = "workContextMenuStrip";
            this.workContextMenuStrip.Size = new System.Drawing.Size(138, 104);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(134, 6);
            // 
            // viewDetailsToolStripMenuItem
            // 
            this.viewDetailsToolStripMenuItem.Name = "viewDetailsToolStripMenuItem";
            this.viewDetailsToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.viewDetailsToolStripMenuItem.Text = "View Details";
            this.viewDetailsToolStripMenuItem.Click += new System.EventHandler(this.viewDetailsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(134, 6);
            // 
            // removeToolStripMenuItem1
            // 
            this.removeToolStripMenuItem1.Name = "removeToolStripMenuItem1";
            this.removeToolStripMenuItem1.Size = new System.Drawing.Size(137, 22);
            this.removeToolStripMenuItem1.Text = "Remove";
            this.removeToolStripMenuItem1.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 546);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mainMenuStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainForm";
            this.Text = "Crypto File Brute Force";
            ((System.ComponentModel.ISupportInitialize)(this.skipBytesNumericUpDown)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.bruteforceTabPage.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.serverModeTabPage.ResumeLayout(false);
            this.serverModeTabPage.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.clientModeTabPage.ResumeLayout(false);
            this.clientModeTabPage.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.clientModeContextMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.workContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox checksumComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox startSearchTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox startChecksumPositionTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox stopAtPositionTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox lazyGenerateComboBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox lazySearchComboBox;
        private System.Windows.Forms.NumericUpDown skipBytesNumericUpDown;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox byteSkippingCheckBox;
        private System.Windows.Forms.CheckBox tplCheckBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage bruteforceTabPage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel dragAndDropPanel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TabPage serverModeTabPage;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox infoTextBox;
        private System.Windows.Forms.TabPage clientModeTabPage;
        private System.Windows.Forms.ListView serverMonitorListView;
        private System.Windows.Forms.TextBox serverPortTextBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox clientInformationTextBox;
        private System.Windows.Forms.Button addServerButton;
        private System.Windows.Forms.TextBox serverIPTextBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ColumnHeader statusColumnHeader;
        private System.Windows.Forms.ColumnHeader workerIdColumnHeader;
        private System.Windows.Forms.ColumnHeader serverIpColumnHeader;
        private System.Windows.Forms.ColumnHeader serverPortColumnHeader;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ListView workMonitorListView;
        private System.Windows.Forms.ColumnHeader workMonitorStatusHeader;
        private System.Windows.Forms.ColumnHeader workMonitorWorkIdHeader;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button startListeningButton;
        private System.Windows.Forms.Button stopListeningButton;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox listeningTextBox;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ListView clientMonitorListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ContextMenuStrip clientModeContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip workContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button addWorkButton;
        private System.Windows.Forms.ColumnHeader workMonitorSecurityHeader;
        private System.Windows.Forms.ColumnHeader workMonitorChecksumHeader;
        private System.Windows.Forms.ColumnHeader workMonitorChecksumOffsetHeader;
        private System.Windows.Forms.ColumnHeader workMonitorLengthHeader;
        private System.Windows.Forms.ColumnHeader workMonitorStartTimeHeader;
        private System.Windows.Forms.ColumnHeader workMonitorEndTimeHeader;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem viewDetailsToolStripMenuItem;
        private System.Windows.Forms.CheckBox convertFromBase64StringCheckBox;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openPossibleChecksumFileToolStripMenuItem;
        private System.Windows.Forms.CheckBox exhaustiveSearchCheckBox;
    }
}

