
namespace XCore.PMS.Winform.View
{
    partial class ZZLKControl_HF
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
            this.dSkinLabel1 = new DSkin.Controls.DSkinLabel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dSkinButton1 = new DSkin.Controls.DSkinButton();
            this.SuspendLayout();
            // 
            // dSkinLabel1
            // 
            this.dSkinLabel1.Location = new System.Drawing.Point(82, 89);
            this.dSkinLabel1.Name = "dSkinLabel1";
            this.dSkinLabel1.Size = new System.Drawing.Size(54, 16);
            this.dSkinLabel1.TabIndex = 0;
            this.dSkinLabel1.Text = "房间列表";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(152, 86);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 1;
            // 
            // dSkinButton1
            // 
            this.dSkinButton1.AdaptImage = true;
            this.dSkinButton1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(186)))), ((int)(((byte)(233)))));
            this.dSkinButton1.ButtonBorderColor = System.Drawing.Color.Gray;
            this.dSkinButton1.ButtonBorderWidth = 1;
            this.dSkinButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.dSkinButton1.Font = new System.Drawing.Font("宋体", 10F);
            this.dSkinButton1.HoverColor = System.Drawing.Color.Empty;
            this.dSkinButton1.HoverImage = null;
            this.dSkinButton1.IsPureColor = false;
            this.dSkinButton1.Location = new System.Drawing.Point(141, 146);
            this.dSkinButton1.Name = "dSkinButton1";
            this.dSkinButton1.NormalImage = null;
            this.dSkinButton1.PressColor = System.Drawing.Color.Empty;
            this.dSkinButton1.PressedImage = null;
            this.dSkinButton1.Radius = 10;
            this.dSkinButton1.ShowButtonBorder = true;
            this.dSkinButton1.Size = new System.Drawing.Size(80, 31);
            this.dSkinButton1.TabIndex = 2;
            this.dSkinButton1.Text = "保存";
            this.dSkinButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dSkinButton1.TextPadding = 0;
            this.dSkinButton1.Click += new System.EventHandler(this.dSkinButton1_Click);
            // 
            // ZZLKControl_HF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderColor = System.Drawing.Color.Gray;
            this.CanResize = false;
            this.CaptionOffset = new System.Drawing.Point(2, 2);
            this.ClientSize = new System.Drawing.Size(361, 217);
            this.Controls.Add(this.dSkinButton1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dSkinLabel1);
            this.DoubleClickMaximized = false;
            this.DrawIcon = false;
            this.EnableAnimation = false;
            this.IsLayeredWindowForm = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ZZLKControl_HF";
            this.ShowIcon = false;
            this.Text = "请选择新的房间后保存";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ZZLKControl_HF_FormClosed);
            this.Load += new System.EventHandler(this.ZZLKControl_HF_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DSkin.Controls.DSkinLabel dSkinLabel1;
        private System.Windows.Forms.ComboBox comboBox1;
        private DSkin.Controls.DSkinButton dSkinButton1;
    }
}