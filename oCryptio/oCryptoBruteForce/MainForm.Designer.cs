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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checksumComboBox = new System.Windows.Forms.ComboBox();
            this.fileLocationTextBox = new System.Windows.Forms.TextBox();
            this.parallelComputingCheckBox = new System.Windows.Forms.CheckBox();
            this.byteSkippingCheckBox = new System.Windows.Forms.CheckBox();
            this.skipBytesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.stopTimeTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.startTimeTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lazyGenerateComboBox = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lazySearchComboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.stopAtPositionTextBox = new System.Windows.Forms.TextBox();
            this.stopButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.startChecksumPositionTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.currentChecksumPositionTextBox = new System.Windows.Forms.TextBox();
            this.startSearchButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.fileLengthTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.startSearchTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.bruteforceTabPage = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checksumValueTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.searchTabPage = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.usePCFCheckBox = new System.Windows.Forms.CheckBox();
            this.loadPCFButton = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.searchReverseValueCheckBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dragAndDropPanel = new System.Windows.Forms.Panel();
            this.possibleChecksumFileLocationTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skipBytesNumericUpDown)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.bruteforceTabPage.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(806, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // checksumComboBox
            // 
            this.checksumComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.checksumComboBox.FormattingEnabled = true;
            this.checksumComboBox.Items.AddRange(new object[] {
            "Adler8 - {1Byte}",
            "Adler16 - {2Bytes}",
            "Adler32 - {4Bytes}",
            "Checksum8 -",
            "Checksum16 -",
            "Checksum24 -",
            "Checksum32 -",
            "Checksum40 -",
            "Checksum48 -",
            "Checksum56 -",
            "Checksum64 -",
            "CRC16 -",
            "CRC16 CCITT -",
            "CRC32 - {4Bytes}",
            "HMAC SHA 1 (128)  - {16Bytes}",
            "HMAC SHA 256 - {32Bytes}",
            "HMAC SHA 384 - {48Byte}",
            "HMAC SHA 512 - {64Byte}",
            "MD5 - {16Byte}",
            "MD5 CNG - {16Byte}"});
            this.checksumComboBox.Location = new System.Drawing.Point(110, 6);
            this.checksumComboBox.Name = "checksumComboBox";
            this.checksumComboBox.Size = new System.Drawing.Size(465, 23);
            this.checksumComboBox.TabIndex = 2;
            // 
            // fileLocationTextBox
            // 
            this.fileLocationTextBox.Location = new System.Drawing.Point(199, 3);
            this.fileLocationTextBox.Name = "fileLocationTextBox";
            this.fileLocationTextBox.ReadOnly = true;
            this.fileLocationTextBox.Size = new System.Drawing.Size(385, 23);
            this.fileLocationTextBox.TabIndex = 3;
            // 
            // parallelComputingCheckBox
            // 
            this.parallelComputingCheckBox.AutoSize = true;
            this.parallelComputingCheckBox.Location = new System.Drawing.Point(24, 80);
            this.parallelComputingCheckBox.Name = "parallelComputingCheckBox";
            this.parallelComputingCheckBox.Size = new System.Drawing.Size(158, 19);
            this.parallelComputingCheckBox.TabIndex = 34;
            this.parallelComputingCheckBox.Text = "Use Parallel Computing ?";
            this.parallelComputingCheckBox.UseVisualStyleBackColor = true;
            // 
            // byteSkippingCheckBox
            // 
            this.byteSkippingCheckBox.AutoSize = true;
            this.byteSkippingCheckBox.Location = new System.Drawing.Point(23, 22);
            this.byteSkippingCheckBox.Name = "byteSkippingCheckBox";
            this.byteSkippingCheckBox.Size = new System.Drawing.Size(136, 19);
            this.byteSkippingCheckBox.TabIndex = 33;
            this.byteSkippingCheckBox.Text = "Enable Byte Skipping";
            this.byteSkippingCheckBox.UseVisualStyleBackColor = true;
            // 
            // skipBytesNumericUpDown
            // 
            this.skipBytesNumericUpDown.Location = new System.Drawing.Point(89, 47);
            this.skipBytesNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.skipBytesNumericUpDown.Name = "skipBytesNumericUpDown";
            this.skipBytesNumericUpDown.Size = new System.Drawing.Size(84, 23);
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
            this.label14.Location = new System.Drawing.Point(20, 50);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(63, 15);
            this.label14.TabIndex = 31;
            this.label14.Text = "Skip bytes:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 55);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 15);
            this.label13.TabIndex = 29;
            this.label13.Text = "Stop Time:";
            // 
            // stopTimeTextBox
            // 
            this.stopTimeTextBox.Location = new System.Drawing.Point(78, 52);
            this.stopTimeTextBox.Name = "stopTimeTextBox";
            this.stopTimeTextBox.ReadOnly = true;
            this.stopTimeTextBox.Size = new System.Drawing.Size(143, 23);
            this.stopTimeTextBox.TabIndex = 28;
            this.stopTimeTextBox.Text = "void";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 15);
            this.label12.TabIndex = 27;
            this.label12.Text = "Start Time:";
            // 
            // startTimeTextBox
            // 
            this.startTimeTextBox.Location = new System.Drawing.Point(78, 23);
            this.startTimeTextBox.Name = "startTimeTextBox";
            this.startTimeTextBox.ReadOnly = true;
            this.startTimeTextBox.Size = new System.Drawing.Size(143, 23);
            this.startTimeTextBox.TabIndex = 26;
            this.startTimeTextBox.Text = "void";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(316, 50);
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
            this.lazyGenerateComboBox.Location = new System.Drawing.Point(414, 47);
            this.lazyGenerateComboBox.Name = "lazyGenerateComboBox";
            this.lazyGenerateComboBox.Size = new System.Drawing.Size(149, 23);
            this.lazyGenerateComboBox.TabIndex = 22;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(328, 26);
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
            this.lazySearchComboBox.Location = new System.Drawing.Point(414, 18);
            this.lazySearchComboBox.Name = "lazySearchComboBox";
            this.lazySearchComboBox.Size = new System.Drawing.Size(149, 23);
            this.lazySearchComboBox.TabIndex = 20;
            this.lazySearchComboBox.SelectedIndexChanged += new System.EventHandler(this.lazySearchComboBox_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(33, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 15);
            this.label8.TabIndex = 19;
            this.label8.Text = "Stop At Position:";
            // 
            // stopAtPositionTextBox
            // 
            this.stopAtPositionTextBox.Location = new System.Drawing.Point(134, 51);
            this.stopAtPositionTextBox.Name = "stopAtPositionTextBox";
            this.stopAtPositionTextBox.Size = new System.Drawing.Size(116, 23);
            this.stopAtPositionTextBox.TabIndex = 18;
            this.stopAtPositionTextBox.Text = "0";
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(585, 61);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(196, 56);
            this.stopButton.TabIndex = 17;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 15);
            this.label7.TabIndex = 16;
            this.label7.Text = "Start Generation From:";
            // 
            // startChecksumPositionTextBox
            // 
            this.startChecksumPositionTextBox.Location = new System.Drawing.Point(134, 26);
            this.startChecksumPositionTextBox.Name = "startChecksumPositionTextBox";
            this.startChecksumPositionTextBox.Size = new System.Drawing.Size(183, 23);
            this.startChecksumPositionTextBox.TabIndex = 15;
            this.startChecksumPositionTextBox.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "Checksum Starts:";
            // 
            // currentChecksumPositionTextBox
            // 
            this.currentChecksumPositionTextBox.Location = new System.Drawing.Point(134, 55);
            this.currentChecksumPositionTextBox.Name = "currentChecksumPositionTextBox";
            this.currentChecksumPositionTextBox.ReadOnly = true;
            this.currentChecksumPositionTextBox.Size = new System.Drawing.Size(183, 23);
            this.currentChecksumPositionTextBox.TabIndex = 11;
            this.currentChecksumPositionTextBox.Text = "0";
            // 
            // startSearchButton
            // 
            this.startSearchButton.Enabled = false;
            this.startSearchButton.Location = new System.Drawing.Point(585, 6);
            this.startSearchButton.Name = "startSearchButton";
            this.startSearchButton.Size = new System.Drawing.Size(196, 55);
            this.startSearchButton.TabIndex = 10;
            this.startSearchButton.Text = "Start";
            this.startSearchButton.UseVisualStyleBackColor = true;
            this.startSearchButton.Click += new System.EventHandler(this.startSearchButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(375, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "File Length:";
            // 
            // fileLengthTextBox
            // 
            this.fileLengthTextBox.Location = new System.Drawing.Point(449, 22);
            this.fileLengthTextBox.Name = "fileLengthTextBox";
            this.fileLengthTextBox.ReadOnly = true;
            this.fileLengthTextBox.Size = new System.Drawing.Size(116, 23);
            this.fileLengthTextBox.TabIndex = 8;
            this.fileLengthTextBox.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Start From Position:";
            // 
            // startSearchTextBox
            // 
            this.startSearchTextBox.Location = new System.Drawing.Point(134, 22);
            this.startSearchTextBox.Name = "startSearchTextBox";
            this.startSearchTextBox.Size = new System.Drawing.Size(116, 23);
            this.startSearchTextBox.TabIndex = 6;
            this.startSearchTextBox.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select Checksum:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(116, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "File Location:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.bruteforceTabPage);
            this.tabControl1.Controls.Add(this.searchTabPage);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 225);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(806, 277);
            this.tabControl1.TabIndex = 9;
            // 
            // bruteforceTabPage
            // 
            this.bruteforceTabPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bruteforceTabPage.Controls.Add(this.groupBox3);
            this.bruteforceTabPage.Controls.Add(this.groupBox2);
            this.bruteforceTabPage.Controls.Add(this.groupBox1);
            this.bruteforceTabPage.Controls.Add(this.label1);
            this.bruteforceTabPage.Controls.Add(this.checksumComboBox);
            this.bruteforceTabPage.Controls.Add(this.startSearchButton);
            this.bruteforceTabPage.Controls.Add(this.stopButton);
            this.bruteforceTabPage.Location = new System.Drawing.Point(4, 24);
            this.bruteforceTabPage.Name = "bruteforceTabPage";
            this.bruteforceTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.bruteforceTabPage.Size = new System.Drawing.Size(798, 249);
            this.bruteforceTabPage.TabIndex = 0;
            this.bruteforceTabPage.Text = "Bruteforce For Checksums";
            this.bruteforceTabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.stopAtPositionTextBox);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.startSearchTextBox);
            this.groupBox3.Controls.Add(this.fileLengthTextBox);
            this.groupBox3.Location = new System.Drawing.Point(8, 35);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(571, 82);
            this.groupBox3.TabIndex = 37;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Search Information";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.checksumValueTextBox);
            this.groupBox2.Controls.Add(this.startChecksumPositionTextBox);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.currentChecksumPositionTextBox);
            this.groupBox2.Location = new System.Drawing.Point(8, 123);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(323, 115);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Checksum Information";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(60, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 15);
            this.label6.TabIndex = 18;
            this.label6.Text = "Checksum:";
            // 
            // checksumValueTextBox
            // 
            this.checksumValueTextBox.Location = new System.Drawing.Point(134, 84);
            this.checksumValueTextBox.Name = "checksumValueTextBox";
            this.checksumValueTextBox.ReadOnly = true;
            this.checksumValueTextBox.Size = new System.Drawing.Size(183, 23);
            this.checksumValueTextBox.TabIndex = 17;
            this.checksumValueTextBox.Text = "0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.startTimeTextBox);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.stopTimeTextBox);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Location = new System.Drawing.Point(337, 123);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 115);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Timer";
            // 
            // searchTabPage
            // 
            this.searchTabPage.Location = new System.Drawing.Point(4, 24);
            this.searchTabPage.Name = "searchTabPage";
            this.searchTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.searchTabPage.Size = new System.Drawing.Size(798, 249);
            this.searchTabPage.TabIndex = 1;
            this.searchTabPage.Text = "Search";
            this.searchTabPage.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(798, 249);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.usePCFCheckBox);
            this.groupBox4.Controls.Add(this.loadPCFButton);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.searchReverseValueCheckBox);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.skipBytesNumericUpDown);
            this.groupBox4.Controls.Add(this.byteSkippingCheckBox);
            this.groupBox4.Controls.Add(this.parallelComputingCheckBox);
            this.groupBox4.Controls.Add(this.lazySearchComboBox);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.lazyGenerateComboBox);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Location = new System.Drawing.Point(14, 63);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(570, 131);
            this.groupBox4.TabIndex = 38;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Miscellaneous";
            // 
            // usePCFCheckBox
            // 
            this.usePCFCheckBox.AutoSize = true;
            this.usePCFCheckBox.Location = new System.Drawing.Point(548, 108);
            this.usePCFCheckBox.Name = "usePCFCheckBox";
            this.usePCFCheckBox.Size = new System.Drawing.Size(15, 14);
            this.usePCFCheckBox.TabIndex = 40;
            this.usePCFCheckBox.UseVisualStyleBackColor = true;
            // 
            // loadPCFButton
            // 
            this.loadPCFButton.Location = new System.Drawing.Point(414, 105);
            this.loadPCFButton.Name = "loadPCFButton";
            this.loadPCFButton.Size = new System.Drawing.Size(125, 23);
            this.loadPCFButton.TabIndex = 39;
            this.loadPCFButton.Text = "Load";
            this.loadPCFButton.UseVisualStyleBackColor = true;
            this.loadPCFButton.Click += new System.EventHandler(this.loadPossibleChecksumFileButton_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(237, 108);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(162, 15);
            this.label16.TabIndex = 38;
            this.label16.Text = "Load Possible Checksum File:";
            // 
            // searchReverseValueCheckBox
            // 
            this.searchReverseValueCheckBox.AutoSize = true;
            this.searchReverseValueCheckBox.Location = new System.Drawing.Point(23, 106);
            this.searchReverseValueCheckBox.Name = "searchReverseValueCheckBox";
            this.searchReverseValueCheckBox.Size = new System.Drawing.Size(149, 19);
            this.searchReverseValueCheckBox.TabIndex = 37;
            this.searchReverseValueCheckBox.Text = "Search Reverse Values ?";
            this.searchReverseValueCheckBox.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(414, 76);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(149, 23);
            this.button1.TabIndex = 36;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(305, 80);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(94, 15);
            this.label15.TabIndex = 35;
            this.label15.Text = "Save / Continue:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.dragAndDropPanel);
            this.panel1.Controls.Add(this.possibleChecksumFileLocationTextBox);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.fileLocationTextBox);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(806, 201);
            this.panel1.TabIndex = 10;
            // 
            // dragAndDropPanel
            // 
            this.dragAndDropPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dragAndDropPanel.BackColor = System.Drawing.Color.DimGray;
            this.dragAndDropPanel.Location = new System.Drawing.Point(590, 3);
            this.dragAndDropPanel.Name = "dragAndDropPanel";
            this.dragAndDropPanel.Size = new System.Drawing.Size(211, 191);
            this.dragAndDropPanel.TabIndex = 11;
            // 
            // possibleChecksumFileLocationTextBox
            // 
            this.possibleChecksumFileLocationTextBox.Location = new System.Drawing.Point(199, 32);
            this.possibleChecksumFileLocationTextBox.Name = "possibleChecksumFileLocationTextBox";
            this.possibleChecksumFileLocationTextBox.ReadOnly = true;
            this.possibleChecksumFileLocationTextBox.Size = new System.Drawing.Size(385, 23);
            this.possibleChecksumFileLocationTextBox.TabIndex = 39;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 35);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(182, 15);
            this.label11.TabIndex = 40;
            this.label11.Text = "Possible Checksum File Location:";
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 504);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainForm";
            this.Text = "Crypto File Brute Force";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skipBytesNumericUpDown)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.bruteforceTabPage.ResumeLayout(false);
            this.bruteforceTabPage.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ComboBox checksumComboBox;
        private System.Windows.Forms.TextBox fileLocationTextBox;
        private System.Windows.Forms.Button startSearchButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox fileLengthTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox startSearchTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox startChecksumPositionTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox currentChecksumPositionTextBox;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox stopAtPositionTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox lazyGenerateComboBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox lazySearchComboBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox stopTimeTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox startTimeTextBox;
        private System.Windows.Forms.NumericUpDown skipBytesNumericUpDown;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox byteSkippingCheckBox;
        private System.Windows.Forms.CheckBox parallelComputingCheckBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage bruteforceTabPage;
        private System.Windows.Forms.TabPage searchTabPage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel dragAndDropPanel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox checksumValueTextBox;
        private System.Windows.Forms.CheckBox searchReverseValueCheckBox;
        private System.Windows.Forms.Button loadPCFButton;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox possibleChecksumFileLocationTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox usePCFCheckBox;
    }
}

