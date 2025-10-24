namespace WindowsFormsApp1
{
    partial class frmLicenseApps
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
            this.btnLocalLicense = new ReaLTaiizor.Controls.DreamButton();
            this.btnIntLicense = new ReaLTaiizor.Controls.DreamButton();
            this.SuspendLayout();
            // 
            // btnLocalLicense
            // 
            this.btnLocalLicense.ColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btnLocalLicense.ColorB = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.btnLocalLicense.ColorC = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnLocalLicense.ColorD = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnLocalLicense.ColorE = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnLocalLicense.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLocalLicense.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(218)))), ((int)(((byte)(255)))));
            this.btnLocalLicense.Location = new System.Drawing.Point(16, 27);
            this.btnLocalLicense.Name = "btnLocalLicense";
            this.btnLocalLicense.Size = new System.Drawing.Size(264, 52);
            this.btnLocalLicense.TabIndex = 1;
            this.btnLocalLicense.Text = "Local Driving License Applications";
            this.btnLocalLicense.UseVisualStyleBackColor = true;
            this.btnLocalLicense.Click += new System.EventHandler(this.btnLocalLicense_Click);
            // 
            // btnIntLicense
            // 
            this.btnIntLicense.ColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btnIntLicense.ColorB = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.btnIntLicense.ColorC = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnIntLicense.ColorD = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnIntLicense.ColorE = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnIntLicense.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIntLicense.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(218)))), ((int)(((byte)(255)))));
            this.btnIntLicense.Location = new System.Drawing.Point(16, 108);
            this.btnIntLicense.Name = "btnIntLicense";
            this.btnIntLicense.Size = new System.Drawing.Size(264, 52);
            this.btnIntLicense.TabIndex = 2;
            this.btnIntLicense.Text = "International Driving License Applications";
            this.btnIntLicense.UseVisualStyleBackColor = true;
            this.btnIntLicense.Click += new System.EventHandler(this.btnIntLicense_Click);
            // 
            // frmLicenseApps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 197);
            this.Controls.Add(this.btnIntLicense);
            this.Controls.Add(this.btnLocalLicense);
            this.Name = "frmLicenseApps";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmLicenseApps";
            this.ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.Controls.DreamButton btnLocalLicense;
        private ReaLTaiizor.Controls.DreamButton btnIntLicense;
    }
}