
namespace JetpackDowngraderGUI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        System.ComponentModel.IContainer components = null;

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
        void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.GamePath = new DarkUI.Controls.DarkTextBox();
            this.checkBox1 = new DarkUI.Controls.DarkCheckBox();
            this.checkBox2 = new DarkUI.Controls.DarkCheckBox();
            this.label1 = new DarkUI.Controls.DarkTitle();
            this.checkBox9 = new DarkUI.Controls.DarkCheckBox();
            this.checkBox4 = new DarkUI.Controls.DarkCheckBox();
            this.checkBox6 = new DarkUI.Controls.DarkCheckBox();
            this.checkBox3 = new DarkUI.Controls.DarkCheckBox();
            this.checkBox5 = new DarkUI.Controls.DarkCheckBox();
            this.checkBox7 = new DarkUI.Controls.DarkCheckBox();
            this.checkBox8 = new DarkUI.Controls.DarkCheckBox();
            this.DSPanel = new DarkUI.Controls.DarkSectionPanel();
            this.ModsPanel = new DarkUI.Controls.DarkSectionPanel();
            this.darkTitle2 = new DarkUI.Controls.DarkTitle();
            this.darkTitle1 = new DarkUI.Controls.DarkTitle();
            this.button7 = new DarkUI.Controls.DarkButton();
            this.HelloUser = new DarkUI.Controls.DarkTitle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new DarkUI.Controls.DarkButton();
            this.button6 = new DarkUI.Controls.DarkButton();
            this.button1 = new System.Windows.Forms.Button();
            this.LangsPanel = new System.Windows.Forms.Panel();
            this.darkLabel1 = new DarkUI.Controls.DarkLabel();
            this.darkListView1 = new DarkUI.Controls.DarkListView();
            this.darkDockPanel1 = new DarkUI.Docking.DarkDockPanel();
            this.darkLabel2 = new DarkUI.Controls.DarkLabel();
            this.darkGroupBox1 = new DarkUI.Controls.DarkGroupBox();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.DSPanel.SuspendLayout();
            this.ModsPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.LangsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            this.SuspendLayout();
            // 
            // GamePath
            // 
            this.GamePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.GamePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GamePath.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GamePath.ForeColor = System.Drawing.Color.LightGray;
            this.GamePath.Location = new System.Drawing.Point(184, 43);
            this.GamePath.Name = "GamePath";
            this.GamePath.Size = new System.Drawing.Size(600, 22);
            this.GamePath.TabIndex = 9;
            this.GamePath.TabStop = false;
            // 
            // checkBox1
            // 
            this.checkBox1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.checkBox1.ForeColor = System.Drawing.Color.BlanchedAlmond;
            this.checkBox1.Location = new System.Drawing.Point(501, 140);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(258, 26);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.TabStop = false;
            this.checkBox1.Text = "Backup original files before downgrade";
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.Font = new System.Drawing.Font("Arial", 9.75F);
            this.checkBox2.ForeColor = System.Drawing.Color.Black;
            this.checkBox2.Location = new System.Drawing.Point(53, 137);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(180, 26);
            this.checkBox2.TabIndex = 11;
            this.checkBox2.TabStop = false;
            this.checkBox2.Text = "Make shortcut on Desktop";
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Gadugi", 11.25F);
            this.label1.Location = new System.Drawing.Point(9, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 19);
            this.label1.TabIndex = 12;
            this.label1.Text = "Path to the game folder:";
            // 
            // checkBox9
            // 
            this.checkBox9.Font = new System.Drawing.Font("Arial", 9.75F);
            this.checkBox9.ForeColor = System.Drawing.Color.Black;
            this.checkBox9.Location = new System.Drawing.Point(53, 163);
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.Size = new System.Drawing.Size(413, 26);
            this.checkBox9.TabIndex = 11;
            this.checkBox9.TabStop = false;
            this.checkBox9.Text = "Remove GTA_SA.SET (Reset game settings and prevents crash)";
            this.checkBox9.CheckedChanged += new System.EventHandler(this.checkBox9_CheckedChanged);
            // 
            // checkBox4
            // 
            this.checkBox4.Font = new System.Drawing.Font("Arial", 9.75F);
            this.checkBox4.ForeColor = System.Drawing.Color.Black;
            this.checkBox4.Location = new System.Drawing.Point(501, 165);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(299, 39);
            this.checkBox4.TabIndex = 11;
            this.checkBox4.TabStop = false;
            this.checkBox4.Text = "Remove unneeded files (ONLY for version \r\nfrom Rockstar Games Launcher)";
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // checkBox6
            // 
            this.checkBox6.Font = new System.Drawing.Font("Arial", 9.75F);
            this.checkBox6.ForeColor = System.Drawing.Color.Black;
            this.checkBox6.Location = new System.Drawing.Point(501, 202);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(258, 26);
            this.checkBox6.TabIndex = 11;
            this.checkBox6.TabStop = false;
            this.checkBox6.Text = "Register game path (Make game visible)";
            this.checkBox6.CheckedChanged += new System.EventHandler(this.checkBox6_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.Font = new System.Drawing.Font("Arial", 9.75F);
            this.checkBox3.ForeColor = System.Drawing.Color.Black;
            this.checkBox3.Location = new System.Drawing.Point(53, 189);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(413, 26);
            this.checkBox3.TabIndex = 11;
            this.checkBox3.TabStop = false;
            this.checkBox3.Text = "Move game to another folder (Prevents auto-update and rehash)";
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBox5
            // 
            this.checkBox5.Font = new System.Drawing.Font("Arial", 9.75F);
            this.checkBox5.ForeColor = System.Drawing.Color.Black;
            this.checkBox5.Location = new System.Drawing.Point(501, 228);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(200, 26);
            this.checkBox5.TabIndex = 11;
            this.checkBox5.TabStop = false;
            this.checkBox5.Text = "Forced (ONLY for version 1.0)";
            this.checkBox5.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
            // 
            // checkBox7
            // 
            this.checkBox7.Font = new System.Drawing.Font("Arial", 9.75F);
            this.checkBox7.ForeColor = System.Drawing.Color.Black;
            this.checkBox7.Location = new System.Drawing.Point(53, 216);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(226, 26);
            this.checkBox7.TabIndex = 11;
            this.checkBox7.TabStop = false;
            this.checkBox7.Text = "DirectPlay (ONLY for Windows 10)";
            this.checkBox7.CheckedChanged += new System.EventHandler(this.checkBox7_CheckedChanged);
            // 
            // checkBox8
            // 
            this.checkBox8.Font = new System.Drawing.Font("Arial", 9.75F);
            this.checkBox8.ForeColor = System.Drawing.Color.Black;
            this.checkBox8.Location = new System.Drawing.Point(53, 241);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(104, 26);
            this.checkBox8.TabIndex = 11;
            this.checkBox8.TabStop = false;
            this.checkBox8.Text = "Install DirectX";
            this.checkBox8.CheckedChanged += new System.EventHandler(this.checkBox8_CheckedChanged);
            // 
            // DSPanel
            // 
            this.DSPanel.Controls.Add(this.ModsPanel);
            this.DSPanel.Controls.Add(this.checkBox1);
            this.DSPanel.Controls.Add(this.darkTitle2);
            this.DSPanel.Controls.Add(this.darkTitle1);
            this.DSPanel.Controls.Add(this.button7);
            this.DSPanel.Controls.Add(this.label1);
            this.DSPanel.Controls.Add(this.pictureBox1);
            this.DSPanel.Controls.Add(this.GamePath);
            this.DSPanel.Controls.Add(this.checkBox2);
            this.DSPanel.Controls.Add(this.checkBox9);
            this.DSPanel.Controls.Add(this.checkBox4);
            this.DSPanel.Controls.Add(this.checkBox8);
            this.DSPanel.Controls.Add(this.checkBox6);
            this.DSPanel.Controls.Add(this.checkBox7);
            this.DSPanel.Controls.Add(this.checkBox3);
            this.DSPanel.Controls.Add(this.checkBox5);
            this.DSPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DSPanel.Location = new System.Drawing.Point(12, 68);
            this.DSPanel.Name = "DSPanel";
            this.DSPanel.SectionHeader = "Downgrader Settings";
            this.DSPanel.Size = new System.Drawing.Size(834, 336);
            this.DSPanel.TabIndex = 14;
            this.DSPanel.Visible = false;
            // 
            // ModsPanel
            // 
            this.ModsPanel.Controls.Add(this.darkGroupBox1);
            this.ModsPanel.Controls.Add(this.darkLabel2);
            this.ModsPanel.Controls.Add(this.darkListView1);
            this.ModsPanel.Controls.Add(this.darkDockPanel1);
            this.ModsPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ModsPanel.Location = new System.Drawing.Point(1, 0);
            this.ModsPanel.Name = "ModsPanel";
            this.ModsPanel.SectionHeader = "Modifications";
            this.ModsPanel.Size = new System.Drawing.Size(834, 336);
            this.ModsPanel.TabIndex = 15;
            this.ModsPanel.Visible = false;
            // 
            // darkTitle2
            // 
            this.darkTitle2.AutoSize = true;
            this.darkTitle2.Font = new System.Drawing.Font("Gadugi", 11.25F);
            this.darkTitle2.Location = new System.Drawing.Point(570, 108);
            this.darkTitle2.Name = "darkTitle2";
            this.darkTitle2.Size = new System.Drawing.Size(122, 19);
            this.darkTitle2.TabIndex = 12;
            this.darkTitle2.Text = "Optional settings";
            // 
            // darkTitle1
            // 
            this.darkTitle1.AutoSize = true;
            this.darkTitle1.Font = new System.Drawing.Font("Gadugi", 11.25F);
            this.darkTitle1.Location = new System.Drawing.Point(133, 108);
            this.darkTitle1.Name = "darkTitle1";
            this.darkTitle1.Size = new System.Drawing.Size(165, 19);
            this.darkTitle1.TabIndex = 12;
            this.darkTitle1.Text = "Recommended settings";
            // 
            // button7
            // 
            this.button7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button7.Location = new System.Drawing.Point(323, 287);
            this.button7.Name = "button7";
            this.button7.Padding = new System.Windows.Forms.Padding(5);
            this.button7.Size = new System.Drawing.Size(167, 29);
            this.button7.TabIndex = 8;
            this.button7.TabStop = false;
            this.button7.Text = "Edit settings manually";
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // HelloUser
            // 
            this.HelloUser.Font = new System.Drawing.Font("Arial", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.HelloUser.Location = new System.Drawing.Point(167, 146);
            this.HelloUser.Name = "HelloUser";
            this.HelloUser.Size = new System.Drawing.Size(393, 46);
            this.HelloUser.TabIndex = 12;
            this.HelloUser.Text = "Select the desired stage";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox4);
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.pictureBox8);
            this.panel1.Location = new System.Drawing.Point(-52, -10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(939, 66);
            this.panel1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(348, 21);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(5);
            this.button2.Size = new System.Drawing.Size(153, 31);
            this.button2.TabIndex = 16;
            this.button2.TabStop = false;
            this.button2.Text = "2. Modifications";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button6.Location = new System.Drawing.Point(166, 21);
            this.button6.Name = "button6";
            this.button6.Padding = new System.Windows.Forms.Padding(5);
            this.button6.Size = new System.Drawing.Size(176, 31);
            this.button6.TabIndex = 15;
            this.button6.TabStop = false;
            this.button6.Text = "1. Downgrader Settings";
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(507, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(166, 31);
            this.button1.TabIndex = 14;
            this.button1.TabStop = false;
            this.button1.Text = "3. Downgrade";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // LangsPanel
            // 
            this.LangsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LangsPanel.Controls.Add(this.darkLabel1);
            this.LangsPanel.Controls.Add(this.pictureBox9);
            this.LangsPanel.Location = new System.Drawing.Point(652, 55);
            this.LangsPanel.Name = "LangsPanel";
            this.LangsPanel.Size = new System.Drawing.Size(117, 122);
            this.LangsPanel.TabIndex = 0;
            this.LangsPanel.Visible = false;
            // 
            // darkLabel1
            // 
            this.darkLabel1.AutoSize = true;
            this.darkLabel1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.darkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel1.Location = new System.Drawing.Point(21, 12);
            this.darkLabel1.Name = "darkLabel1";
            this.darkLabel1.Size = new System.Drawing.Size(71, 16);
            this.darkLabel1.TabIndex = 3;
            this.darkLabel1.Text = "Languages";
            this.darkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // darkListView1
            // 
            this.darkListView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.darkListView1.Location = new System.Drawing.Point(25, 71);
            this.darkListView1.MultiSelect = true;
            this.darkListView1.Name = "darkListView1";
            this.darkListView1.Size = new System.Drawing.Size(366, 243);
            this.darkListView1.TabIndex = 0;
            this.darkListView1.Text = "darkListView1";
            // 
            // darkDockPanel1
            // 
            this.darkDockPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.darkDockPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.darkDockPanel1.Location = new System.Drawing.Point(12, 63);
            this.darkDockPanel1.Name = "darkDockPanel1";
            this.darkDockPanel1.Size = new System.Drawing.Size(390, 262);
            this.darkDockPanel1.TabIndex = 1;
            // 
            // darkLabel2
            // 
            this.darkLabel2.AutoSize = true;
            this.darkLabel2.Font = new System.Drawing.Font("Arial", 9.75F);
            this.darkLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel2.Location = new System.Drawing.Point(10, 34);
            this.darkLabel2.Name = "darkLabel2";
            this.darkLabel2.Size = new System.Drawing.Size(84, 16);
            this.darkLabel2.TabIndex = 3;
            this.darkLabel2.Text = "Modifications";
            this.darkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // darkGroupBox1
            // 
            this.darkGroupBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.darkGroupBox1.Location = new System.Drawing.Point(418, 30);
            this.darkGroupBox1.Name = "darkGroupBox1";
            this.darkGroupBox1.Size = new System.Drawing.Size(401, 284);
            this.darkGroupBox1.TabIndex = 4;
            this.darkGroupBox1.TabStop = false;
            this.darkGroupBox1.Text = "About";
            // 
            // pictureBox9
            // 
            this.pictureBox9.Image = global::JetpackDowngraderGUI.Properties.Resources.EN;
            this.pictureBox9.Location = new System.Drawing.Point(22, 43);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(71, 62);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox9.TabIndex = 13;
            this.pictureBox9.TabStop = false;
            this.pictureBox9.Click += new System.EventHandler(this.pictureBox9_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::JetpackDowngraderGUI.Properties.Resources.Language;
            this.pictureBox2.Location = new System.Drawing.Point(734, 18);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(44, 34);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::JetpackDowngraderGUI.Properties.Resources.GitHub;
            this.pictureBox4.Location = new System.Drawing.Point(822, 18);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(44, 34);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 13;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::JetpackDowngraderGUI.Properties.Resources.JPD;
            this.pictureBox3.Location = new System.Drawing.Point(778, 18);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(44, 34);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 13;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(790, 43);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 22);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = global::JetpackDowngraderGUI.Properties.Resources.Up;
            this.pictureBox7.Location = new System.Drawing.Point(514, 77);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(44, 39);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox7.TabIndex = 13;
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::JetpackDowngraderGUI.Properties.Resources.Up;
            this.pictureBox6.Location = new System.Drawing.Point(352, 77);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(44, 39);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 13;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::JetpackDowngraderGUI.Properties.Resources.Up;
            this.pictureBox5.Location = new System.Drawing.Point(170, 77);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(44, 39);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 13;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = global::JetpackDowngraderGUI.Properties.Resources.Patches;
            this.pictureBox8.Location = new System.Drawing.Point(694, 18);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(34, 34);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox8.TabIndex = 16;
            this.pictureBox8.TabStop = false;
            this.pictureBox8.Click += new System.EventHandler(this.pictureBox8_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.ClientSize = new System.Drawing.Size(859, 416);
            this.Controls.Add(this.LangsPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.DSPanel);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.HelloUser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Jetpack GUI";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DSPanel.ResumeLayout(false);
            this.DSPanel.PerformLayout();
            this.ModsPanel.ResumeLayout(false);
            this.ModsPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.LangsPanel.ResumeLayout(false);
            this.LangsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        DarkUI.Controls.DarkTextBox GamePath;
        System.Windows.Forms.PictureBox pictureBox1;
        DarkUI.Controls.DarkCheckBox checkBox1;
        DarkUI.Controls.DarkCheckBox checkBox2;
        DarkUI.Controls.DarkTitle label1;
        DarkUI.Controls.DarkCheckBox checkBox9;
        DarkUI.Controls.DarkCheckBox checkBox4;
        DarkUI.Controls.DarkCheckBox checkBox6;
        DarkUI.Controls.DarkCheckBox checkBox3;
        DarkUI.Controls.DarkCheckBox checkBox5;
        DarkUI.Controls.DarkCheckBox checkBox7;
        DarkUI.Controls.DarkCheckBox checkBox8;
        DarkUI.Controls.DarkSectionPanel DSPanel;
        DarkUI.Controls.DarkTitle darkTitle1;
        DarkUI.Controls.DarkTitle darkTitle2;
        DarkUI.Controls.DarkButton button7;
        System.Windows.Forms.PictureBox pictureBox3;
        System.Windows.Forms.PictureBox pictureBox4;
        DarkUI.Controls.DarkTitle HelloUser;
        System.Windows.Forms.PictureBox pictureBox5;
        System.Windows.Forms.PictureBox pictureBox6;
        System.Windows.Forms.PictureBox pictureBox7;
        DarkUI.Controls.DarkSectionPanel ModsPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private DarkUI.Controls.DarkButton button2;
        private DarkUI.Controls.DarkButton button6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel LangsPanel;
        private DarkUI.Controls.DarkLabel darkLabel1;
        private System.Windows.Forms.PictureBox pictureBox9;
        private DarkUI.Controls.DarkListView darkListView1;
        private DarkUI.Controls.DarkLabel darkLabel2;
        private DarkUI.Docking.DarkDockPanel darkDockPanel1;
        private DarkUI.Controls.DarkGroupBox darkGroupBox1;
        private System.Windows.Forms.PictureBox pictureBox8;
    }
}