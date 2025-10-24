using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1
{
    partial class frmLoginScreen
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
            this.panel2 = new ReaLTaiizor.Controls.Panel();
            this.bigLabel1 = new ReaLTaiizor.Controls.BigLabel();
            this.btnLogin = new ReaLTaiizor.Controls.CyberButton();
            this.chkRememberMe = new ReaLTaiizor.Controls.ForeverToggle();
            this.nightControlBox1 = new ReaLTaiizor.Controls.NightControlBox();
            this.materialButton2 = new ReaLTaiizor.Controls.MaterialButton();
            this.materialButton1 = new ReaLTaiizor.Controls.MaterialButton();
            this.dungeonLinkLabel1 = new ReaLTaiizor.Controls.DungeonLinkLabel();
            this.materialLabel1 = new ReaLTaiizor.Controls.MaterialLabel();
            this.txtPassword = new ReaLTaiizor.Controls.HopeTextBox();
            this.txtUserName = new ReaLTaiizor.Controls.HopeTextBox();
            this.panel1 = new ReaLTaiizor.Controls.Panel();
            this.hopePictureBox1 = new ReaLTaiizor.Controls.HopePictureBox();
            this.foreverLabel1 = new ReaLTaiizor.Controls.ForeverLabel();
            this.foreverLabel2 = new ReaLTaiizor.Controls.ForeverLabel();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hopePictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(12)))), ((int)(((byte)(45)))));
            this.panel2.Controls.Add(this.foreverLabel2);
            this.panel2.Controls.Add(this.foreverLabel1);
            this.panel2.Controls.Add(this.bigLabel1);
            this.panel2.Controls.Add(this.btnLogin);
            this.panel2.Controls.Add(this.chkRememberMe);
            this.panel2.Controls.Add(this.nightControlBox1);
            this.panel2.Controls.Add(this.materialButton2);
            this.panel2.Controls.Add(this.materialButton1);
            this.panel2.Controls.Add(this.dungeonLinkLabel1);
            this.panel2.Controls.Add(this.materialLabel1);
            this.panel2.Controls.Add(this.txtPassword);
            this.panel2.Controls.Add(this.txtUserName);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(41)))), ((int)(((byte)(50)))));
            this.panel2.Location = new System.Drawing.Point(458, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.panel2.Size = new System.Drawing.Size(315, 424);
            this.panel2.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.panel2.TabIndex = 1;
            this.panel2.Click += new System.EventHandler(this.panel2_Click);
            // 
            // bigLabel1
            // 
            this.bigLabel1.BackColor = System.Drawing.Color.Transparent;
            this.bigLabel1.Font = new System.Drawing.Font("Segoe UI", 23F);
            this.bigLabel1.ForeColor = System.Drawing.Color.White;
            this.bigLabel1.Location = new System.Drawing.Point(-2, 34);
            this.bigLabel1.Name = "bigLabel1";
            this.bigLabel1.Size = new System.Drawing.Size(309, 95);
            this.bigLabel1.TabIndex = 26;
            this.bigLabel1.Text = "Driving && Vehicle License Managment";
            // 
            // btnLogin
            // 
            this.btnLogin.Alpha = 20;
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.Background = true;
            this.btnLogin.Background_WidthPen = 4F;
            this.btnLogin.BackgroundPen = true;
            this.btnLogin.ColorBackground = System.Drawing.Color.Teal;
            this.btnLogin.ColorBackground_1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(52)))), ((int)(((byte)(68)))));
            this.btnLogin.ColorBackground_2 = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(63)))), ((int)(((byte)(86)))));
            this.btnLogin.ColorBackground_Pen = System.Drawing.Color.MediumSeaGreen;
            this.btnLogin.ColorLighting = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(200)))), ((int)(((byte)(238)))));
            this.btnLogin.ColorPen_1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(52)))), ((int)(((byte)(68)))));
            this.btnLogin.ColorPen_2 = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(63)))), ((int)(((byte)(86)))));
            this.btnLogin.CyberButtonStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            this.btnLogin.Effect_1 = true;
            this.btnLogin.Effect_1_ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(200)))), ((int)(((byte)(238)))));
            this.btnLogin.Effect_1_Transparency = 25;
            this.btnLogin.Effect_2 = true;
            this.btnLogin.Effect_2_ColorBackground = System.Drawing.Color.White;
            this.btnLogin.Effect_2_Transparency = 20;
            this.btnLogin.Font = new System.Drawing.Font("Arial", 11F);
            this.btnLogin.ForeColor = System.Drawing.Color.Black;
            this.btnLogin.Lighting = false;
            this.btnLogin.LinearGradient_Background = false;
            this.btnLogin.LinearGradientPen = false;
            this.btnLogin.Location = new System.Drawing.Point(202, 275);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.PenWidth = 15;
            this.btnLogin.Rounding = true;
            this.btnLogin.RoundingInt = 70;
            this.btnLogin.Size = new System.Drawing.Size(111, 50);
            this.btnLogin.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.btnLogin.TabIndex = 25;
            this.btnLogin.Tag = "Cyber";
            this.btnLogin.TextButton = "Sign In";
            this.btnLogin.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.btnLogin.Timer_Effect_1 = 5;
            this.btnLogin.Timer_RGB = 300;
            this.btnLogin.Click += new System.EventHandler(this.cyberButton1_Click);
            // 
            // chkRememberMe
            // 
            this.chkRememberMe.BackColor = System.Drawing.Color.Transparent;
            this.chkRememberMe.BaseColor = System.Drawing.Color.Teal;
            this.chkRememberMe.BaseColorRed = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(85)))), ((int)(((byte)(96)))));
            this.chkRememberMe.BGColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(85)))), ((int)(((byte)(86)))));
            this.chkRememberMe.Checked = true;
            this.chkRememberMe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkRememberMe.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkRememberMe.Location = new System.Drawing.Point(6, 281);
            this.chkRememberMe.Name = "chkRememberMe";
            this.chkRememberMe.Options = ReaLTaiizor.Controls.ForeverToggle._Options.Style1;
            this.chkRememberMe.Size = new System.Drawing.Size(76, 33);
            this.chkRememberMe.TabIndex = 23;
            this.chkRememberMe.Text = "foreverToggle1";
            this.chkRememberMe.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.chkRememberMe.ToggleColor = System.Drawing.Color.Black;
            // 
            // nightControlBox1
            // 
            this.nightControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nightControlBox1.BackColor = System.Drawing.Color.Transparent;
            this.nightControlBox1.CloseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.nightControlBox1.CloseHoverForeColor = System.Drawing.Color.White;
            this.nightControlBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nightControlBox1.DefaultLocation = true;
            this.nightControlBox1.DisableMaximizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.nightControlBox1.DisableMinimizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.nightControlBox1.EnableCloseColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.nightControlBox1.EnableMaximizeButton = false;
            this.nightControlBox1.EnableMaximizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.nightControlBox1.EnableMinimizeButton = true;
            this.nightControlBox1.EnableMinimizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.nightControlBox1.Location = new System.Drawing.Point(176, 0);
            this.nightControlBox1.MaximizeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.nightControlBox1.MaximizeHoverForeColor = System.Drawing.Color.White;
            this.nightControlBox1.MinimizeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.nightControlBox1.MinimizeHoverForeColor = System.Drawing.Color.White;
            this.nightControlBox1.Name = "nightControlBox1";
            this.nightControlBox1.Size = new System.Drawing.Size(139, 31);
            this.nightControlBox1.TabIndex = 6;
            this.nightControlBox1.Click += new System.EventHandler(this.nightControlBox1_Click);
            // 
            // materialButton2
            // 
            this.materialButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.materialButton2.AutoSize = false;
            this.materialButton2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton2.CharacterCasing = ReaLTaiizor.Controls.MaterialButton.CharacterCasingEnum.Normal;
            this.materialButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.materialButton2.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton2.Depth = 0;
            this.materialButton2.HighEmphasis = true;
            this.materialButton2.Icon = null;
            this.materialButton2.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            this.materialButton2.Location = new System.Drawing.Point(2627, 40);
            this.materialButton2.Margin = new System.Windows.Forms.Padding(64, 44, 64, 44);
            this.materialButton2.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.materialButton2.Name = "materialButton2";
            this.materialButton2.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton2.Size = new System.Drawing.Size(1728, 266);
            this.materialButton2.TabIndex = 21;
            this.materialButton2.Text = "Sign Up";
            this.materialButton2.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton2.UseAccentColor = false;
            this.materialButton2.UseVisualStyleBackColor = true;
            // 
            // materialButton1
            // 
            this.materialButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.materialButton1.AutoSize = false;
            this.materialButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton1.CharacterCasing = ReaLTaiizor.Controls.MaterialButton.CharacterCasingEnum.Normal;
            this.materialButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.materialButton1.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton1.Depth = 0;
            this.materialButton1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.materialButton1.HighEmphasis = true;
            this.materialButton1.Icon = null;
            this.materialButton1.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            this.materialButton1.Location = new System.Drawing.Point(160, 3481);
            this.materialButton1.Margin = new System.Windows.Forms.Padding(64, 44, 64, 44);
            this.materialButton1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.materialButton1.Name = "materialButton1";
            this.materialButton1.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton1.Size = new System.Drawing.Size(1728, 266);
            this.materialButton1.TabIndex = 20;
            this.materialButton1.Text = "Sign In";
            this.materialButton1.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton1.UseAccentColor = false;
            // 
            // dungeonLinkLabel1
            // 
            this.dungeonLinkLabel1.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(72)))), ((int)(((byte)(20)))));
            this.dungeonLinkLabel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.dungeonLinkLabel1.AutoSize = true;
            this.dungeonLinkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.dungeonLinkLabel1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dungeonLinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.dungeonLinkLabel1.LinkColor = System.Drawing.Color.Cyan;
            this.dungeonLinkLabel1.Location = new System.Drawing.Point(6, 237);
            this.dungeonLinkLabel1.Name = "dungeonLinkLabel1";
            this.dungeonLinkLabel1.Size = new System.Drawing.Size(118, 20);
            this.dungeonLinkLabel1.TabIndex = 18;
            this.dungeonLinkLabel1.TabStop = true;
            this.dungeonLinkLabel1.Text = "Forgot Password";
            this.dungeonLinkLabel1.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(119)))), ((int)(((byte)(70)))));
            // 
            // materialLabel1
            // 
            this.materialLabel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.materialLabel1.HighEmphasis = true;
            this.materialLabel1.Location = new System.Drawing.Point(88, 290);
            this.materialLabel1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(103, 19);
            this.materialLabel1.TabIndex = 12;
            this.materialLabel1.Text = "Remember me";
            this.materialLabel1.UseAccent = true;
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(12)))), ((int)(((byte)(45)))));
            this.txtPassword.BaseColor = System.Drawing.Color.Transparent;
            this.txtPassword.BorderColorA = System.Drawing.Color.DodgerBlue;
            this.txtPassword.BorderColorB = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(181)))));
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtPassword.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtPassword.Hint = "";
            this.txtPassword.Location = new System.Drawing.Point(6, 187);
            this.txtPassword.MaxLength = 128;
            this.txtPassword.Multiline = false;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPassword.SelectedText = "";
            this.txtPassword.SelectionLength = 0;
            this.txtPassword.SelectionStart = 0;
            this.txtPassword.Size = new System.Drawing.Size(295, 38);
            this.txtPassword.TabIndex = 8;
            this.txtPassword.TabStop = false;
            this.txtPassword.Text = "Enter Password";
            this.txtPassword.UseSystemPasswordChar = false;
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(12)))), ((int)(((byte)(45)))));
            this.txtUserName.BaseColor = System.Drawing.Color.Transparent;
            this.txtUserName.BorderColorA = System.Drawing.Color.DodgerBlue;
            this.txtUserName.BorderColorB = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(181)))));
            this.txtUserName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtUserName.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtUserName.Hint = "";
            this.txtUserName.Location = new System.Drawing.Point(6, 132);
            this.txtUserName.MaxLength = 128;
            this.txtUserName.Multiline = false;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.PasswordChar = '\0';
            this.txtUserName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUserName.SelectedText = "";
            this.txtUserName.SelectionLength = 0;
            this.txtUserName.SelectionStart = 0;
            this.txtUserName.Size = new System.Drawing.Size(295, 38);
            this.txtUserName.TabIndex = 7;
            this.txtUserName.TabStop = false;
            this.txtUserName.Text = "Enter UserName";
            this.txtUserName.UseSystemPasswordChar = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.Bakcground2;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.hopePictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(41)))), ((int)(((byte)(50)))));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(458, 424);
            this.panel1.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.panel1.TabIndex = 0;
            // 
            // hopePictureBox1
            // 
            this.hopePictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(196)))), ((int)(((byte)(204)))));
            this.hopePictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hopePictureBox1.Image = global::WindowsFormsApp1.Properties.Resources.wallpapersden2;
            this.hopePictureBox1.Location = new System.Drawing.Point(5, 5);
            this.hopePictureBox1.Name = "hopePictureBox1";
            this.hopePictureBox1.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            this.hopePictureBox1.Size = new System.Drawing.Size(448, 414);
            this.hopePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.hopePictureBox1.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.hopePictureBox1.TabIndex = 0;
            this.hopePictureBox1.TabStop = false;
            this.hopePictureBox1.TextRenderingType = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // foreverLabel1
            // 
            this.foreverLabel1.AutoSize = true;
            this.foreverLabel1.BackColor = System.Drawing.Color.Transparent;
            this.foreverLabel1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.foreverLabel1.ForeColor = System.Drawing.Color.LightGray;
            this.foreverLabel1.Location = new System.Drawing.Point(8, 343);
            this.foreverLabel1.Name = "foreverLabel1";
            this.foreverLabel1.Size = new System.Drawing.Size(167, 25);
            this.foreverLabel1.TabIndex = 27;
            this.foreverLabel1.Text = "UserName: Demo";
            // 
            // foreverLabel2
            // 
            this.foreverLabel2.AutoSize = true;
            this.foreverLabel2.BackColor = System.Drawing.Color.Transparent;
            this.foreverLabel2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.foreverLabel2.ForeColor = System.Drawing.Color.LightGray;
            this.foreverLabel2.Location = new System.Drawing.Point(8, 379);
            this.foreverLabel2.Name = "foreverLabel2";
            this.foreverLabel2.Size = new System.Drawing.Size(158, 25);
            this.foreverLabel2.TabIndex = 28;
            this.foreverLabel2.Text = "Password: demo";
            // 
            // frmLoginScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(773, 424);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmLoginScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form5";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.frmLoginScreen_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmLoginScreen_KeyPress);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hopePictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.Controls.NightControlBox nightControlBox1;
        private ReaLTaiizor.Controls.Panel panel1;
        private ReaLTaiizor.Controls.Panel panel2;
        private ReaLTaiizor.Controls.MaterialButton materialButton1;
        private ReaLTaiizor.Controls.MaterialButton materialButton2;
        private ReaLTaiizor.Controls.DungeonLinkLabel dungeonLinkLabel1;
        private ReaLTaiizor.Controls.MaterialLabel materialLabel1;
        private ReaLTaiizor.Controls.HopeTextBox txtUserName;
        private ReaLTaiizor.Controls.HopeTextBox txtPassword;
        private ReaLTaiizor.Controls.HopePictureBox hopePictureBox1;
        private ReaLTaiizor.Controls.ForeverToggle chkRememberMe;
        private ReaLTaiizor.Controls.CyberButton btnLogin;
        private ReaLTaiizor.Controls.BigLabel bigLabel1;
        private ReaLTaiizor.Controls.ForeverLabel foreverLabel2;
        private ReaLTaiizor.Controls.ForeverLabel foreverLabel1;
    }
}