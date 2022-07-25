
namespace 实力产业管理系统
{
    partial class UpdateRoomForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtMC = new System.Windows.Forms.TextBox();
            this.txtQY = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbtZK = new System.Windows.Forms.RadioButton();
            this.txtXF = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "餐桌名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "最低消费：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "容纳人数：";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(253)))), ((int)(((byte)(239)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Location = new System.Drawing.Point(214, 255);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 27);
            this.button1.TabIndex = 7;
            this.button1.Text = "取消";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(253)))), ((int)(((byte)(239)))));
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.Location = new System.Drawing.Point(82, 255);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(74, 27);
            this.button2.TabIndex = 6;
            this.button2.Text = "确定";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtMC
            // 
            this.txtMC.Location = new System.Drawing.Point(104, 28);
            this.txtMC.Name = "txtMC";
            this.txtMC.Size = new System.Drawing.Size(186, 23);
            this.txtMC.TabIndex = 8;
            // 
            // txtQY
            // 
            this.txtQY.Location = new System.Drawing.Point(104, 145);
            this.txtQY.Name = "txtQY";
            this.txtQY.Size = new System.Drawing.Size(186, 23);
            this.txtQY.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbtZK);
            this.panel1.Location = new System.Drawing.Point(29, 174);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(259, 50);
            this.panel1.TabIndex = 11;
            // 
            // rbtZK
            // 
            this.rbtZK.AutoSize = true;
            this.rbtZK.Location = new System.Drawing.Point(4, 15);
            this.rbtZK.Name = "rbtZK";
            this.rbtZK.Size = new System.Drawing.Size(170, 21);
            this.rbtZK.TabIndex = 3;
            this.rbtZK.Text = "消费时是否对会员开启折扣";
            this.rbtZK.UseVisualStyleBackColor = true;
            // 
            // txtXF
            // 
            this.txtXF.Location = new System.Drawing.Point(104, 84);
            this.txtXF.Name = "txtXF";
            this.txtXF.Size = new System.Drawing.Size(186, 23);
            this.txtXF.TabIndex = 12;
            // 
            // UpdateRoomForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.ClientSize = new System.Drawing.Size(327, 317);
            this.Controls.Add(this.txtXF);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtQY);
            this.Controls.Add(this.txtMC);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateRoomForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "房间编辑";
            this.Load += new System.EventHandler(this.UpdateRoomForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtMC;
        private System.Windows.Forms.TextBox txtQY;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtXF;
        public System.Windows.Forms.RadioButton rbtZK;
    }
}