
namespace JetpackDowngraderGUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
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
            this.button6 = new DarkUI.Controls.DarkButton();
            this.button2 = new DarkUI.Controls.DarkButton();
            this.DSPanel = new DarkUI.Controls.DarkSectionPanel();
            this.darkTitle2 = new DarkUI.Controls.DarkTitle();
            this.darkTitle1 = new DarkUI.Controls.DarkTitle();
            this.button7 = new DarkUI.Controls.DarkButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.DSPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(366, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(166, 31);
            this.button1.TabIndex = 2;
            this.button1.TabStop = false;
            this.button1.Text = "3. Downgrade";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Green;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Arial", 9.75F);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(538, 20);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(166, 31);
            this.button3.TabIndex = 2;
            this.button3.TabStop = false;
            this.button3.Text = "4. Play && Test";
            this.button3.UseVisualStyleBackColor = false;
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
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button6.Location = new System.Drawing.Point(25, 20);
            this.button6.Name = "button6";
            this.button6.Padding = new System.Windows.Forms.Padding(5);
            this.button6.Size = new System.Drawing.Size(176, 31);
            this.button6.TabIndex = 8;
            this.button6.TabStop = false;
            this.button6.Text = "1. Downgrader Settings";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(207, 20);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(5);
            this.button2.Size = new System.Drawing.Size(153, 31);
            this.button2.TabIndex = 8;
            this.button2.TabStop = false;
            this.button2.Text = "2. Modifications";
            // 
            // DSPanel
            // 
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
            this.DSPanel.Location = new System.Drawing.Point(25, 67);
            this.DSPanel.Name = "DSPanel";
            this.DSPanel.SectionHeader = "Downgrader Settings";
            this.DSPanel.Size = new System.Drawing.Size(834, 323);
            this.DSPanel.TabIndex = 14;
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
            this.button7.Location = new System.Drawing.Point(323, 280);
            this.button7.Name = "button7";
            this.button7.Padding = new System.Windows.Forms.Padding(5);
            this.button7.Size = new System.Drawing.Size(167, 29);
            this.button7.TabIndex = 8;
            this.button7.TabStop = false;
            this.button7.Text = "Edit settings manually";
            this.button7.Click += new System.EventHandler(this.button7_Click);
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
            // pictureBox4
            // 
            this.pictureBox4.Image = global::JetpackDowngraderGUI.Properties.Resources.GitHub;
            this.pictureBox4.Location = new System.Drawing.Point(815, 16);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(44, 39);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 13;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::JetpackDowngraderGUI.Properties.Resources.JPD;
            this.pictureBox3.Location = new System.Drawing.Point(765, 16);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(44, 39);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 13;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::JetpackDowngraderGUI.Properties.Resources.Settings;
            this.pictureBox2.Location = new System.Drawing.Point(715, 16);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(44, 39);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.ClientSize = new System.Drawing.Size(886, 410);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.DSPanel);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Jetpack Downgrader";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DSPanel.ResumeLayout(false);
            this.DSPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private DarkUI.Controls.DarkTextBox GamePath;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DarkUI.Controls.DarkCheckBox checkBox1;
        private DarkUI.Controls.DarkCheckBox checkBox2;
        private DarkUI.Controls.DarkTitle label1;
        private DarkUI.Controls.DarkCheckBox checkBox9;
        private DarkUI.Controls.DarkCheckBox checkBox4;
        private DarkUI.Controls.DarkCheckBox checkBox6;
        private DarkUI.Controls.DarkCheckBox checkBox3;
        private DarkUI.Controls.DarkCheckBox checkBox5;
        private DarkUI.Controls.DarkCheckBox checkBox7;
        private DarkUI.Controls.DarkCheckBox checkBox8;
        private DarkUI.Controls.DarkButton button6;
        private DarkUI.Controls.DarkButton button2;
        private DarkUI.Controls.DarkSectionPanel DSPanel;
        private DarkUI.Controls.DarkTitle darkTitle1;
        private DarkUI.Controls.DarkTitle darkTitle2;
        private DarkUI.Controls.DarkButton button7;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
    }
}