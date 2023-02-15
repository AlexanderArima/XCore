
namespace XCore.PMS.Winform.View
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
            this.dSkinPanel1 = new DSkin.Controls.DSkinPanel();
            this.dSkinTabControl1 = new DSkin.Controls.DSkinTabControl();
            this.dSkinTabPage1 = new DSkin.Controls.DSkinTabPage();
            this.gnlkControl1 = new XCore.PMS.Winform.Control.GNLKControl();
            this.dSkinTabPage2 = new DSkin.Controls.DSkinTabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.房间管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dSkinPanel1.SuspendLayout();
            this.dSkinTabControl1.SuspendLayout();
            this.dSkinTabPage1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dSkinPanel1
            // 
            this.dSkinPanel1.BackColor = System.Drawing.Color.Transparent;
            this.dSkinPanel1.Controls.Add(this.dSkinTabControl1);
            this.dSkinPanel1.Location = new System.Drawing.Point(7, 69);
            this.dSkinPanel1.Name = "dSkinPanel1";
            this.dSkinPanel1.RightBottom = ((System.Drawing.Image)(resources.GetObject("dSkinPanel1.RightBottom")));
            this.dSkinPanel1.Size = new System.Drawing.Size(786, 624);
            this.dSkinPanel1.TabIndex = 0;
            this.dSkinPanel1.Text = "dSkinPanel1";
            // 
            // dSkinTabControl1
            // 
            this.dSkinTabControl1.BitmapCache = false;
            this.dSkinTabControl1.Controls.Add(this.dSkinTabPage1);
            this.dSkinTabControl1.Controls.Add(this.dSkinTabPage2);
            this.dSkinTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dSkinTabControl1.HoverBackColors = new System.Drawing.Color[] {
        System.Drawing.Color.Transparent,
        System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))))};
            this.dSkinTabControl1.ItemBackgroundImage = null;
            this.dSkinTabControl1.ItemBackgroundImageHover = null;
            this.dSkinTabControl1.ItemBackgroundImageSelected = null;
            this.dSkinTabControl1.ItemSize = new System.Drawing.Size(96, 33);
            this.dSkinTabControl1.Location = new System.Drawing.Point(0, 0);
            this.dSkinTabControl1.Name = "dSkinTabControl1";
            this.dSkinTabControl1.NormalBackColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))))};
            this.dSkinTabControl1.PageImagePosition = DSkin.Controls.ePageImagePosition.Left;
            this.dSkinTabControl1.SelectedBackColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(186)))), ((int)(((byte)(233))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))))};
            this.dSkinTabControl1.Size = new System.Drawing.Size(786, 624);
            this.dSkinTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.dSkinTabControl1.TabIndex = 0;
            this.dSkinTabControl1.UpdownBtnArrowNormalColor = System.Drawing.Color.Black;
            this.dSkinTabControl1.UpdownBtnArrowPressColor = System.Drawing.Color.Gray;
            this.dSkinTabControl1.UpdownBtnBackColor = System.Drawing.Color.White;
            this.dSkinTabControl1.UpdownBtnBorderColor = System.Drawing.Color.Black;
            // 
            // dSkinTabPage1
            // 
            this.dSkinTabPage1.BackColor = System.Drawing.Color.Transparent;
            this.dSkinTabPage1.Controls.Add(this.gnlkControl1);
            this.dSkinTabPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dSkinTabPage1.Location = new System.Drawing.Point(0, 33);
            this.dSkinTabPage1.Name = "dSkinTabPage1";
            this.dSkinTabPage1.Size = new System.Drawing.Size(786, 591);
            this.dSkinTabPage1.TabIndex = 0;
            this.dSkinTabPage1.TabItemImage = null;
            this.dSkinTabPage1.Text = "国内旅客";
            this.dSkinTabPage1.Visible = false;
            // 
            // gnlkControl1
            // 
            this.gnlkControl1.BackColor = System.Drawing.Color.White;
            this.gnlkControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gnlkControl1.Location = new System.Drawing.Point(0, 0);
            this.gnlkControl1.Name = "gnlkControl1";
            this.gnlkControl1.Size = new System.Drawing.Size(786, 591);
            this.gnlkControl1.TabIndex = 0;
            // 
            // dSkinTabPage2
            // 
            this.dSkinTabPage2.BackColor = System.Drawing.Color.Transparent;
            this.dSkinTabPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dSkinTabPage2.Location = new System.Drawing.Point(0, 33);
            this.dSkinTabPage2.Name = "dSkinTabPage2";
            this.dSkinTabPage2.Size = new System.Drawing.Size(786, 591);
            this.dSkinTabPage2.TabIndex = 1;
            this.dSkinTabPage2.TabItemImage = null;
            this.dSkinTabPage2.Text = "境外旅客";
            this.dSkinTabPage2.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.房间管理ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(4, 34);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(792, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 房间管理ToolStripMenuItem
            // 
            this.房间管理ToolStripMenuItem.Name = "房间管理ToolStripMenuItem";
            this.房间管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.房间管理ToolStripMenuItem.Text = "房间管理";
            this.房间管理ToolStripMenuItem.Click += new System.EventHandler(this.房间管理ToolStripMenuItem_Click_2);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderColor = System.Drawing.Color.Gray;
            this.CanResize = false;
            this.CaptionOffset = new System.Drawing.Point(2, 2);
            this.ClientSize = new System.Drawing.Size(800, 700);
            this.Controls.Add(this.dSkinPanel1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleClickMaximized = false;
            this.DrawIcon = false;
            this.EnableAnimation = false;
            this.IsLayeredWindowForm = false;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "主界面";
            this.dSkinPanel1.ResumeLayout(false);
            this.dSkinTabControl1.ResumeLayout(false);
            this.dSkinTabPage1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DSkin.Controls.DSkinPanel dSkinPanel1;
        private DSkin.Controls.DSkinTabControl dSkinTabControl1;
        private DSkin.Controls.DSkinTabPage dSkinTabPage1;
        private DSkin.Controls.DSkinTabPage dSkinTabPage2;
        private Control.GNLKControl gnlkControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 房间管理ToolStripMenuItem;
    }
}