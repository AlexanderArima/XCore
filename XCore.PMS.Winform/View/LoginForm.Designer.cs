
namespace XCore.PMS.Winform.View
{
    partial class LoginForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dSkinLabel1 = new DSkin.Controls.DSkinLabel();
            this.dSkinLabel2 = new DSkin.Controls.DSkinLabel();
            this.dSkinTextBox1 = new DSkin.Controls.DSkinTextBox();
            this.dSkinTextBox2 = new DSkin.Controls.DSkinTextBox();
            this.dSkinButton1 = new DSkin.Controls.DSkinButton();
            this.dSkinButton2 = new DSkin.Controls.DSkinButton();
            this.SuspendLayout();
            // 
            // dSkinLabel1
            // 
            this.dSkinLabel1.Font = new System.Drawing.Font("宋体", 11F);
            this.dSkinLabel1.Location = new System.Drawing.Point(74, 63);
            this.dSkinLabel1.Name = "dSkinLabel1";
            this.dSkinLabel1.Size = new System.Drawing.Size(51, 19);
            this.dSkinLabel1.TabIndex = 0;
            this.dSkinLabel1.Text = "用户名";
            // 
            // dSkinLabel2
            // 
            this.dSkinLabel2.Font = new System.Drawing.Font("宋体", 11F);
            this.dSkinLabel2.Location = new System.Drawing.Point(89, 123);
            this.dSkinLabel2.Name = "dSkinLabel2";
            this.dSkinLabel2.Size = new System.Drawing.Size(36, 19);
            this.dSkinLabel2.TabIndex = 1;
            this.dSkinLabel2.Text = "密码";
            // 
            // dSkinTextBox1
            // 
            this.dSkinTextBox1.BitmapCache = false;
            this.dSkinTextBox1.Location = new System.Drawing.Point(140, 61);
            this.dSkinTextBox1.Name = "dSkinTextBox1";
            this.dSkinTextBox1.Size = new System.Drawing.Size(173, 21);
            this.dSkinTextBox1.TabIndex = 2;
            this.dSkinTextBox1.TransparencyKey = System.Drawing.Color.Empty;
            this.dSkinTextBox1.WaterFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dSkinTextBox1.WaterText = "";
            this.dSkinTextBox1.WaterTextOffset = new System.Drawing.Point(0, 0);
            // 
            // dSkinTextBox2
            // 
            this.dSkinTextBox2.BitmapCache = false;
            this.dSkinTextBox2.Location = new System.Drawing.Point(140, 121);
            this.dSkinTextBox2.Name = "dSkinTextBox2";
            this.dSkinTextBox2.Size = new System.Drawing.Size(173, 21);
            this.dSkinTextBox2.TabIndex = 3;
            this.dSkinTextBox2.TransparencyKey = System.Drawing.Color.Empty;
            this.dSkinTextBox2.WaterFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dSkinTextBox2.WaterText = "";
            this.dSkinTextBox2.WaterTextOffset = new System.Drawing.Point(0, 0);
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
            this.dSkinButton1.Location = new System.Drawing.Point(89, 181);
            this.dSkinButton1.Name = "dSkinButton1";
            this.dSkinButton1.NormalImage = null;
            this.dSkinButton1.PressColor = System.Drawing.Color.Empty;
            this.dSkinButton1.PressedImage = null;
            this.dSkinButton1.Radius = 10;
            this.dSkinButton1.ShowButtonBorder = true;
            this.dSkinButton1.Size = new System.Drawing.Size(85, 30);
            this.dSkinButton1.TabIndex = 4;
            this.dSkinButton1.Text = "登录";
            this.dSkinButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dSkinButton1.TextPadding = 0;
            this.dSkinButton1.Click += new System.EventHandler(this.dSkinButton1_Click);
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
            this.dSkinButton2.Location = new System.Drawing.Point(228, 181);
            this.dSkinButton2.Name = "dSkinButton2";
            this.dSkinButton2.NormalImage = null;
            this.dSkinButton2.PressColor = System.Drawing.Color.Empty;
            this.dSkinButton2.PressedImage = null;
            this.dSkinButton2.Radius = 10;
            this.dSkinButton2.ShowButtonBorder = true;
            this.dSkinButton2.Size = new System.Drawing.Size(85, 30);
            this.dSkinButton2.TabIndex = 5;
            this.dSkinButton2.Text = "取消";
            this.dSkinButton2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dSkinButton2.TextPadding = 0;
            this.dSkinButton2.Click += new System.EventHandler(this.dSkinButton2_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderColor = System.Drawing.Color.Gray;
            this.CanResize = false;
            this.CaptionOffset = new System.Drawing.Point(2, 2);
            this.ClientSize = new System.Drawing.Size(397, 258);
            this.Controls.Add(this.dSkinButton2);
            this.Controls.Add(this.dSkinButton1);
            this.Controls.Add(this.dSkinTextBox2);
            this.Controls.Add(this.dSkinTextBox1);
            this.Controls.Add(this.dSkinLabel2);
            this.Controls.Add(this.dSkinLabel1);
            this.DoubleClickMaximized = false;
            this.DrawIcon = false;
            this.EnableAnimation = false;
            this.IsLayeredWindowForm = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.ShowIcon = false;
            this.Text = "登录";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DSkin.Controls.DSkinLabel dSkinLabel1;
        private DSkin.Controls.DSkinLabel dSkinLabel2;
        private DSkin.Controls.DSkinTextBox dSkinTextBox1;
        private DSkin.Controls.DSkinTextBox dSkinTextBox2;
        private DSkin.Controls.DSkinButton dSkinButton1;
        private DSkin.Controls.DSkinButton dSkinButton2;
    }
}

