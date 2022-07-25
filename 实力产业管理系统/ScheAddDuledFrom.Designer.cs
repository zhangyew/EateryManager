
namespace 实力产业管理系统
{
    partial class ScheAddDuledFrom
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tvFJ = new System.Windows.Forms.TreeView();
            this.txtBZ = new System.Windows.Forms.TextBox();
            this.rbtSJ = new System.Windows.Forms.DateTimePicker();
            this.txtRS = new System.Windows.Forms.TextBox();
            this.txtXM = new System.Windows.Forms.TextBox();
            this.txtDH = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(284, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "选择预订餐台信息";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(197)))), ((int)(((byte)(216)))));
            this.button2.Location = new System.Drawing.Point(426, 415);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(76, 27);
            this.button2.TabIndex = 25;
            this.button2.Text = "×  取消";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(197)))), ((int)(((byte)(216)))));
            this.button1.Location = new System.Drawing.Point(258, 415);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 27);
            this.button1.TabIndex = 24;
            this.button1.Text = "√  确定";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // tvFJ
            // 
            this.tvFJ.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvFJ.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tvFJ.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(74)))), ((int)(((byte)(52)))));
            this.tvFJ.Location = new System.Drawing.Point(287, 143);
            this.tvFJ.Name = "tvFJ";
            this.tvFJ.Size = new System.Drawing.Size(215, 234);
            this.tvFJ.TabIndex = 9;
            this.tvFJ.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvFJ_AfterSelect_1);
            // 
            // txtBZ
            // 
            this.txtBZ.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBZ.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBZ.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(116)))), ((int)(((byte)(177)))));
            this.txtBZ.Location = new System.Drawing.Point(92, 143);
            this.txtBZ.Multiline = true;
            this.txtBZ.Name = "txtBZ";
            this.txtBZ.Size = new System.Drawing.Size(146, 234);
            this.txtBZ.TabIndex = 12;
            // 
            // rbtSJ
            // 
            this.rbtSJ.CustomFormat = "yyyy-MM-dd HH:mm";
            this.rbtSJ.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.rbtSJ.Location = new System.Drawing.Point(345, 84);
            this.rbtSJ.Name = "rbtSJ";
            this.rbtSJ.Size = new System.Drawing.Size(157, 23);
            this.rbtSJ.TabIndex = 14;
            // 
            // txtRS
            // 
            this.txtRS.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRS.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRS.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(116)))), ((int)(((byte)(177)))));
            this.txtRS.Location = new System.Drawing.Point(345, 31);
            this.txtRS.Multiline = true;
            this.txtRS.Name = "txtRS";
            this.txtRS.Size = new System.Drawing.Size(157, 21);
            this.txtRS.TabIndex = 11;
            this.txtRS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRS_KeyPress);
            // 
            // txtXM
            // 
            this.txtXM.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtXM.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtXM.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(116)))), ((int)(((byte)(177)))));
            this.txtXM.Location = new System.Drawing.Point(92, 35);
            this.txtXM.Multiline = true;
            this.txtXM.Name = "txtXM";
            this.txtXM.Size = new System.Drawing.Size(146, 21);
            this.txtXM.TabIndex = 9;
            // 
            // txtDH
            // 
            this.txtDH.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDH.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDH.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(116)))), ((int)(((byte)(177)))));
            this.txtDH.Location = new System.Drawing.Point(92, 83);
            this.txtDH.Multiline = true;
            this.txtDH.Name = "txtDH";
            this.txtDH.Size = new System.Drawing.Size(146, 21);
            this.txtDH.TabIndex = 8;
            this.txtDH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDH_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(31, 143);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 17);
            this.label9.TabIndex = 7;
            this.label9.Text = "客户留言：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(276, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 17);
            this.label8.TabIndex = 6;
            this.label8.Text = "客户人数：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(284, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "预抵时段：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "宾客姓名:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "联系手机：";
            // 
            // ScheAddDuledFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(234)))), ((int)(((byte)(207)))));
            this.ClientSize = new System.Drawing.Size(544, 462);
            this.Controls.Add(this.tvFJ);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtBZ);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDH);
            this.Controls.Add(this.rbtSJ);
            this.Controls.Add(this.txtRS);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtXM);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(78)))), ((int)(((byte)(47)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScheAddDuledFrom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "顾客预定";
            this.Load += new System.EventHandler(this.ScheAddDuledFrom_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TreeView tvFJ;
        private System.Windows.Forms.TextBox txtBZ;
        private System.Windows.Forms.DateTimePicker rbtSJ;
        private System.Windows.Forms.TextBox txtRS;
        private System.Windows.Forms.TextBox txtXM;
        private System.Windows.Forms.TextBox txtDH;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}