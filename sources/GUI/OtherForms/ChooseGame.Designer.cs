
namespace JetpackGUI
{
    partial class ChooseGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseGame));
            this.label3 = new System.Windows.Forms.Label();
            this.vitNX_Button1 = new VitNX.UI.ControlsV1.Controls.VitNX_Button();
            this.vitNX_Button2 = new VitNX.UI.ControlsV1.Controls.VitNX_Button();
            this.vitNX_Button3 = new VitNX.UI.ControlsV1.Controls.VitNX_Button();
            this.vitNX_Button4 = new VitNX.UI.ControlsV1.Controls.VitNX_Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label3.Font = new System.Drawing.Font("Gadugi", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Gainsboro;
            this.label3.Location = new System.Drawing.Point(29, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(445, 40);
            this.label3.TabIndex = 89903;
            this.label3.Text = "Choose a game";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // vitNX_Button1
            // 
            this.vitNX_Button1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.vitNX_Button1.Location = new System.Drawing.Point(36, 85);
            this.vitNX_Button1.Name = "vitNX_Button1";
            this.vitNX_Button1.Padding = new System.Windows.Forms.Padding(5);
            this.vitNX_Button1.Size = new System.Drawing.Size(207, 30);
            this.vitNX_Button1.TabIndex = 17;
            this.vitNX_Button1.TabStop = false;
            this.vitNX_Button1.Text = "Grand Theft Auto: Vice City";
            this.vitNX_Button1.Click += new System.EventHandler(this.button2_Click);
            // 
            // vitNX_Button2
            // 
            this.vitNX_Button2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.vitNX_Button2.Location = new System.Drawing.Point(36, 130);
            this.vitNX_Button2.Name = "vitNX_Button2";
            this.vitNX_Button2.Padding = new System.Windows.Forms.Padding(5);
            this.vitNX_Button2.Size = new System.Drawing.Size(207, 30);
            this.vitNX_Button2.TabIndex = 17;
            this.vitNX_Button2.TabStop = false;
            this.vitNX_Button2.Text = "Grand Theft Auto: San Andreas";
            this.vitNX_Button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // vitNX_Button3
            // 
            this.vitNX_Button3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.vitNX_Button3.Location = new System.Drawing.Point(267, 85);
            this.vitNX_Button3.Name = "vitNX_Button3";
            this.vitNX_Button3.Padding = new System.Windows.Forms.Padding(5);
            this.vitNX_Button3.Size = new System.Drawing.Size(207, 30);
            this.vitNX_Button3.TabIndex = 17;
            this.vitNX_Button3.TabStop = false;
            this.vitNX_Button3.Text = "Grand Theft Auto III";
            this.vitNX_Button3.Click += new System.EventHandler(this.button2_Click);
            // 
            // vitNX_Button4
            // 
            this.vitNX_Button4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.vitNX_Button4.Location = new System.Drawing.Point(267, 130);
            this.vitNX_Button4.Name = "vitNX_Button4";
            this.vitNX_Button4.Padding = new System.Windows.Forms.Padding(5);
            this.vitNX_Button4.Size = new System.Drawing.Size(207, 30);
            this.vitNX_Button4.TabIndex = 17;
            this.vitNX_Button4.TabStop = false;
            this.vitNX_Button4.Text = "Grand Theft Auto IV";
            this.vitNX_Button4.Click += new System.EventHandler(this.button2_Click);
            // 
            // ChooseGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.ClientSize = new System.Drawing.Size(502, 185);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.vitNX_Button1);
            this.Controls.Add(this.vitNX_Button2);
            this.Controls.Add(this.vitNX_Button3);
            this.Controls.Add(this.vitNX_Button4);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChooseGame";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select the desired part of the game series";
            this.Load += new System.EventHandler(this.MyLang_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private VitNX.UI.ControlsV1.Controls.VitNX_Button vitNX_Button1;
        private VitNX.UI.ControlsV1.Controls.VitNX_Button vitNX_Button2;
        private VitNX.UI.ControlsV1.Controls.VitNX_Button vitNX_Button3;
        private VitNX.UI.ControlsV1.Controls.VitNX_Button vitNX_Button4;
    }
}