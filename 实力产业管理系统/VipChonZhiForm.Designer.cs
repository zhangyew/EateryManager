﻿
namespace 实力产业管理系统
{
    partial class VipChonZhiForm
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
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMM = new System.Windows.Forms.TextBox();
            this.txtBZ = new System.Windows.Forms.TextBox();
            this.txtJE = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblYE = new System.Windows.Forms.Label();
            this.lblXM = new System.Windows.Forms.Label();
            this.lblKH = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(253)))), ((int)(((byte)(239)))));
            this.button11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button11.Location = new System.Drawing.Point(451, 247);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(68, 23);
            this.button11.TabIndex = 25;
            this.button11.Text = "取消";
            this.button11.UseVisualStyleBackColor = false;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(253)))), ((int)(((byte)(239)))));
            this.button12.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button12.Location = new System.Drawing.Point(326, 247);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(68, 23);
            this.button12.TabIndex = 24;
            this.button12.Text = "确定";
            this.button12.UseVisualStyleBackColor = false;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMM);
            this.groupBox2.Controls.Add(this.txtBZ);
            this.groupBox2.Controls.Add(this.txtJE);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(23, 85);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(511, 144);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "余额充值";
            // 
            // txtMM
            // 
            this.txtMM.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMM.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMM.Location = new System.Drawing.Point(350, 36);
            this.txtMM.Multiline = true;
            this.txtMM.Name = "txtMM";
            this.txtMM.Size = new System.Drawing.Size(100, 25);
            this.txtMM.TabIndex = 8;
            // 
            // txtBZ
            // 
            this.txtBZ.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBZ.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBZ.Location = new System.Drawing.Point(88, 72);
            this.txtBZ.Multiline = true;
            this.txtBZ.Name = "txtBZ";
            this.txtBZ.Size = new System.Drawing.Size(168, 50);
            this.txtBZ.TabIndex = 7;
            // 
            // txtJE
            // 
            this.txtJE.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtJE.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtJE.Location = new System.Drawing.Point(88, 36);
            this.txtJE.Multiline = true;
            this.txtJE.Name = "txtJE";
            this.txtJE.Size = new System.Drawing.Size(100, 25);
            this.txtJE.TabIndex = 4;
            this.txtJE.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtJE_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 105);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 17);
            this.label10.TabIndex = 3;
            this.label10.Text = "转账备注：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(300, 41);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 17);
            this.label9.TabIndex = 2;
            this.label9.Text = "密码：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 17);
            this.label8.TabIndex = 1;
            this.label8.Text = "充值金额：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblYE);
            this.groupBox1.Controls.Add(this.lblXM);
            this.groupBox1.Controls.Add(this.lblKH);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(23, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(511, 70);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "会员信息";
            // 
            // lblYE
            // 
            this.lblYE.AutoSize = true;
            this.lblYE.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblYE.ForeColor = System.Drawing.Color.Blue;
            this.lblYE.Location = new System.Drawing.Point(424, 35);
            this.lblYE.Name = "lblYE";
            this.lblYE.Size = new System.Drawing.Size(45, 19);
            this.lblYE.TabIndex = 5;
            this.lblYE.Text = "1030";
            // 
            // lblXM
            // 
            this.lblXM.AutoSize = true;
            this.lblXM.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblXM.ForeColor = System.Drawing.Color.Blue;
            this.lblXM.Location = new System.Drawing.Point(238, 35);
            this.lblXM.Name = "lblXM";
            this.lblXM.Size = new System.Drawing.Size(45, 19);
            this.lblXM.TabIndex = 4;
            this.lblXM.Text = "1030";
            // 
            // lblKH
            // 
            this.lblKH.AutoSize = true;
            this.lblKH.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblKH.ForeColor = System.Drawing.Color.Blue;
            this.lblKH.Location = new System.Drawing.Point(73, 35);
            this.lblKH.Name = "lblKH";
            this.lblKH.Size = new System.Drawing.Size(45, 19);
            this.lblKH.TabIndex = 3;
            this.lblKH.Text = "1030";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(344, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "储蓄卡余额：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(188, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "姓名：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "卡号：";
            // 
            // VipChonZhiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(234)))), ((int)(((byte)(207)))));
            this.ClientSize = new System.Drawing.Size(557, 280);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VipChonZhiForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "会员卡充值";
            this.Load += new System.EventHandler(this.VipChonZhiForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtMM;
        private System.Windows.Forms.TextBox txtBZ;
        private System.Windows.Forms.TextBox txtJE;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblYE;
        private System.Windows.Forms.Label lblXM;
        private System.Windows.Forms.Label lblKH;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}