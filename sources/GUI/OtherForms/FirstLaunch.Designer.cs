
namespace JetpackGUI
{
    partial class MyLang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyLang));
            this.darkLabel1 = new VitNX.Controls.VitNX_Label();
            this.AllLangs = new VitNX.Controls.VitNX_ComboBox();
            this.button2 = new VitNX.Controls.VitNX_Button();
            this.SuspendLayout();
            // 
            // darkLabel1
            // 
            this.darkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.darkLabel1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.darkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel1.Location = new System.Drawing.Point(18, 14);
            this.darkLabel1.Name = "darkLabel1";
            this.darkLabel1.Size = new System.Drawing.Size(210, 16);
            this.darkLabel1.TabIndex = 4;
            this.darkLabel1.Text = "Select a localization from the list";
            this.darkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AllLangs
            // 
            this.AllLangs.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.AllLangs.FormattingEnabled = true;
            this.AllLangs.Location = new System.Drawing.Point(21, 41);
            this.AllLangs.Name = "AllLangs";
            this.AllLangs.Size = new System.Drawing.Size(207, 22);
            this.AllLangs.TabIndex = 7;
            this.AllLangs.SelectedIndexChanged += new System.EventHandler(this.AllLangs_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(21, 73);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(5);
            this.button2.Size = new System.Drawing.Size(207, 30);
            this.button2.TabIndex = 17;
            this.button2.TabStop = false;
            this.button2.Text = "Apply and launch";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // MyLang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.ClientSize = new System.Drawing.Size(243, 116);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.AllLangs);
            this.Controls.Add(this.darkLabel1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MyLang";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "First launch";
            this.Load += new System.EventHandler(this.MyLang_Load);
            this.ResumeLayout(false);

        }

        #endregion

       VitNX.Controls.VitNX_Label darkLabel1;
       VitNX.Controls.VitNX_ComboBox AllLangs;
       VitNX.Controls.VitNX_Button button2;
    }
}