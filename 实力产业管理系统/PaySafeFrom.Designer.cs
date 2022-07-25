
namespace 实力产业管理系统
{
    partial class PaySafeFrom
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKH = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblsjh = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtsjh = new System.Windows.Forms.TextBox();
            this.buyz = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(302, 43);
            this.label1.TabIndex = 0;
            this.label1.Text = "系统监测到当前支付环境存在异常，进行安全验证后可继续执行会员使用!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "会员卡号：";
            // 
            // txtKH
            // 
            this.txtKH.Location = new System.Drawing.Point(139, 72);
            this.txtKH.Name = "txtKH";
            this.txtKH.ReadOnly = true;
            this.txtKH.Size = new System.Drawing.Size(100, 23);
            this.txtKH.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "绑定手机号：";
            // 
            // lblsjh
            // 
            this.lblsjh.AutoSize = true;
            this.lblsjh.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblsjh.Location = new System.Drawing.Point(139, 120);
            this.lblsjh.Name = "lblsjh";
            this.lblsjh.Size = new System.Drawing.Size(100, 19);
            this.lblsjh.TabIndex = 4;
            this.lblsjh.Text = "151****0663";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "请输入完整手机号：";
            // 
            // txtsjh
            // 
            this.txtsjh.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtsjh.Location = new System.Drawing.Point(139, 158);
            this.txtsjh.Name = "txtsjh";
            this.txtsjh.Size = new System.Drawing.Size(130, 26);
            this.txtsjh.TabIndex = 6;
            this.txtsjh.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtsjh_KeyPress);
            // 
            // buyz
            // 
            this.buyz.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.buyz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buyz.Location = new System.Drawing.Point(44, 224);
            this.buyz.Name = "buyz";
            this.buyz.Size = new System.Drawing.Size(97, 30);
            this.buyz.TabIndex = 7;
            this.buyz.Text = "验证";
            this.buyz.UseVisualStyleBackColor = false;
            this.buyz.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Location = new System.Drawing.Point(196, 224);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 30);
            this.button2.TabIndex = 8;
            this.button2.Text = "逃跑";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(17, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "(注：你只有一次机会)";
            // 
            // PaySafeFrom
            // 
            this.AcceptButton = this.buyz;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 276);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buyz);
            this.Controls.Add(this.txtsjh);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblsjh);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtKH);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaySafeFrom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "安全验证";
            this.Load += new System.EventHandler(this.PaySafeFrom_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtKH;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblsjh;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtsjh;
        private System.Windows.Forms.Button buyz;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
    }
}