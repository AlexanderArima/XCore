
namespace XCore.PMS.Winform.View
{
    partial class RoomForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoomForm));
            this.dSkinPanel1 = new DSkin.Controls.DSkinPanel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.dSkinPanel2 = new DSkin.Controls.DSkinPanel();
            this.dSkinMenuStrip1 = new DSkin.Controls.DSkinMenuStrip();
            this.dSkinContextMenuStrip1 = new DSkin.Controls.DSkinContextMenuStrip();
            this.添加房间ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改房间ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除房间ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dSkinPanel1.SuspendLayout();
            this.dSkinMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dSkinPanel1
            // 
            this.dSkinPanel1.BackColor = System.Drawing.Color.Transparent;
            this.dSkinPanel1.Controls.Add(this.webBrowser1);
            this.dSkinPanel1.Location = new System.Drawing.Point(7, 61);
            this.dSkinPanel1.Name = "dSkinPanel1";
            this.dSkinPanel1.RightBottom = ((System.Drawing.Image)(resources.GetObject("dSkinPanel1.RightBottom")));
            this.dSkinPanel1.Size = new System.Drawing.Size(632, 632);
            this.dSkinPanel1.TabIndex = 0;
            this.dSkinPanel1.Text = "dSkinPanel1";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(632, 632);
            this.webBrowser1.TabIndex = 0;
            // 
            // dSkinPanel2
            // 
            this.dSkinPanel2.BackColor = System.Drawing.Color.Transparent;
            this.dSkinPanel2.Location = new System.Drawing.Point(648, 61);
            this.dSkinPanel2.Name = "dSkinPanel2";
            this.dSkinPanel2.RightBottom = ((System.Drawing.Image)(resources.GetObject("dSkinPanel2.RightBottom")));
            this.dSkinPanel2.Size = new System.Drawing.Size(148, 390);
            this.dSkinPanel2.TabIndex = 1;
            this.dSkinPanel2.Text = "dSkinPanel2";
            // 
            // dSkinMenuStrip1
            // 
            this.dSkinMenuStrip1.Arrow = System.Drawing.Color.Black;
            this.dSkinMenuStrip1.Back = System.Drawing.Color.White;
            this.dSkinMenuStrip1.BackRadius = 4;
            this.dSkinMenuStrip1.BackRectangle = new System.Drawing.Rectangle(10, 10, 10, 10);
            this.dSkinMenuStrip1.Base = System.Drawing.Color.White;
            this.dSkinMenuStrip1.BaseFore = System.Drawing.Color.Black;
            this.dSkinMenuStrip1.BaseForeAnamorphosis = false;
            this.dSkinMenuStrip1.BaseForeAnamorphosisBorder = 4;
            this.dSkinMenuStrip1.BaseForeAnamorphosisColor = System.Drawing.Color.White;
            this.dSkinMenuStrip1.BaseHoverFore = System.Drawing.Color.White;
            this.dSkinMenuStrip1.BaseItemAnamorphosis = true;
            this.dSkinMenuStrip1.BaseItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.dSkinMenuStrip1.BaseItemBorderShow = true;
            this.dSkinMenuStrip1.BaseItemDown = ((System.Drawing.Image)(resources.GetObject("dSkinMenuStrip1.BaseItemDown")));
            this.dSkinMenuStrip1.BaseItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.dSkinMenuStrip1.BaseItemMouse = ((System.Drawing.Image)(resources.GetObject("dSkinMenuStrip1.BaseItemMouse")));
            this.dSkinMenuStrip1.BaseItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.dSkinMenuStrip1.BaseItemRadius = 4;
            this.dSkinMenuStrip1.BaseItemRadiusStyle = DSkin.Common.RoundStyle.All;
            this.dSkinMenuStrip1.BaseItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.dSkinMenuStrip1.CheckedImage = null;
            this.dSkinMenuStrip1.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.dSkinMenuStrip1.Fore = System.Drawing.Color.Black;
            this.dSkinMenuStrip1.HoverFore = System.Drawing.Color.White;
            this.dSkinMenuStrip1.ItemAnamorphosis = true;
            this.dSkinMenuStrip1.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.dSkinMenuStrip1.ItemBorderShow = true;
            this.dSkinMenuStrip1.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.dSkinMenuStrip1.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.dSkinMenuStrip1.ItemRadius = 4;
            this.dSkinMenuStrip1.ItemRadiusStyle = DSkin.Common.RoundStyle.All;
            this.dSkinMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加房间ToolStripMenuItem,
            this.修改房间ToolStripMenuItem,
            this.删除房间ToolStripMenuItem});
            this.dSkinMenuStrip1.Location = new System.Drawing.Point(4, 34);
            this.dSkinMenuStrip1.Name = "dSkinMenuStrip1";
            this.dSkinMenuStrip1.RadiusStyle = DSkin.Common.RoundStyle.All;
            this.dSkinMenuStrip1.Size = new System.Drawing.Size(792, 25);
            this.dSkinMenuStrip1.SkinAllColor = true;
            this.dSkinMenuStrip1.TabIndex = 2;
            this.dSkinMenuStrip1.Text = "dSkinMenuStrip1";
            this.dSkinMenuStrip1.TitleAnamorphosis = true;
            this.dSkinMenuStrip1.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.dSkinMenuStrip1.TitleRadius = 4;
            this.dSkinMenuStrip1.TitleRadiusStyle = DSkin.Common.RoundStyle.All;
            // 
            // dSkinContextMenuStrip1
            // 
            this.dSkinContextMenuStrip1.Arrow = System.Drawing.Color.Black;
            this.dSkinContextMenuStrip1.Back = System.Drawing.Color.White;
            this.dSkinContextMenuStrip1.BackRadius = 4;
            this.dSkinContextMenuStrip1.Base = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(200)))), ((int)(((byte)(254)))));
            this.dSkinContextMenuStrip1.CheckedImage = null;
            this.dSkinContextMenuStrip1.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.dSkinContextMenuStrip1.Fore = System.Drawing.Color.Black;
            this.dSkinContextMenuStrip1.HoverFore = System.Drawing.Color.White;
            this.dSkinContextMenuStrip1.ItemAnamorphosis = true;
            this.dSkinContextMenuStrip1.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.dSkinContextMenuStrip1.ItemBorderShow = true;
            this.dSkinContextMenuStrip1.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.dSkinContextMenuStrip1.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.dSkinContextMenuStrip1.ItemRadius = 4;
            this.dSkinContextMenuStrip1.ItemRadiusStyle = DSkin.Common.RoundStyle.All;
            this.dSkinContextMenuStrip1.ItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.dSkinContextMenuStrip1.Name = "dSkinContextMenuStrip1";
            this.dSkinContextMenuStrip1.RadiusStyle = DSkin.Common.RoundStyle.All;
            this.dSkinContextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            this.dSkinContextMenuStrip1.SkinAllColor = true;
            this.dSkinContextMenuStrip1.TitleAnamorphosis = true;
            this.dSkinContextMenuStrip1.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.dSkinContextMenuStrip1.TitleRadius = 4;
            this.dSkinContextMenuStrip1.TitleRadiusStyle = DSkin.Common.RoundStyle.All;
            // 
            // 添加房间ToolStripMenuItem
            // 
            this.添加房间ToolStripMenuItem.Name = "添加房间ToolStripMenuItem";
            this.添加房间ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.添加房间ToolStripMenuItem.Text = "添加房间";
            this.添加房间ToolStripMenuItem.Click += new System.EventHandler(this.添加房间ToolStripMenuItem_Click);
            // 
            // 修改房间ToolStripMenuItem
            // 
            this.修改房间ToolStripMenuItem.Name = "修改房间ToolStripMenuItem";
            this.修改房间ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.修改房间ToolStripMenuItem.Text = "修改房间";
            // 
            // 删除房间ToolStripMenuItem
            // 
            this.删除房间ToolStripMenuItem.Name = "删除房间ToolStripMenuItem";
            this.删除房间ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.删除房间ToolStripMenuItem.Text = "删除房间";
            // 
            // RoomForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderColor = System.Drawing.Color.Gray;
            this.CanResize = false;
            this.CaptionOffset = new System.Drawing.Point(2, 2);
            this.ClientSize = new System.Drawing.Size(800, 700);
            this.Controls.Add(this.dSkinPanel2);
            this.Controls.Add(this.dSkinPanel1);
            this.Controls.Add(this.dSkinMenuStrip1);
            this.DoubleClickMaximized = false;
            this.DrawIcon = false;
            this.EnableAnimation = false;
            this.IsLayeredWindowForm = false;
            this.MainMenuStrip = this.dSkinMenuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RoomForm";
            this.ShowIcon = false;
            this.Text = "房间管理";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RoomForm_FormClosed);
            this.Load += new System.EventHandler(this.RoomForm_Load);
            this.dSkinPanel1.ResumeLayout(false);
            this.dSkinMenuStrip1.ResumeLayout(false);
            this.dSkinMenuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DSkin.Controls.DSkinPanel dSkinPanel1;
        private DSkin.Controls.DSkinPanel dSkinPanel2;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private DSkin.Controls.DSkinMenuStrip dSkinMenuStrip1;
        private DSkin.Controls.DSkinContextMenuStrip dSkinContextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 添加房间ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改房间ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除房间ToolStripMenuItem;
    }
}