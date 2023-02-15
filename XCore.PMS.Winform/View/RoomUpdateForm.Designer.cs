
namespace XCore.PMS.Winform.View
{
    partial class RoomUpdateForm
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dSkinButton2 = new DSkin.Controls.DSkinButton();
            this.dSkinButton1 = new DSkin.Controls.DSkinButton();
            this.dSkinTextBox1 = new DSkin.Controls.DSkinTextBox();
            this.dSkinLabel2 = new DSkin.Controls.DSkinLabel();
            this.dSkinLabel1 = new DSkin.Controls.DSkinLabel();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.dSkinLabel3 = new DSkin.Controls.DSkinLabel();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(134, 106);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(152, 20);
            this.comboBox1.TabIndex = 12;
            // 
            // dSkinButton2
            // 
            this.dSkinButton2.AdaptImage = true;
            this.dSkinButton2.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(186)))), ((int)(((byte)(233)))));
            this.dSkinButton2.ButtonBorderColor = System.Drawing.Color.Gray;
            this.dSkinButton2.ButtonBorderWidth = 1;
            this.dSkinButton2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.dSkinButton2.HoverColor = System.Drawing.Color.Empty;
            this.dSkinButton2.HoverImage = null;
            this.dSkinButton2.IsPureColor = false;
            this.dSkinButton2.Location = new System.Drawing.Point(209, 204);
            this.dSkinButton2.Name = "dSkinButton2";
            this.dSkinButton2.NormalImage = null;
            this.dSkinButton2.PressColor = System.Drawing.Color.Empty;
            this.dSkinButton2.PressedImage = null;
            this.dSkinButton2.Radius = 10;
            this.dSkinButton2.ShowButtonBorder = true;
            this.dSkinButton2.Size = new System.Drawing.Size(77, 30);
            this.dSkinButton2.TabIndex = 11;
            this.dSkinButton2.Text = "取消";
            this.dSkinButton2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dSkinButton2.TextPadding = 0;
            // 
            // dSkinButton1
            // 
            this.dSkinButton1.AdaptImage = true;
            this.dSkinButton1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(186)))), ((int)(((byte)(233)))));
            this.dSkinButton1.ButtonBorderColor = System.Drawing.Color.Gray;
            this.dSkinButton1.ButtonBorderWidth = 1;
            this.dSkinButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.dSkinButton1.HoverColor = System.Drawing.Color.Empty;
            this.dSkinButton1.HoverImage = null;
            this.dSkinButton1.IsPureColor = false;
            this.dSkinButton1.Location = new System.Drawing.Point(79, 204);
            this.dSkinButton1.Name = "dSkinButton1";
            this.dSkinButton1.NormalImage = null;
            this.dSkinButton1.PressColor = System.Drawing.Color.Empty;
            this.dSkinButton1.PressedImage = null;
            this.dSkinButton1.Radius = 10;
            this.dSkinButton1.ShowButtonBorder = true;
            this.dSkinButton1.Size = new System.Drawing.Size(77, 30);
            this.dSkinButton1.TabIndex = 10;
            this.dSkinButton1.Text = "提交";
            this.dSkinButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dSkinButton1.TextPadding = 0;
            this.dSkinButton1.Click += new System.EventHandler(this.dSkinButton1_Click);
            // 
            // dSkinTextBox1
            // 
            this.dSkinTextBox1.BitmapCache = false;
            this.dSkinTextBox1.Location = new System.Drawing.Point(134, 57);
            this.dSkinTextBox1.Name = "dSkinTextBox1";
            this.dSkinTextBox1.Size = new System.Drawing.Size(152, 21);
            this.dSkinTextBox1.TabIndex = 9;
            this.dSkinTextBox1.TransparencyKey = System.Drawing.Color.Empty;
            this.dSkinTextBox1.WaterFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dSkinTextBox1.WaterText = "";
            this.dSkinTextBox1.WaterTextOffset = new System.Drawing.Point(0, 0);
            // 
            // dSkinLabel2
            // 
            this.dSkinLabel2.Font = new System.Drawing.Font("宋体", 11F);
            this.dSkinLabel2.Location = new System.Drawing.Point(79, 106);
            this.dSkinLabel2.Name = "dSkinLabel2";
            this.dSkinLabel2.Size = new System.Drawing.Size(36, 19);
            this.dSkinLabel2.TabIndex = 8;
            this.dSkinLabel2.Text = "类型";
            // 
            // dSkinLabel1
            // 
            this.dSkinLabel1.Font = new System.Drawing.Font("宋体", 11F);
            this.dSkinLabel1.Location = new System.Drawing.Point(79, 59);
            this.dSkinLabel1.Name = "dSkinLabel1";
            this.dSkinLabel1.Size = new System.Drawing.Size(36, 19);
            this.dSkinLabel1.TabIndex = 7;
            this.dSkinLabel1.Text = "名称";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(134, 152);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(152, 20);
            this.comboBox2.TabIndex = 14;
            // 
            // dSkinLabel3
            // 
            this.dSkinLabel3.Font = new System.Drawing.Font("宋体", 11F);
            this.dSkinLabel3.Location = new System.Drawing.Point(79, 152);
            this.dSkinLabel3.Name = "dSkinLabel3";
            this.dSkinLabel3.Size = new System.Drawing.Size(36, 19);
            this.dSkinLabel3.TabIndex = 13;
            this.dSkinLabel3.Text = "状态";
            // 
            // RoomUpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderColor = System.Drawing.Color.Gray;
            this.CanResize = false;
            this.CaptionOffset = new System.Drawing.Point(2, 2);
            this.ClientSize = new System.Drawing.Size(372, 292);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.dSkinLabel3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dSkinButton2);
            this.Controls.Add(this.dSkinButton1);
            this.Controls.Add(this.dSkinTextBox1);
            this.Controls.Add(this.dSkinLabel2);
            this.Controls.Add(this.dSkinLabel1);
            this.DoubleClickMaximized = false;
            this.DrawIcon = false;
            this.EnableAnimation = false;
            this.IsLayeredWindowForm = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RoomUpdateForm";
            this.ShowIcon = false;
            this.Text = "修改房间";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RoomUpdateForm_FormClosed);
            this.Load += new System.EventHandler(this.RoomUpdateForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private DSkin.Controls.DSkinButton dSkinButton2;
        private DSkin.Controls.DSkinButton dSkinButton1;
        private DSkin.Controls.DSkinTextBox dSkinTextBox1;
        private DSkin.Controls.DSkinLabel dSkinLabel2;
        private DSkin.Controls.DSkinLabel dSkinLabel1;
        private System.Windows.Forms.ComboBox comboBox2;
        private DSkin.Controls.DSkinLabel dSkinLabel3;
    }
}